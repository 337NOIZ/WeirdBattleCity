
using System.Collections.Generic;

using UnityEngine;

using UnityEngine.InputSystem;

public class Player : Damageable
{
    #region Variables

    [Space]

    [SerializeField] public Transform cameraPivot = null;

    [Space]

    [SerializeField] public Vector2 cameraPivotLocalEulerAnglesMin = new Vector2(-55f, 0f);

    [SerializeField] public Vector2 cameraPivotLocalEulerAnglesMax = new Vector2(55f, 0f);

    [Space]

    [SerializeField] public float cameraPivotLocalRotationSensitivity = 1f;

    [Space]

    [SerializeField] private Animator animator = null;

    [Space]

    [SerializeField] private GameObject _leftHandAmmos = null;

    [SerializeField] private GameObject _leftHandWeapons = null;

    [Space]

    [SerializeField] private GameObject _rightHandConsumables = null;

    [SerializeField] private GameObject _rightHandWeapons = null;

    [Space]

    [SerializeField] private Vector3 movePosition = Vector3.zero;

    [Space]

    [SerializeField] private Vector2 moveDirection = Vector2.zero;

    [Space]

    [SerializeField] private Vector2 lookDirection = Vector2.zero;

    [Space]

    [SerializeField] private int jumpCount = 0;

    [Space]

    [SerializeField] private PlayerData playerData;

    private new Rigidbody rigidbody;

    private GroundedCheckSphere groundedCheckSphere;

    private Dictionary<ItemCode, Ammo> leftHandAmmos = new Dictionary<ItemCode, Ammo>();

    private Dictionary<ItemCode, Weapon> leftHandWeapons = new Dictionary<ItemCode, Weapon>();

    private Dictionary<ItemCode, Consumable> rightHandConsumables = new Dictionary<ItemCode, Consumable>();

    private Dictionary<ItemCode, Weapon> rightHandWeapons = new Dictionary<ItemCode, Weapon>();

    private Dictionary<ItemType, Item> currentItem;

    private bool isGrounded = false;

    private bool isRunKeyPressed = false;

