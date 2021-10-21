
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.InputSystem;

public sealed class Player : Damageable
{
    #region Variables

    [Space]

    [SerializeField] public Transform cameraPivot = null;

    [SerializeField] public Transform cameraFollower = null;

    [Space]

    [SerializeField] private Vector3 cameraForward;

    [SerializeField] private Vector3 cameraRight;

    [Space]

    [SerializeField] public Vector2 cameraPivotLocalEulerAnglesMin = new Vector2(-55f, 0f);

    [SerializeField] public Vector2 cameraPivotLocalEulerAnglesMax = new Vector2(55f, 0f);

    [Space]

    [SerializeField] public float cameraPivotLocalRotationSensitivity = 1f;

    [Space]

    [SerializeField] private Animator animator = null;

    [Space]

    [SerializeField] public Transform AimTarget = null;

    [Space]

    [SerializeField] private GameObject _ammos = null;

    [SerializeField] private GameObject _consumables = null;

    [SerializeField] private GameObject _weapons = null;

    [Space]

    [SerializeField] private Vector3 movePosition = Vector3.zero;

    [Space]

    [SerializeField] private Vector2 moveDirection = Vector2.zero;

    [Space]

    [SerializeField] private Vector2 lookDirection = Vector2.zero;

    [Space]

    [SerializeField] private int jumpCount = 0;

    private GroundedCheckSphere groundedCheckSphere;

    [Space]

    [SerializeField] private PlayerData playerData = null;

    private Dictionary<ItemCode, Ammo> ammos = new Dictionary<ItemCode, Ammo>();

    private Dictionary<ItemCode, Consumable> consumables = new Dictionary<ItemCode, Consumable>();

    private Dictionary<ItemCode, Weapon> weapons = new Dictionary<ItemCode, Weapon>();

    private Dictionary<ItemType, Item> currentItem;

    private RaycastHit raycastHit;

    private bool isGrounded = false;

    private bool isRunKeyPressed = false;

    #endregion

    #region Initialize

    protected override void Awake()
    {
        base.Awake();

        groundedCheckSphere = GetComponent<GroundedCheckSphere>();
    }

    public void Initialize()
    {
        playerData = GameManager.instance.gameData.playerData;

        damageableData = playerData.damageableData;

        transform.position = playerData.transformPosition;

        cameraPivot.localEulerAngles = playerData.cameraPivotLocalEulerAngles;

        animator.transform.localEulerAngles = playerData.animatorlocalEulerAngles;

        var ammos = _ammos.GetComponentsInChildren<Ammo>();

        int length = ammos.Length;

        for (int index = 0; index < length; ++index)
        {
            this.ammos.Add(ammos[index].itemCode, ammos[index]);

            ammos[index].animator = animator;
        }

        var consumables = _consumables.GetComponentsInChildren<Consumable>();

        length = consumables.Length;

        for (int index = 0; index < length; ++index)
        {
            this.consumables.Add(consumables[index].itemCode, consumables[index]);

            consumables[index].animator = animator;
        }

        var weapons = _weapons.GetComponentsInChildren<Weapon>();

        length = weapons.Length;

        for (int index = 0; index < length; ++index)
        {
            this.weapons.Add(weapons[index].itemCode, weapons[index]);

            weapons[index].animator = animator;
        }

        playerData = GameManager.instance.gameData.playerData;

        currentItem = new Dictionary<ItemType, Item>();

        int number = playerData.currentItemNumber[ItemType.consumable];

        currentItem.Add(ItemType.consumable, this.consumables[playerData.inventory[ItemType.consumable][number].itemCode]);

        number = playerData.currentItemNumber[ItemType.weapon];

        currentItem.Add(ItemType.weapon, this.weapons[playerData.inventory[ItemType.weapon][number].itemCode]);

        SelectWeapon(number);

        StartCoroutine(currentItem[ItemType.weapon].Draw());
    }

    #endregion

    private void Update()
    {
        if(Physics.Raycast(cameraFollower.position, cameraFollower.forward, out raycastHit, 1000f) == true)
        {
            AimTarget.position = raycastHit.point;
        }

        else
        {
            AimTarget.position = cameraFollower.forward * 1000f;
        }

        //Debug.DrawLine(cameraPivot.position, AimTarget.position, Color.red);
    }

    private void FixedUpdate()
    {
        GroundedCheck();

        Move();
    }

    private void LateUpdate()
    {
        Look();
    }

