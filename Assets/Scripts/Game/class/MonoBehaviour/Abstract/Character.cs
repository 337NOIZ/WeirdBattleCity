
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected LayerMask _attackableLayers = default;

    [SerializeField] protected TransformWizard _model = null;

    [SerializeField] protected Transform _head = null;

    [SerializeField] protected GameObject _hitBoxs_GameObject = null;

    [SerializeField] protected GameObject _inventoryItems_GameObject = null;

    [SerializeField] protected TransformWizard _ragDoll = null;

    [SerializeField] protected Transform _aim = null;

    [SerializeField] protected Canvas _canvas = null;

    [SerializeField] protected ImageFillAmountController _healthPointBar = null;

    [SerializeField] protected ImageFillAmountController _experiencePointBar = null;

    public abstract CharacterType characterType { get; }

    public abstract CharacterCode characterCode { get; }

    protected virtual string _motionTriggerName { get; } = "character";

    public LayerMask attackableLayers { get => _attackableLayers; }

    public Transform head { get => _head; }

    public Transform aim { get => _aim; }

    protected Rigidbody _rigidbody;

    public Animator animator { get; protected set; } = null;

    public AnimatorWizard animatorWizard { get; protected set; } = null;

    public SkillWizard skillWizard { get; protected set; } = null;

    protected CharacterData _characterData;

    public CharacterInfo characterInfo { get; protected set; }

    protected MovementInfo _movementInfo  = null;

    public DamageableInfo damageableInfo { get; set; } = null;

    protected ExperienceInfo _experienceInfo = null;

    protected List<SkillInfo> _skillInfos = null;

    protected InventoryInfo _inventoryInfo = null;

    protected TransformInfo _transformInfo = null;

    protected SkillInfo _skillInfo = null;

    protected bool _isRunning = false;

    protected bool isRunning
    {
        set
        {
            _isRunning = value;

            if(_isRunning == true)
            {
                animator.SetFloat("isRunning", 1f);
            }

            else
            {
                animator.SetFloat("isRunning", 0f);
            }
        }
    }

    protected Character _attacker = null;

    protected int _skillInfos_Count = 0;

    protected Dictionary<ItemCode, InventoryItem> _inventoryItems = null;

    protected Dictionary<ItemType, InventoryItem> _currentInventoryItems = null;

    protected RaycastHit _raycastHit;

    public virtual void Awaken()
    {
        _rigidbody = GetComponent<Rigidbody>();

        skillWizard = GetComponentInChildren<SkillWizard>();

        skillWizard.Awaken();

        animatorWizard = skillWizard.animatorWizard;

        animator = skillWizard.animator;

        var hitBoxs = _hitBoxs_GameObject.GetComponentsInChildren<HitBox>();

        int index_Max = hitBoxs.Length;

        for (int index = 0; index < index_Max; ++index)
        {
            hitBoxs[index].Awaken(this);
        }
    }

    protected virtual void _Awaken_()
    {
        _movementInfo = characterInfo.movementInfo;

        damageableInfo = characterInfo.damageableInfo;

        _experienceInfo = characterInfo.experienceInfo;

        _skillInfos = characterInfo.skillInfos;

        if(_skillInfos != null)
        {
            _skillInfos_Count = _skillInfos.Count;
        }

        _inventoryInfo = characterInfo.inventoryInfo;

        if (_inventoryInfo != null)
        {
            var inventoryitems = _inventoryItems_GameObject.GetComponentsInChildren<InventoryItem>();

            if (inventoryitems != null)
            {
                int index_Max = inventoryitems.Length;

                _inventoryItems = new Dictionary<ItemCode, InventoryItem>();

                for (int index = 0; index < index_Max; ++index)
                {
                    _inventoryItems.Add(inventoryitems[index].itemCode, inventoryitems[index]);

                    inventoryitems[index].Awaken(this);
                }
            }

            _currentInventoryItems = new Dictionary<ItemType, InventoryItem>()
                {
                    { ItemType.Consumable, null },

                    { ItemType.Weapon, null },
                };

            _SetCurrentItem_(ItemType.Consumable, _inventoryInfo.currentItemNumbers[ItemType.Consumable]);

            _SetCurrentItem_(ItemType.Weapon, _inventoryInfo.currentItemNumbers[ItemType.Weapon]);

            StartCoroutine(_currentInventoryItems[ItemType.Weapon].Draw());
        }
    }

    public virtual void Initialize(int characterLevel) { }

    public virtual void LevelUp(int characterLevel)
    {
        var characterInfo_LevelUpData = new CharacterInfo.LevelUpData(_characterData.levelUpData);

        if (characterLevel != 1)
        {
            characterInfo_LevelUpData.level = characterLevel;
        }

        characterInfo.LevelUp(characterInfo_LevelUpData);
    }

    public void Launch()
    {
        StartCoroutine(_Launce_());
    }

    protected virtual IEnumerator _Launce_() { yield return null; }

    public void TakeAttack(Character attacker, float damage, List<StatusEffectInfo> statusEffectInfos)
    {
        _attacker = attacker;
        
        GetHealthPoint(-damage);

        TakeStatusEffect(statusEffectInfos);
    }

    public void TakeForce(Vector3 position, float force)
    {
        var velocity = (transform.position - position).normalized * force;

        _rigidbody.velocity += velocity;
    }

    public void GetHealthPoint(float healthPoint)
    {
        if (healthPoint != 0f)
        {
            if (healthPoint > 0f || IsInvincible() == false)
            {
                damageableInfo.healthPoint += healthPoint;

                if (damageableInfo.healthPoint == 0f)
                {
                    Dead();
                }

                else
                {
                    _healthPointBar.StartFillByLerp(1f - damageableInfo.healthPoint / damageableInfo.healthPoint_Max, 0.1f);
                }
            }
        }
    }

    protected virtual bool IsInvincible()
    {
        if(invincible == null)
        {
            invincible = Invincible();

            StartCoroutine(invincible);

            return false;
        }

        return true;
    }

    protected IEnumerator invincible = null;

    protected IEnumerator Invincible()
    {
        damageableInfo.SetInvincibleTimer();

        while (damageableInfo.invincibleTimer > 0f)
        {
            yield return null;

            damageableInfo.invincibleTimer -= Time.deltaTime;
        }

        invincible = null;
    }

    protected virtual void Dead()
    {
        StopAllCoroutines();

        TransformWizard.AlignTransforms(_ragDoll, _model);

        _model.gameObject.SetActive(false);

        _ragDoll.gameObject.SetActive(true);

        StartCoroutine(_Dead_());
    }

    protected virtual IEnumerator _Dead_()
    {
        _healthPointBar.StartFillByLerp(1f - damageableInfo.healthPoint / damageableInfo.healthPoint_Max, 0.1f);

        while (_healthPointBar.fillByLerp != null) yield return null;
    }

    public void GetExperiencePoint(float experiencePoint)
    {
        if (_getExperiencePoint != null)
        {
            _experienceInfo.experiencePoint += experiencePoint;
        }

        else
        {
            _getExperiencePoint = GetExperiencePointRoutine_(experiencePoint);

            StartCoroutine(_getExperiencePoint);
        }
    }

    protected IEnumerator _getExperiencePoint = null;

    protected IEnumerator GetExperiencePointRoutine_(float experiencePoint)
    {
        _experienceInfo.experiencePoint += experiencePoint;

        _experiencePointBar.StartFillByLerp(1f - _experienceInfo.experiencePoint / _experienceInfo.experiencePoint_Max, 0.1f);

        while(_experiencePointBar.fillByLerp != null) yield return null;

        while (_experienceInfo.experiencePoint >= _experienceInfo.experiencePoint_Max)
        {
            _experienceInfo.experiencePoint -= _experienceInfo.experiencePoint_Max;

            LevelUp(1);

            _experiencePointBar.fillAmount = 1f;

            if (_experienceInfo.experiencePoint > 0f)
            {
                _experiencePointBar.StartFillByLerp(1f - _experienceInfo.experiencePoint / _experienceInfo.experiencePoint_Max, 0.1f);

                while (_experiencePointBar.fillByLerp != null) yield return null;
            }
        }

        _getExperiencePoint = null;
    }

    public void TakeStatusEffect(List<StatusEffectInfo> statusEffectInfos)
    {
        if (statusEffectInfos != null)
        {
            int index_Max = statusEffectInfos.Count;

            for (int index = 0; index < index_Max; ++index)
            {
                var statusEffectInfo = statusEffectInfos[index];

                var statusEffectCode = statusEffectInfo.statusEffectCode;

                ParticleEffect particleEffect = null;

                switch (statusEffectCode)
                {
                    case StatusEffectCode.Healing:

                        particleEffect = ObjectPool.instance.Pop(ParticleEffectCode.Healing);

                        particleEffect.transform.parent = transform;

                        particleEffect.transform.localPosition = Vector3.zero;

                        particleEffect.gameObject.SetActive(true);

                        particleEffect.Play();

                        GetHealthPoint(damageableInfo.healthPoint_Max * statusEffectInfo.power);

                        break;

                    default:

                        var characterInfo_StatusEffectInfos = characterInfo.statusEffectInfos;

                        if (characterInfo_StatusEffectInfos.ContainsKey(statusEffectCode) == true)
                        {
                            if (characterInfo_StatusEffectInfos[statusEffectCode].power < statusEffectInfo.power)
                            {
                                GainStatusEffect(statusEffectCode, statusEffectInfo.power - characterInfo_StatusEffectInfos[statusEffectCode].power);

                                characterInfo_StatusEffectInfos[statusEffectCode].power = statusEffectInfo.power;

                                if (characterInfo_StatusEffectInfos[statusEffectCode].durationTimer < statusEffectInfo.durationTime)
                                {
                                    characterInfo_StatusEffectInfos[statusEffectCode].durationTimer = statusEffectInfo.durationTime;
                                }
                            }
                        }

                        else
                        {
                            switch(statusEffectCode)
                            {
                                case StatusEffectCode.MovementSpeedDown:

                                    particleEffect = ObjectPool.instance.Pop(ParticleEffectCode.MovementSpeedDown);

                                    particleEffect.transform.parent = transform;

                                    particleEffect.transform.position = head.position;

                                    particleEffect.gameObject.SetActive(true);

                                    particleEffect.Play();

                                    break;

                                default:

                                    break;
                            }

                            characterInfo_StatusEffectInfos.Add(statusEffectCode, statusEffectInfo);

                            StartCoroutine(_StatusEffect_(statusEffectInfo, particleEffect));
                        }

                        break;
                }
            }
        }
    }

    protected void GainStatusEffect(StatusEffectCode statusEffectCode, float power)
    {
        switch (statusEffectCode)
        {
            case StatusEffectCode.MovementSpeedDown:

                _movementInfo.movingSpeed_Multiply -= power;

                break;

            case StatusEffectCode.MovementSpeedUp:

                _movementInfo.movingSpeed_Multiply += power;

                break;

            default:

                break;
        }
    }

    protected IEnumerator _StatusEffect_(StatusEffectInfo statusEffectInfo, ParticleEffect particleEffect)
    {
        var statusEffectCode = statusEffectInfo.statusEffectCode;

        GainStatusEffect(statusEffectCode, statusEffectInfo.power);

        statusEffectInfo.SetDurationTimer();

        while (statusEffectInfo.durationTimer > 0f)
        {
            yield return null;

            statusEffectInfo.durationTimer -= Time.deltaTime;
        }

        GainStatusEffect(statusEffectCode, -statusEffectInfo.power);

        characterInfo.statusEffectInfos.Remove(statusEffectCode);

        if(particleEffect != null)
        {
            particleEffect.Stop();
        }
    }

    public virtual void GetMoney(float moneyAmount) { }

    protected virtual bool IsSkillValid(int skillNumber) { return true; }

    protected virtual void SkillEventAction() { }

    public bool GetItem(ItemInfo itemInfo)
    {
        var itemInfo_ = FindItem(itemInfo.itemType, itemInfo.itemCode);

        if (itemInfo_ != null)
        {
            int stackCount_Max = itemInfo_.stackCount_Max;

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

        _inventoryInfo.itemInfos[itemInfo.itemType].Add(itemInfo);

        return true;
    }

    public ItemInfo FindItem(ItemType itemType, ItemCode itemCode)
    {
        var itemInfos = _inventoryInfo.itemInfos[itemType];

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

    protected void StartSwitchingItem(ItemType itemType, int number)
    {
        switch(itemType)
        {
            case ItemType.Consumable:

                _SetCurrentItem_(ItemType.Consumable, number);

                break;

            case ItemType.Weapon:

                if(_switchingWeapon == null)
                {
                    _switchingWeapon = SwitchingWeapon_(number);

                    StartCoroutine(_switchingWeapon);
                }

                break;
        }
    }

    protected IEnumerator _switchingWeapon = null;

    protected IEnumerator SwitchingWeapon_(int number)
    {
        yield return _currentInventoryItems[ItemType.Weapon].Store();

        _SetCurrentItem_(ItemType.Weapon, number);

        yield return _currentInventoryItems[ItemType.Weapon].Draw();

        _switchingWeapon = null;
    }

    protected void _SetCurrentItem_(ItemType itemType, int number)
    {
        int number_Max = _inventoryInfo.itemInfos[itemType].Count - 1;

        if (number > number_Max)
        {
            number = 0;
        }

        else if (number < 0)
        {
            number = number_Max;
        }

        _inventoryInfo.currentItemNumbers[itemType] = number;

        _currentInventoryItems[itemType] = _inventoryItems[_inventoryInfo.itemInfos[itemType][number].itemCode];

        _currentInventoryItems[itemType].Initialize(_inventoryInfo.itemInfos[itemType][number]);
    }

    protected void StartItemSkill(ItemType itemType, int skillNumber)
    {
        _currentInventoryItems[itemType].StartSkill(skillNumber);
    }

    protected void StopItemSkill(ItemType itemType, bool keepAiming)
    {
        _currentInventoryItems[itemType].StopSkill(keepAiming);
    }

    public void StartReloadWeapon()
    {
        _currentInventoryItems[ItemType.Weapon].StartReload();
    }
}