    #endregion

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        groundedCheckSphere = GetComponent<GroundedCheckSphere>();
    }

    public void Initialize()
    {
        playerData = DataManager.instance.gameData.playerData;

        damageableData = playerData.damageableData;

        transform.position = playerData.transformPosition;

        cameraPivot.localEulerAngles = playerData.cameraPivotLocalEulerAngles;

        animator.transform.localEulerAngles = playerData.animatorlocalEulerAngles;

        _leftHandAmmos.SetActive(true);

        var ammos = _leftHandAmmos.GetComponentsInChildren<Ammo>();

        int length = ammos.Length;

        for (int index = 0; index < length; ++index)
        {
            leftHandAmmos.Add(ammos[index].itemCode, ammos[index]);

            ammos[index].Initialize(animator);
        }

        _rightHandConsumables.SetActive(true);

        var consumables = _rightHandConsumables.GetComponentsInChildren<Consumable>();

        length = consumables.Length;

        for (int index = 0; index < length; ++index)
        {
            rightHandConsumables.Add(consumables[index].itemCode, consumables[index]);

            consumables[index].Initialize(animator);
        }

        _leftHandWeapons.SetActive(true);

        var weapons = _leftHandWeapons.GetComponentsInChildren<Weapon>();

        length = weapons.Length;

        for (int intdex = 0; intdex < length; ++intdex)
        {
            leftHandWeapons.Add(weapons[intdex].itemCode, weapons[intdex]);

            weapons[intdex].Initialize(animator);
        }

        _rightHandWeapons.SetActive(true);

        weapons = _rightHandWeapons.GetComponentsInChildren<Weapon>();

        length = weapons.Length;

        for (int index = 0; index < length; ++index)
        {
            rightHandWeapons.Add(weapons[index].itemCode, weapons[index]);

            weapons[index].Initialize(animator);
        }

        playerData = DataManager.instance.gameData.playerData;

        currentItem = new Dictionary<ItemType, Item>();

        int number = playerData.currentItemNumber[ItemType.CONSUMABLE];

        currentItem.Add(ItemType.CONSUMABLE, rightHandConsumables[playerData.inventory[ItemType.CONSUMABLE][number].itemCode]);

        number = playerData.currentItemNumber[ItemType.WEAPON];

        currentItem.Add(ItemType.WEAPON, rightHandWeapons[playerData.inventory[ItemType.WEAPON][number].itemCode]);

        currentItem[ItemType.WEAPON].itemData = playerData.inventory[ItemType.WEAPON][number];

        currentItem[ItemType.WEAPON].Equip(true);
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
            ChangeWeaponNext();
        }
    }

    public void OnChangeWeaponPrevious(InputValue value)
    {
        if (value.isPressed == true)
        {
            ChangeWeaponPrevious();
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
        playerData.transformPosition = rigidbody.position;

        if (groundedCheckSphere.isGrounded == true)
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
        }
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

            rigidbody.AddForce(new Vector3(movePosition.x, playerData.jumpForce, movePosition.z), ForceMode.Impulse);

            animator.SetBool("isGrounded", false);

            animator.SetTrigger("jump");
        }
    }

    #endregion

    public void GetItem(ItemData itemData)
    {
        int count = playerData.inventory[itemData.itemType].Count;

        for (int i = 0; i < count; ++i)
        {
            if (playerData.inventory[itemData.itemType][i].itemCode == itemData.itemCode)
            {
                itemData.count = playerData.inventory[itemData.itemType][i].Stack(itemData.count);

                if (itemData.onlyHaveOne == false || itemData.count == 0)
                {
                    return;
                }
            }
        }

        playerData.inventory[itemData.itemType].Add(itemData);
    }

    public void SelectConsumable(ItemCode itemCode)
    {
        for (int number = playerData.inventory[ItemType.CONSUMABLE].Count - 1; number >= 0; --number)
        {
            if (playerData.inventory[ItemType.CONSUMABLE][number].itemCode == itemCode)
            {
                currentItem[ItemType.CONSUMABLE] = rightHandConsumables[itemCode];

                currentItem[ItemType.CONSUMABLE].itemData = playerData.inventory[ItemType.CONSUMABLE][number];

                break;
            }
        }
    }

    public void Consum(bool state)
    {
        currentItem[ItemType.CONSUMABLE].Consum(state);
    }

    public void Attack(bool state)
    {
        currentItem[ItemType.WEAPON].Attack(state);
    }

    public void Reload(bool state)
    {
        currentItem[ItemType.WEAPON].Reload(state);
    }

    public void SelectWeapon(int number)
    {
        currentItem[ItemType.CONSUMABLE].Consum(false);

        if (number > playerData.inventory[ItemType.WEAPON].Count - 1)
        {
            number = 0;
        }

        else if (number < 0)
        {
            number = playerData.inventory[ItemType.WEAPON].Count - 1;
        }

        if (playerData.currentItemNumber[ItemType.WEAPON] != number)
        {
            playerData.currentItemNumber[ItemType.WEAPON] = number;

            currentItem[ItemType.WEAPON].Equip(false);

            currentItem[ItemType.WEAPON] = rightHandWeapons[playerData.inventory[ItemType.WEAPON][number].itemCode];

            currentItem[ItemType.WEAPON].itemData = playerData.inventory[ItemType.WEAPON][number];

            currentItem[ItemType.WEAPON].gameObject.SetActive(true);

            currentItem[ItemType.WEAPON].Equip(true);
        }
    }

    public void ChangeWeaponNext()
    {
        SelectWeapon(playerData.currentItemNumber[ItemType.WEAPON] + 1);
    }

    public void ChangeWeaponPrevious()
    {
        SelectWeapon(playerData.currentItemNumber[ItemType.WEAPON] - 1);
    }

    protected override void Dead()
    {
        Debug.Log("YOU DIED");
    }
}