    private void OnCollisionEnter(Collision collision)
    {
        int layer = collision.gameObject.layer;

        if (layer == groundedCheckSphere.nameToLayer)
        {
            isGrounded = true;
        }

        if (layer == groundedCheckSphere.nameToLayer)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == groundedCheckSphere.nameToLayer)
        {
            isGrounded = false;
        }
    }

    #region InputSystem

    public void OnMove(InputValue value)
    {
        Move(value.Get<Vector2>());
    }

    public void OnLook(InputValue value)
    {
        Look(value.Get<Vector2>());
    }

    public void OnRun(InputValue value)
    {
        Run(value.isPressed);
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed == true)
        {
            Jump();
        }
    }

    public void OnAttack(InputValue value)
    {
        Attack(value.isPressed);
    }

    public void OnReload(InputValue value)
    {
        if (value.isPressed == true)
        {
            Reload(true);
        }
    }

    public void OnSelectWeapon(InputValue value)
    {

    }

    public void OnChangeWeaponNext(InputValue value)
    {
        if (value.isPressed == true)
        {
            SwitchWeaponNext();
        }
    }

    public void OnChangeWeaponPrevious(InputValue value)
    {
        if (value.isPressed == true)
        {
            SwitchWeaponPrevious();
        }
    }

    #endregion

    #region Movement

    private void GroundedCheck()
    {
        if (isGrounded == true)
        {
            if (groundedCheckSphere.Check() == true)
            {
                jumpCount = 0;

                animator.SetBool("isGrounded", true);
            }
        }

        else
        {
            if (groundedCheckSphere.Check() == false)
            {
                if (jumpCount == 0)
                {
                    ++jumpCount;
                }

                animator.SetBool("isGrounded", false);
            }
        }
    }

    public void Move(Vector2 moveDirection)
    {
        this.moveDirection = moveDirection;
    }

    private void Move()
    {
        cameraForward = new Vector3(cameraPivot.forward.x, 0f, cameraPivot.forward.z).normalized;

        cameraRight = new Vector3(cameraPivot.right.x, 0f, cameraPivot.right.z).normalized;

        if (moveDirection == Vector2.zero)
        {
            animator.SetFloat("movingSpeed", 0f);
        }

        else
        {
            movePosition = Vector3.zero;

            movePosition = cameraForward * moveDirection.y + cameraRight * moveDirection.x;

            playerData.animatorlocalEulerAngles = cameraForward;

            movePosition *= playerData.movingSpeed * playerData.movingSpeedMultiply;

            if (isRunKeyPressed == false)
            {
                animator.SetFloat("movingSpeed", playerData.movingSpeedMultiply);
            }

            else
            {
                movePosition *= playerData.runningSpeedMultiply;

                animator.SetFloat("movingSpeed", playerData.movingSpeedMultiply * playerData.runningSpeedMultiply);
            }

            rigidbody.MovePosition(rigidbody.position + movePosition * Time.deltaTime);
        }

        if (animator.GetBool("isAiming") == false)
        {
            if(movePosition != Vector3.zero)
            {
                animator.transform.forward = movePosition;
            }
        }

        else
        {
            animator.transform.forward = cameraForward;
        }

        /*if (groundedCheckSphere.isGrounded == true)
        {
            if (moveDirection == Vector2.zero)
            {
                movePosition = Vector3.zero;

                animator.SetFloat("movingSpeed", 0f);
            }

            else
            {
                Vector3 lookForward = new Vector3(cameraPivot.forward.x, 0f, cameraPivot.forward.z).normalized;

                Vector3 lookRight = new Vector3(cameraPivot.right.x, 0f, cameraPivot.right.z).normalized;

                movePosition = lookForward * moveDirection.y + lookRight * moveDirection.x;

                if (true)
                {
                    animator.transform.forward = movePosition;
                }

                playerData.animatorlocalEulerAngles = lookForward;

                movePosition *= playerData.movingSpeed * playerData.movingSpeedMultiply;

                if (isRunKeyPressed == false)
                {
                    animator.SetFloat("movingSpeed", playerData.movingSpeedMultiply);
                }

                else
                {
                    movePosition *= playerData.runningSpeedMultiply;

                    animator.SetFloat("movingSpeed", playerData.movingSpeedMultiply * playerData.runningSpeedMultiply);
                }

                playerData.transformPosition += movePosition * Time.deltaTime;

                rigidbody.MovePosition(playerData.transformPosition);
            }
        }*/

        playerData.transformPosition = rigidbody.position;
    }

    public void Look(Vector2 lookDirection)
    {
        this.lookDirection = lookDirection;
    }

    private void Look()
    {
        if (lookDirection != Vector2.zero)
        {
            playerData.cameraPivotLocalEulerAngles += new Vector3(lookDirection.y, lookDirection.x, 0f) * cameraPivotLocalRotationSensitivity * Time.deltaTime;

            playerData.cameraPivotLocalEulerAngles.x = Mathf.Clamp(playerData.cameraPivotLocalEulerAngles.x, cameraPivotLocalEulerAnglesMin.x, cameraPivotLocalEulerAnglesMax.x);

            cameraPivot.localEulerAngles = playerData.cameraPivotLocalEulerAngles;
        }
    }

    public void Run(bool state)
    {
        if (state == true)
        {
            isRunKeyPressed = !isRunKeyPressed;
        }
    }

    public void Jump()
    {
        if (jumpCount < playerData.jumpCountMax)
        {
            ++jumpCount;

            rigidbody.velocity = Vector3.zero;

            rigidbody.AddForce(new Vector3(0f, playerData.jumpForce, 0f), ForceMode.Impulse);

            //rigidbody.AddForce(new Vector3(movePosition.x, playerData.jumpForce, movePosition.z), ForceMode.Impulse);

            animator.SetBool("isGrounded", false);

            animator.SetTrigger("jump");
        }
    }

    #endregion

    public void GetItem(ItemInfo itemInfo)
    {
        int count = playerData.inventory[itemInfo.itemType].Count;

        for (int i = 0; i < count; ++i)
        {
            if (playerData.inventory[itemInfo.itemType][i].itemCode == itemInfo.itemCode)
            {
                itemInfo.stackCount = playerData.inventory[itemInfo.itemType][i].Stack(itemInfo.stackCount);

                if (itemInfo.stackCount == 0)
                {
                    return;
                }
            }
        }

        playerData.inventory[itemInfo.itemType].Add(itemInfo);
    }

    public void SelectItem(ItemType itemType, ItemCode itemCode)
    {
        for (int number = 0; number < playerData.inventory[itemType].Count; ++number)
        {
            if (playerData.inventory[itemType][number].itemCode == itemCode)
            {
                currentItem[itemType] = consumables[itemCode];

                currentItem[itemType].Initialize(playerData.inventory[itemType][number]);

                break;
            }
        }
    }

    private void SelectWeapon(int number)
    {
        playerData.currentItemNumber[ItemType.weapon] = number;

        currentItem[ItemType.weapon] = weapons[playerData.inventory[ItemType.weapon][number].itemCode];

        currentItem[ItemType.consumable].Initialize(playerData.inventory[ItemType.consumable][number]);
    }

    public void SwitchWeapon(int number)
    {
        if (_switchWeapon == null)
        {
            if (number > playerData.inventory[ItemType.weapon].Count - 1)
            {
                number = 0;
            }

            else if (number < 0)
            {
                number = playerData.inventory[ItemType.weapon].Count - 1;
            }

            if (number != playerData.currentItemNumber[ItemType.weapon])
            {
                _switchWeapon = _SwitchWeapon(number);

                StartCoroutine(_switchWeapon);
            }
        }
    }

    private IEnumerator _switchWeapon = null;

    private IEnumerator _SwitchWeapon(int number)
    {
        yield return currentItem[ItemType.weapon].Store();

        SelectWeapon(number);

        yield return currentItem[ItemType.weapon].Draw();

        _switchWeapon = null;
    }

    public void SwitchWeaponNext()
    {
        SwitchWeapon(playerData.currentItemNumber[ItemType.weapon] + 1);
    }

    public void SwitchWeaponPrevious()
    {
        SwitchWeapon(playerData.currentItemNumber[ItemType.weapon] - 1);
    }

    public void Consum(bool state)
    {
        currentItem[ItemType.consumable].Consum(state);
    }

    public void Medikit()
    {

    }

    public void Grenade()
    {

    }

    public void Attack(bool state)
    {
        movePosition = new Vector3(cameraPivot.forward.x, 0f, cameraPivot.forward.z).normalized;

        animator.transform.forward = movePosition;

        currentItem[ItemType.weapon].Attack(state);
    }

    public void Reload(bool state)
    {
        currentItem[ItemType.weapon].Reload(state);
    }

    protected override void Dead()
    {
        Debug.Log("YOU DIED");
    }
}