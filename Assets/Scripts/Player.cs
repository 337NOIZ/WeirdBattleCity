using System.Collections;

using UnityEngine;

public sealed class Player : Character
{
    public override CharacterType characterType { get => CharacterType.Player; }

    public override CharacterCode characterCode { get => CharacterCode.Player; }

    public static Player instance { get; private set; }

    [SerializeField] private Transform _cameraPivot = null;

    [SerializeField] private Transform _cameraFollower = null;

    [SerializeField] private MoneyBox _moneyBox = null;

    private GroundedCheckSphere _groundedCheckSphere;

    private PlayerInfo _playerInfo;

    private Vector2 _lookDirection = Vector2.zero;

    private Vector3 _movePosition = Vector3.zero;

    private Vector2 _moveDirection = Vector2.zero;

    private void Awake()
    {
        instance = this;
    }

    public override void Awaken()
    {
        base.Awaken();

        _groundedCheckSphere = GetComponent<GroundedCheckSphere>();

        _groundedCheckSphere.Awaken();

        _Awaken();
    }

    protected override void _Awaken()
    {
        _characterData = GameManager.instance.gameData.levelData.characterDatas[characterCode];

        _playerInfo = GameManager.instance.gameInfo.levelInfo.playerInfo;

        characterInfo = _playerInfo.characterInfo;

        base._Awaken();

        if (characterInfo.transformInfo == null)
        {
            characterInfo.transformInfo = new TransformInfo(transform.position, transform.localEulerAngles);
        }

        _transformInfo = characterInfo.transformInfo;

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

            _moneyBox.StartMoveTowardsMoneyAmount(characterInfo.moneyAmount, 1f);
        }
    }

    public override void LevelUp(int characterLevel)
    {
        base.LevelUp(characterLevel);

        _healthPointBar.StartFillByLerp(1f - damageableInfo.healthPoint / damageableInfo.healthPoint_Max, 0.1f);
    }

    protected override IEnumerator _Launch()
    {
        while (true)
        {
            yield return CoroutineManager.WaitForFixedUpdate;

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

            if (_lookDirection != Vector2.zero)
            {
                _playerInfo.cameraPivot_LocalEulerAngles += new Vector3(_lookDirection.y, _lookDirection.x, 0f) * _playerInfo.cameraPivot_Sensitivity * Time.deltaTime;

                _playerInfo.cameraPivot_LocalEulerAngles = new Vector3(Mathf.Clamp(_playerInfo.cameraPivot_LocalEulerAngles.x, -55f, 55f), _playerInfo.cameraPivot_LocalEulerAngles.y, _playerInfo.cameraPivot_LocalEulerAngles.z);
            }

            _cameraPivot.transform.localEulerAngles = _playerInfo.cameraPivot_LocalEulerAngles;

            if (Physics.Raycast(_cameraFollower.position, _cameraFollower.forward * 1000f, out _raycastHit, _attackableLayers) == true)
            {
                _aim.position = _raycastHit.point;
            }

            else
            {
                _aim.position = _cameraFollower.forward * 1000f;
            }

            Vector3 cameraForward = new Vector3(_cameraPivot.transform.forward.x, 0f, _cameraPivot.transform.forward.z).normalized;

            Vector3 cameraRight = new Vector3(_cameraPivot.transform.right.x, 0f, _cameraPivot.transform.right.z).normalized;

            if (_moveDirection == Vector2.zero)
            {
                animator.SetBool("isMoving", false);

                if (animator.GetBool("isAiming") == true)
                {
                    animator.transform.forward = cameraForward;
                }
            }

            else
            {
                _movePosition = cameraForward * _moveDirection.y + cameraRight * _moveDirection.x;

                float movingSpeed = _movementInfo.movingSpeed_Walk;

                if (animator.GetBool("isAiming") == true)
                {
                    animator.transform.forward = cameraForward;
                }

                else
                {
                    if (_isRunning == true)
                    {
                        movingSpeed = _movementInfo.movingSpeed_Run;
                    }

                    animator.transform.forward = _movePosition;
                }

                _rigidbody.MovePosition(_rigidbody.position + _movePosition * movingSpeed * Time.deltaTime);

                animator.SetFloat("movingMotionSpeed", _movementInfo.movingSpeed_Multiply);

                animator.SetBool("isMoving", true);
            }

            _playerInfo.animator_LocalEulerAngles = animator.transform.localEulerAngles;
        }
    }

    protected override IEnumerator _Dead()
    {
        VirtualController.instance.interactable = false;

        var audioSourceMaster = AudioManager.instance.Pop(AudioClipCode.Die_0);

        audioSourceMaster.transform.position = transform.position;

        audioSourceMaster.gameObject.SetActive(true);

        audioSourceMaster.Play();

        yield return base._Dead();

        SceneManager.instance.LoadScene(SceneCode.Title);
    }

    public override void GetMoney(float moneyAmount)
    {
        characterInfo.moneyAmount += moneyAmount;

        if (_moneyBox != null)
        {
            _moneyBox.StartMoveTowardsMoneyAmount(characterInfo.moneyAmount, 1f);
        }
    }

    public void Look(Vector2 lookDirection)
    {
        _lookDirection = lookDirection;
    }

    public void Move(Vector2 moveDirection)
    {
        _moveDirection = moveDirection;
    }

    public void Run()
    {
        isRunning = !_isRunning;
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

    public void StartSwitchingItemNext(ItemType itemType)
    {
        StartSwitchingItem(itemType, _inventoryInfo.currentItemNumbers[itemType] + 1);
    }

    public void StartSwitchingItemPrevious(ItemType itemType)
    {
        StartSwitchingItem(itemType, _inventoryInfo.currentItemNumbers[itemType] - 1);
    }

    public void StartGrenadeSkill()
    {
        StartSwitchingItem(ItemType.Consumable, 0);

        StartItemSkill(ItemType.Consumable, 0);
    }

    public void StartMedikitSkill()
    {
        StartSwitchingItem(ItemType.Consumable, 1);

        StartItemSkill(ItemType.Consumable, 0);
    }

    public void StartWeaponSkill(int skillNumber)
    {
        StartItemSkill(ItemType.Weapon, skillNumber);
    }

    public void StopWeaponSkill()
    {
        StopItemSkill(ItemType.Weapon, true);
    }
}