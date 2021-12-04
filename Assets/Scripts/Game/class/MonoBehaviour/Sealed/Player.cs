
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public sealed class Player : Character
{
    public override CharacterType characterType => CharacterType.player;

    public override CharacterCode characterCode => CharacterCode.player;

    public static Player instance { get; private set; }

    [SerializeField] private ThirdPersonCamera _thirdPersonCamera = null;

    [SerializeField] private MoneyBox _moneyBox = null;

    [SerializeField] private GameObject _items = null;

    private GroundedCheckSphere groundedCheckSphere;

    public PlayerData playerData { get; private set; }

    public PlayerInfo playerInfo { get; private set; }

    private PlayerInfo.InventoryInfo inventoryInfo;

    private Vector2 lookDirection = Vector2.zero;

    private Vector3 movePosition = Vector3.zero;

    private Vector2 moveDirection = Vector2.zero;

    private bool isRunKeyPressed = false;

    private Dictionary<ItemCode, InventoryItem> items = new Dictionary<ItemCode, InventoryItem>();

    private Dictionary<ItemType, int> selectedItemNumbers;

    private Dictionary<ItemType, InventoryItem> currentItems;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        GroundedCheck();
    }

    private void FixedUpdate()
    {
        Look();

        Move();
    }

    public override void Initialize()
    {
        base.Initialize();

        groundedCheckSphere = GetComponent<GroundedCheckSphere>();

        playerData = GameMaster.instance.gameData.levelData.playerData;

        characterData = playerData.characterData;

        playerInfo = GameMaster.instance.gameInfo.levelInfo.playerInfo;

        characterInfo = playerInfo.characterInfo;

        damageableInfo = characterInfo.damageableInfo;

        experienceInfo = characterInfo.experienceInfo;

        movementInfo = characterInfo.movementInfo;

        inventoryInfo = playerInfo.inventoryInfo;

        _healthPointBar.fillAmount = 1f;

        StartCoroutine(_healthPointBar.FillByLerp(1f - damageableInfo.healthPoint / damageableInfo.healthPoint_Max, 0.1f));

        _experiencePointBar.fillAmount = 1f;

        StartCoroutine(_experiencePointBar.FillByLerp(1f - experienceInfo.experiencePoint / experienceInfo.experiencePoint_Max, 0.1f));

        _moneyBox.moneyAmount = 0f;

        _moneyBox.SetMoneyAmountWithDirect(characterInfo.moneyAmount, 1f);

        var items = _items.GetComponentsInChildren<InventoryItem>();

        int index_Max = items.Length;

        for (int index = 0; index < index_Max; ++index)
        {
            this.items.Add(items[index].itemCode, items[index]);

            items[index].Initialize();
        }

        selectedItemNumbers = new Dictionary<ItemType, int>()
        {
            { ItemType.consumable, 0 },

            { ItemType.weapon, 0 },
        };

        currentItems = new Dictionary<ItemType, InventoryItem>()
        {
            { ItemType.consumable, null },

            { ItemType.weapon, null },
        };

        int number = inventoryInfo.currentItemNumbers[ItemType.consumable];

        SelectItem(ItemType.consumable, number);

        SetCurrentItem(ItemType.consumable, number);

        number = inventoryInfo.currentItemNumbers[ItemType.weapon];

        SelectItem(ItemType.weapon, number);

        SetCurrentItem(ItemType.weapon, number);

        StartCoroutine(currentItems[ItemType.weapon].Draw());
    }

    public override void LevelUp(int characterLevel)
    {
        base.LevelUp(characterLevel);

        StartCoroutine(_healthPointBar.FillByLerp(1f - damageableInfo.healthPoint / damageableInfo.healthPoint_Max, 0.1f));
    }

    protected override void Dead()
    {
        Debug.Log("YOU DIED");
    }

    private void GroundedCheck()
    {
        if (groundedCheckSphere.isGrounded == true)
        {
            movementInfo.jumpCount = 0;

            animator.SetBool("isGrounded", true);
        }

        else
        {
            if (movementInfo.jumpCount == 0)
            {
                ++movementInfo.jumpCount;
            }

            animator.SetBool("isGrounded", false);
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
            playerInfo.cameraPivot_LocalEulerAngles += new Vector3(lookDirection.y, lookDirection.x, 0f) * playerInfo.cameraPivot_Sensitivity * Time.deltaTime;

            playerInfo.cameraPivot_LocalEulerAngles = new Vector3(Mathf.Clamp(playerInfo.cameraPivot_LocalEulerAngles.x, -55f, 55f), playerInfo.cameraPivot_LocalEulerAngles.y, playerInfo.cameraPivot_LocalEulerAngles.z);
        }

        _thirdPersonCamera.transform.localEulerAngles = playerInfo.cameraPivot_LocalEulerAngles;

        _aim.position = _thirdPersonCamera.GetAimPosition();
    }

    public void Move(Vector2 moveDirection)
    {
        this.moveDirection = moveDirection;
    }

    private void Move()
    {
        Vector3 cameraForward = new Vector3(_thirdPersonCamera.transform.forward.x, 0f, _thirdPersonCamera.transform.forward.z).normalized;

        Vector3 cameraRight = new Vector3(_thirdPersonCamera.transform.right.x, 0f, _thirdPersonCamera.transform.right.z).normalized;

        if (moveDirection == Vector2.zero)
        {
            animator.SetFloat("movingMotionSpeed", 0f);
        }

        else
        {
            movePosition = cameraForward * moveDirection.y + cameraRight * moveDirection.x;

            float movingSpeed = movementInfo.movingSpeed_Walk;

            animator.SetFloat("movingMotionSpeed", movementInfo.movingSpeed_Multiply);

            if (animator.GetBool("isAiming") == false)
            {
                if (isRunKeyPressed == false)
                {
                    animator.SetFloat("movingDirection_X", 1f);
                }

                else
                {
                    movingSpeed = movementInfo.movingSpeed_Run;

                    animator.SetFloat("movingDirection_X", 2f);
                }

                animator.SetFloat("movingDirection_Y", 0f);

                animator.transform.forward = movePosition;
            }

            else
            {
                animator.SetFloat("movingDirection_X", movePosition.z);

                animator.SetFloat("movingDirection_Y", -movePosition.x);
            }

            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + movePosition * movingSpeed * movementInfo.movingSpeed_Multiply * Time.deltaTime);
        }

        if (animator.GetBool("isAiming") == true)
        {
            animator.transform.forward = cameraForward;
        }

        playerInfo.animator_LocalEulerAngles = animator.transform.localEulerAngles;
    }

    public void Run()
    {
        isRunKeyPressed = !isRunKeyPressed;
    }

    public void Jump()
    {
        if (movementInfo.jumpCount < movementInfo.jumpCount_Max)
        {
            ++movementInfo.jumpCount;

            _rigidbody.velocity = Vector3.zero;

            _rigidbody.AddForce(new Vector3(0f, movementInfo.jumpForce, 0f), ForceMode.Impulse);

            animator.SetBool("isGrounded", false);

            animator.SetTrigger("jumpingMotion");
        }
    }

    public ItemInfo SearchItem(ItemType itemType, ItemCode itemCode)
    {
        var itemInfos = inventoryInfo.itemInfos[itemType];

        int count = itemInfos.Count;

        for (int index = 0; index < count; ++index)
        {
            if (itemInfos[index].itemCode == itemCode)
            {
                return itemInfos[index];
            }
        }

        return null;
    }

    public bool GetItem(ItemInfo itemInfo)
    {
        var itemInfo_ = SearchItem(itemInfo.itemType, itemInfo.itemCode);

        if (itemInfo_ != null)
        {
            float stackCount_Max = itemInfo_.stackCount_Max;

            if (itemInfo_.stackCount < stackCount_Max)
            {
                itemInfo_.stackCount += itemInfo.stackCount;

                if (itemInfo_.stackCount > stackCount_Max)
                {
                    itemInfo_.stackCount = stackCount_Max;
                }

                return true;
            }

            return false;
        }

        inventoryInfo.itemInfos[itemInfo.itemType].Add(itemInfo);

        return true;
    }

    public override void GetMoney(float moneyAmount)
    {
        characterInfo.moneyAmount += moneyAmount;

        _moneyBox.SetMoneyAmountWithDirect(characterInfo.moneyAmount, 1f);
    }

    public void SelectWeaponNext()
    {
        SelectItemNext(ItemType.weapon);
    }

    public void SelectWeaponPrevious()
    {
        SelectItemPrevious(ItemType.weapon);
    }

    private int SelectItemNext(ItemType itemType)
    {
        return SelectItem(itemType, selectedItemNumbers[itemType] + 1);
    }

    private int SelectItemPrevious(ItemType itemType)
    {
        return SelectItem(itemType, selectedItemNumbers[itemType] - 1);
    }

    private int SelectItem(ItemType itemType, int number)
    {
        int number_max = inventoryInfo.itemInfos[itemType].Count - 1;

        if (number > number_max)
        {
            number = 0;
        }

        else if (number < 0)
        {
            number = number_max;
        }

        return selectedItemNumbers[itemType] = number;
    }

    private void SetCurrentItem(ItemType itemType, int number)
    {
        inventoryInfo.currentItemNumbers[itemType] = number;

        currentItems[itemType] = items[inventoryInfo.itemInfos[itemType][number].itemCode];

        currentItems[itemType].Initialize(inventoryInfo.itemInfos[itemType][number]);
    }

    public void SwitchConsumableNext()
    {
        SelectItemNext(ItemType.consumable);

        SetCurrentItem(ItemType.consumable, selectedItemNumbers[ItemType.consumable]);
    }

    public void SwitchConsumablePrevious()
    {
        SelectItemPrevious(ItemType.consumable);

        SetCurrentItem(ItemType.consumable, selectedItemNumbers[ItemType.consumable]);
    }

    public void SwitchWeapon()
    {
        if (switchWeaponRoutine == null && inventoryInfo.currentItemNumbers[ItemType.weapon] != selectedItemNumbers[ItemType.weapon])
        {
            switchWeaponRoutine = SwitchWeaponRoutine(selectedItemNumbers[ItemType.weapon]);

            StartCoroutine(switchWeaponRoutine);
        }
    }

    private IEnumerator switchWeaponRoutine = null;

    private IEnumerator SwitchWeaponRoutine(int number)
    {
        yield return currentItems[ItemType.weapon].Store();

        SetCurrentItem(ItemType.weapon, number);

        yield return currentItems[ItemType.weapon].Draw();

        switchWeaponRoutine = null;
    }

    public void ConsumableSkill(int skillNumber)
    {
        Skill(ItemType.consumable, skillNumber);
    }

    public void WeaponSkill(int skillNumber)
    {
        Skill(ItemType.weapon, skillNumber);
    }

    private void Skill(ItemType itemType, int skillNumber)
    {
        StartCoroutine(currentItems[itemType].Skill(skillNumber));
    }

    public void StopWeaponSkill(bool keepAiming)
    {
        StartCoroutine(currentItems[ItemType.weapon].StopSkill(keepAiming));
    }

    public void ReloadWeapon()
    {
        StartCoroutine(currentItems[ItemType.weapon].Reload());
    }
}