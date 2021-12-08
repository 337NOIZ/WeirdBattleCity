
using System.Collections.Generic;

using UnityEngine;
public sealed class Player : Character
{
    public override CharacterType characterType { get => CharacterType.player; }

    public override CharacterCode characterCode { get => CharacterCode.player; }

    public static Player instance { get; private set; }

    [SerializeField] private ThirdPersonCamera _thirdPersonCamera = null;

    [SerializeField] private MoneyBox _moneyBox = null;

    private GroundedCheckSphere _groundedCheckSphere;

    private PlayerInfo _playerInfo;

    private Vector2 _lookDirection = Vector2.zero;

    private Vector3 _movePosition = Vector3.zero;

    private Vector2 _moveDirection = Vector2.zero;

    private bool _isRunKeyPressed = false;

    public bool[] Item_unlock = new bool[3];

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

    public override void Awaken()
    {
        base.Awaken();

        _groundedCheckSphere = GetComponent<GroundedCheckSphere>();
    }

    public override void Initialize()
    {
        _playerInfo = GameMaster.instance.gameInfo.levelInfo.playerInfo;

        _characterInfo = _playerInfo.characterInfo;

        if (_characterInfo.transformInfo == null)
        {
            _characterInfo.transformInfo = new TransformInfo(transform.position, transform.localEulerAngles);
        }

        base.Initialize();

        if (_healthPointBar != null)
        {
            _healthPointBar.fillAmount = 1f;

            _healthPointBar.StartFillByLerp(1f - damageableInfo.healthPoint / damageableInfo.healthPoint_Max, 0.1f);
        }
        
        if(_experiencePointBar != null)
        {
            _experiencePointBar.fillAmount = 1f;

            _experiencePointBar.StartFillByLerp(1f - _experienceInfo.experiencePoint / _experienceInfo.experiencePoint_Max, 0.1f);
        }
        
        if(_moneyBox != null)
        {
            _moneyBox.moneyAmount = 0f;

            _moneyBox.SetMoneyAmountWithDirect(_characterInfo.moneyAmount, 1f);
        }
    }

    public override void LevelUp(int characterLevel)
    {
        base.LevelUp(characterLevel);

        _healthPointBar.StartFillByLerp(1f - damageableInfo.healthPoint / damageableInfo.healthPoint_Max, 0.1f);
    }

    public override void GetMoney(float moneyAmount)
    {
        _characterInfo.moneyAmount += moneyAmount;

        _moneyBox.SetMoneyAmountWithDirect(_characterInfo.moneyAmount, 1f);
    }

    protected override void Dead()
    {
        Debug.Log("YOU DIED");
    }

    private void GroundedCheck()
    {
        if (_groundedCheckSphere.isGrounded == true)
        {
            _movementInfo.jumpCount = 0;

            animator.SetBool("isGrounded", true);
        }

        else
        {
            if (_movementInfo.jumpCount == 0)
            {
                ++_movementInfo.jumpCount;
            }

            animator.SetBool("isGrounded", false);
        }
    }

    public void Look(Vector2 lookDirection)
    {
        _lookDirection = lookDirection;
    }

    private void Look()
    {
        if (_lookDirection != Vector2.zero)
        {
            _playerInfo.cameraPivot_LocalEulerAngles += new Vector3(_lookDirection.y, _lookDirection.x, 0f) * _playerInfo.cameraPivot_Sensitivity * Time.deltaTime;

            _playerInfo.cameraPivot_LocalEulerAngles = new Vector3(Mathf.Clamp(_playerInfo.cameraPivot_LocalEulerAngles.x, -55f, 55f), _playerInfo.cameraPivot_LocalEulerAngles.y, _playerInfo.cameraPivot_LocalEulerAngles.z);
        }

        _thirdPersonCamera.transform.localEulerAngles = _playerInfo.cameraPivot_LocalEulerAngles;

        _aim.position = _thirdPersonCamera.GetAimPosition();
    }

    public void Move(Vector2 moveDirection)
    {
        _moveDirection = moveDirection;
    }

    private void Move()
    {
        Vector3 cameraForward = new Vector3(_thirdPersonCamera.transform.forward.x, 0f, _thirdPersonCamera.transform.forward.z).normalized;

        Vector3 cameraRight = new Vector3(_thirdPersonCamera.transform.right.x, 0f, _thirdPersonCamera.transform.right.z).normalized;

        if (_moveDirection == Vector2.zero)
        {
            animator.SetFloat("movingMotionSpeed", 0f);
        }

        else
        {
            _movePosition = cameraForward * _moveDirection.y + cameraRight * _moveDirection.x;

            float movingSpeed = _movementInfo.movingSpeed_Walk;

            animator.SetFloat("movingMotionSpeed", _movementInfo.movingSpeed_Multiply);

            if (animator.GetBool("isAiming") == false)
            {
                if (_isRunKeyPressed == false)
                {
                    animator.SetFloat("movingDirection_X", 1f);
                }

                else
                {
                    movingSpeed = _movementInfo.movingSpeed_Run;

                    animator.SetFloat("movingDirection_X", 2f);
                }

                animator.SetFloat("movingDirection_Y", 0f);

                animator.transform.forward = _movePosition;
            }

            else
            {
                animator.SetFloat("movingDirection_X", _movePosition.z);

                animator.SetFloat("movingDirection_Y", -_movePosition.x);
            }

            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + _movePosition * movingSpeed * _movementInfo.movingSpeed_Multiply * Time.deltaTime);
        }

        if (animator.GetBool("isAiming") == true)
        {
            animator.transform.forward = cameraForward;
        }

        _playerInfo.animator_LocalEulerAngles = animator.transform.localEulerAngles;
    }

    public void Run()
    {
        _isRunKeyPressed = !_isRunKeyPressed;
    }

    public void Jump()
    {
        if (_movementInfo.jumpCount < _movementInfo.jumpCount_Max)
        {
            ++_movementInfo.jumpCount;

            _rigidbody.velocity = Vector3.zero;

            _rigidbody.AddForce(new Vector3(0f, _movementInfo.jumpForce, 0f), ForceMode.Impulse);

            animator.SetBool("isGrounded", false);

            animator.SetTrigger("jumpingMotion");
        }
    }

    public void SelectItemNext(ItemType itemType)
    {
        SelectItem(itemType, selectedItemNumbers[itemType] + 1);
    }

    public void SelectItemPrevious(ItemType itemType)
    {
        SelectItem(itemType, selectedItemNumbers[itemType] - 1);
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
        if (switchWeaponRoutine == null && _inventoryInfo.currentItemNumbers[ItemType.weapon] != selectedItemNumbers[ItemType.weapon])
        {
            switchWeaponRoutine = SwitchWeaponRoutine(selectedItemNumbers[ItemType.weapon]);

            StartCoroutine(switchWeaponRoutine);
        }
    }

    public void ConsumableSkill(int skillNumber)
    {
        ItemSkill(ItemType.consumable, skillNumber);
    }

    public void WeaponSkill(int skillNumber)
    {
        ItemSkill(ItemType.weapon, skillNumber);
    }

    public void StopWeaponSkill(bool keepAiming)
    {
        StartCoroutine(currentItems[ItemType.weapon].StopSkill(keepAiming));
    }
}