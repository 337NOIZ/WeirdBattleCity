
using System.Collections.Generic;

using UnityEngine;

using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [System.Serializable]

    private class CameraPivot
    {
        [Space] public Transform transform = null;

        [Space] public Vector2 rotation = Vector2.zero;

        [Space] public Vector2 rotationMin = new Vector2(-55f, 0f);

        public Vector2 rotationMax = new Vector2(55f, 0f);

        [Space] public float rotationSensitivity = 1f;
    }

    [Space, SerializeField] private CameraPivot cameraPivot = null;

    [Space, SerializeField] private Animator animator = null;

    [Space, SerializeField] private GameObject _leftHandAmmos = null;

    [SerializeField] private GameObject _leftHandWeapons = null;

    [Space, SerializeField] private GameObject _rightHandConsumables = null;

    [SerializeField] private GameObject _rightHandWeapons = null;

    [Space] public PlayerData playerData;

    [Space, SerializeField] private Vector2 moveDirection = Vector2.zero;

    [Space, SerializeField] private Vector2 lookDirection = Vector2.zero;

    [Space, SerializeField] private bool _toggleRun = true;

    public bool toggleRun
    {
        get
        {
            if (PlayerPrefs.HasKey("ToggleRun") != true)
            {
                toggleRun = true;
            }

            else if (PlayerPrefs.GetInt("ToggleRun") == 0)
            {
                return false;
            }

            return true;
        }

        set
        {
            if (value == true)
            {
                PlayerPrefs.SetInt("ToggleRun", 1);
            }

            else
            {
                PlayerPrefs.SetInt("ToggleRun", 0);
            }
        }
    }

    public bool isRunKeyPressed { get; set; } = false;

    [Space, SerializeField] private int jumpCount = 0;

    [Space, SerializeField] private bool isGrounded = false;

    [System.Serializable]

    private class GroundedCheckSphere
    {
        [Space] public Vector3 position = new Vector3(0f, 0f, 0f);

        [Space] public float radius = 0.1f;

        [Space] public Color defaultColor = new Color(1f, 0f, 0f, 0.5f);

        public Color groundedColor = new Color(0f, 1f, 0f, 0.5f);

        [Space] public string layerMask = "Stepable";

        [Space] public QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.Ignore;

        public bool CheckSphere(Vector3 position)
        {
            return Physics.CheckSphere(position + this.position, radius, LayerMask.GetMask(layerMask), queryTriggerInteraction);
        }
    }

    [Space, SerializeField] private GroundedCheckSphere groundedCheckSphere = null;

    private Dictionary<ItemCode, Ammo> leftHandAmmos = new Dictionary<ItemCode, Ammo>();

    private Dictionary<ItemCode, Weapon> leftHandWeapons = new Dictionary<ItemCode, Weapon>();

    private Dictionary<ItemCode, Consumable> rightHandConsumables = new Dictionary<ItemCode, Consumable>();

    private Dictionary<ItemCode, Weapon> rightHandWeapons = new Dictionary<ItemCode, Weapon>();

    public Dictionary<ItemType, List<ItemData>> inventory;

    public Dictionary<ItemType, int> currentItemNumber;

    private Dictionary<ItemType, Item> currentItem;

    private new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        toggleRun = _toggleRun;
    }

    private void Start()
    {
        cameraPivot.rotation = Vector2.zero;

        cameraPivot.transform.localRotation = Quaternion.Euler(cameraPivot.rotation.x, cameraPivot.rotation.y, 0f);

        animator.transform.localRotation = Quaternion.Euler(Vector3.zero);

        var ammos = _leftHandAmmos.GetComponentsInChildren<Ammo>();

        int length = ammos.Length;

        for (int i = 0; i < length; ++i)
        {
            leftHandAmmos.Add(ammos[i].itemCode, ammos[i]);

            ammos[i].gameObject.SetActive(false);
        }

        var consumables = _rightHandConsumables.GetComponentsInChildren<Consumable>();

        length = consumables.Length;

        for (int i = 0; i < length; ++i)
        {
            rightHandConsumables.Add(consumables[i].itemCode, consumables[i]);

            consumables[i].gameObject.SetActive(false);
        }

        var weapons = _leftHandWeapons.GetComponentsInChildren<Weapon>();

        length = weapons.Length;

        for (int i = 0; i < length; ++i)
        {
            leftHandWeapons.Add(weapons[i].itemCode, weapons[i]);

            weapons[i].gameObject.SetActive(false);
        }

        weapons = _rightHandWeapons.GetComponentsInChildren<Weapon>();

        length = weapons.Length;

        for (int i = 0; i < length; ++i)
        {
            rightHandWeapons.Add(weapons[i].itemCode, weapons[i]);

            weapons[i].gameObject.SetActive(false);
        }

        playerData = DataManager.instance.gameData.playerData;

        inventory = playerData.inventory;

        currentItemNumber = playerData.currentItemNumber;

        currentItem = new Dictionary<ItemType, Item>();

        int number = currentItemNumber[ItemType.CONSUMABLE];

        currentItem.Add(ItemType.CONSUMABLE, rightHandConsumables[inventory[ItemType.CONSUMABLE][number].itemCode]);

        number = currentItemNumber[ItemType.WEAPON];

        currentItem.Add(ItemType.WEAPON, rightHandWeapons[inventory[ItemType.WEAPON][number].itemCode]);

        currentItem[ItemType.WEAPON].itemData = inventory[ItemType.WEAPON][number];

        currentItem[ItemType.WEAPON].gameObject.SetActive(true);
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
        if (collision.gameObject.layer == LayerMask.NameToLayer(groundedCheckSphere.layerMask))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(groundedCheckSphere.layerMask))
        {
            isGrounded = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundedCheckSphere.CheckSphere(transform.position) == false)
        {
            Gizmos.color = groundedCheckSphere.defaultColor;
        }

        else
        {
            Gizmos.color = groundedCheckSphere.groundedColor;
        }

        Gizmos.DrawSphere(transform.position + groundedCheckSphere.position, groundedCheckSphere.radius);
    }

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
        if (value.isPressed == true)
        {
            currentItem[ItemType.CONSUMABLE].Consum(false);

            currentItem[ItemType.WEAPON].Reload(false);

            currentItem[ItemType.WEAPON].Attack(true);
        }

        else
        {
            currentItem[ItemType.WEAPON].Attack(false);
        }
    }

    public void OnReload(InputValue value)
    {
        if (value.isPressed == true)
        {
            currentItem[ItemType.CONSUMABLE].Consum(false);

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

    private void GroundedCheck()
    {
        if (isGrounded == true)
        {
            if (groundedCheckSphere.CheckSphere(rigidbody.position) == true)
            {
                jumpCount = 0;

                animator.SetBool("isGrounded", true);
            }
        }

        else
        {
            if (groundedCheckSphere.CheckSphere(rigidbody.position) == false)
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
        var movePosition = Vector3.zero;

        if (moveDirection == Vector2.zero)
        {
            animator.SetFloat("movingSpeed", 0f);
        }

        else
        {
            Vector3 lookForward = new Vector3(cameraPivot.transform.forward.x, 0f, cameraPivot.transform.forward.z).normalized;

            Vector3 lookRight = new Vector3(cameraPivot.transform.right.x, 0f, cameraPivot.transform.right.z).normalized;

            movePosition = lookForward * moveDirection.y + lookRight * moveDirection.x;

            animator.transform.forward = movePosition;

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
        }

        rigidbody.MovePosition(rigidbody.position + movePosition * Time.deltaTime);

    }

    public void Look(Vector2 lookDirection)
    {
        this.lookDirection = lookDirection;
    }

    private void Look()
    {
        if (lookDirection != Vector2.zero)
        {
            cameraPivot.rotation += new Vector2(lookDirection.y, lookDirection.x) * Time.deltaTime * cameraPivot.rotationSensitivity;

            cameraPivot.rotation.x = Mathf.Clamp(cameraPivot.rotation.x, cameraPivot.rotationMin.x, cameraPivot.rotationMax.x);

            cameraPivot.transform.localRotation = Quaternion.Euler(cameraPivot.rotation.x, cameraPivot.rotation.y, 0f);
        }
    }

    public void Run(bool state)
    {
        if (toggleRun == true)
        {
            if (state == true)
            {
                isRunKeyPressed = !isRunKeyPressed;
            }
        }

        else
        {
            isRunKeyPressed = state;
        }
    }

    public void Jump()
    {
        if (jumpCount < playerData.jumpCountMax)
        {
            ++jumpCount;

            rigidbody.velocity = Vector3.zero;

            rigidbody.AddForce(Vector3.up * playerData.jumpForce, ForceMode.Impulse);

            animator.SetBool("isGrounded", false);

            animator.SetTrigger("jump");
        }
    }

    public void GetItem(ItemData itemData)
    {
        int count = inventory[itemData.itemType].Count;

        for (int i = 0; i < count; ++i)
        {
            if (inventory[itemData.itemType][i].itemCode == itemData.itemCode)
            {
                itemData.count = inventory[itemData.itemType][i].Stack(itemData.count);

                if (itemData.onlyHaveOne == false || itemData.count == 0)
                {
                    return;
                }
            }
        }

        inventory[itemData.itemType].Add(itemData);
    }

    public void SelectConsumable(ItemCode itemCode)
    {
        for (int number = inventory[ItemType.CONSUMABLE].Count - 1; number >= 0; --number)
        {
            if (inventory[ItemType.CONSUMABLE][number].itemCode == itemCode)
            {
                currentItem[ItemType.CONSUMABLE] = rightHandConsumables[itemCode];

                currentItem[ItemType.CONSUMABLE].itemData = inventory[ItemType.CONSUMABLE][number];

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

        if (number > inventory[ItemType.WEAPON].Count - 1)
        {
            number = 0;
        }

        else if (number < 0)
        {
            number = inventory[ItemType.WEAPON].Count - 1;
        }

        if (currentItemNumber[ItemType.WEAPON] != number)
        {
            currentItemNumber[ItemType.WEAPON] = number;

            currentItem[ItemType.WEAPON].gameObject.SetActive(false);

            currentItem[ItemType.WEAPON] = rightHandWeapons[inventory[ItemType.WEAPON][number].itemCode];

            currentItem[ItemType.WEAPON].itemData = inventory[ItemType.WEAPON][number];

            currentItem[ItemType.WEAPON].gameObject.SetActive(true);
        }
    }

    public void ChangeWeaponNext()
    {
        SelectWeapon(currentItemNumber[ItemType.WEAPON] + 1);
    }

    public void ChangeWeaponPrevious()
    {
        SelectWeapon(currentItemNumber[ItemType.WEAPON] - 1);
    }

    public void GetDamage(float damage)
    {

    }
}