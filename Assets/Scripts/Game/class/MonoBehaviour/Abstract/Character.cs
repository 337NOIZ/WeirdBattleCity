
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public abstract CharacterType characterType { get; }

    public abstract CharacterCode characterCode { get; }

    [SerializeField] protected LayerMask _attackable = default;

    [SerializeField] protected Transform _aimTarget = null;

    [SerializeField] protected Transform _aim = null;

    [SerializeField] protected TransformWizard _model = null;

    [SerializeField] protected TransformWizard _ragDoll = null;

    [SerializeField] protected GameObject _items = null;

    [SerializeField] protected Canvas _canvas = null;

    [SerializeField] protected ImageFillAmountController _healthPointBar = null;

    [SerializeField] protected ImageFillAmountController _experiencePointBar = null;

    public LayerMask attackable { get => _attackable; }

    public Transform aimTarget { get => _aimTarget; }

    protected Rigidbody _rigidbody;

    public Animator animator { get; protected set; } = null;

    public AnimatorWizard animatorWizard { get; protected set; } = null;

    public SkillWizard skillWizard { get; protected set; } = null;

    public CharacterData _characterData;

    protected CharacterInfo _characterInfo;

    protected MovementInfo _movementInfo  = null;

    public DamageableInfo damageableInfo { get; protected set; } = null;

    protected ExperienceInfo _experienceInfo = null;

    protected List<SkillInfo> _skillInfos = null;

    protected InventoryInfo _inventoryInfo = null;

    protected TransformInfo _transformInfo = null;

    protected int _skillInfos_Count = 0;

    protected string _animatorStance = "characterStance";

    protected SkillInfo _skillInfo = null;

    protected Character _skillTarget = null;

    protected Transform _skillTarget_transform = null;

    protected Transform _skillTarget_aimTarget = null;

    protected Character skillTarget
    {
        set
        {
            _skillTarget = value;

            if (_skillTarget != null)
            {
                _skillTarget_transform = _skillTarget.transform;

                _skillTarget_aimTarget = _skillTarget.aimTarget;
            }

            else
            {
                _skillTarget_transform = null;

                _skillTarget_aimTarget = null;
            }
        }
    }

    protected Dictionary<ItemCode, InventoryItem> items = new Dictionary<ItemCode, InventoryItem>();

    protected Dictionary<ItemType, int> selectedItemNumbers;

    protected Dictionary<ItemType, InventoryItem> currentItems;

    protected Character _attacker = null;

    public virtual void Awaken()
    {
        _rigidbody = GetComponent<Rigidbody>();

        skillWizard = GetComponentInChildren<SkillWizard>();

        skillWizard.Awaken();

        animatorWizard = skillWizard.animatorWizard;

        animator = skillWizard.animator;
    }

    public virtual void Initialize()
    {
        _movementInfo = _characterInfo.movementInfo;

        damageableInfo = _characterInfo.damageableInfo;

        _experienceInfo = _characterInfo.experienceInfo;

        _skillInfos = _characterInfo.skillInfos;

        if(_skillInfos != null)
        {
            _skillInfos_Count = _skillInfos.Count;
        }

        _inventoryInfo = _characterInfo.inventoryInfo;

        if (_items != null)
        {
            var items = _items.GetComponentsInChildren<InventoryItem>();

            if (items != null)
            {
                int index_Max = items.Length;

                for (int index = 0; index < index_Max; ++index)
                {
                    this.items.Add(items[index].itemCode, items[index]);

                    items[index].Awaken(this);
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

                int number = _inventoryInfo.currentItemNumbers[ItemType.consumable];

                SelectItem(ItemType.consumable, number);

                SetCurrentItem(ItemType.consumable, number);

                number = _inventoryInfo.currentItemNumbers[ItemType.weapon];

                SelectItem(ItemType.weapon, number);

                SetCurrentItem(ItemType.weapon, number);

                StartCoroutine(currentItems[ItemType.weapon].Draw());
            }
        }

        _transformInfo = _characterInfo.transformInfo;
    }

    public virtual void SetLevel(int characterLevel) { }

    public virtual void LevelUp(int characterLevel)
    {
        var characterInfo_LevelUpData = new CharacterInfo.LevelUpData(_characterData.levelUpData);

        if (characterLevel != 1)
        {
            characterInfo_LevelUpData.level = characterLevel;
        }

        _characterInfo.LevelUp(characterInfo_LevelUpData);
    }

    public void TakeAttack(Character attacker, float damage, List<StatusEffectInfo> statusEffectInfos)
    {
        _attacker = attacker;
        
        GetHealthPoint(-damage);
    }

    public void TakeStatusEffect(StatusEffectInfo statusEffectInfo)
    {

    }

    protected Dictionary<StatusEffectCode, IEnumerator> statusEffect = new Dictionary<StatusEffectCode, IEnumerator>();

    //protected IEnumerator StatusEffect(StatusEffectInfo statusEffectInfo)
    //{

    //}

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

    protected virtual void Dead() { }

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

    public virtual void GetMoney(float moneyAmount) { }

    protected virtual bool SearchSkillTarget() { return false; }

    protected bool IsSkillTargetWithinRange(float range)
    {
        return Vector3.Distance(transform.position, _skillTarget_transform.position) <= range;
    }

    protected virtual bool IsSkillValid() { return true; }

    protected virtual void SkillEventAction() { }

    public bool GetItem(ItemInfo itemInfo)
    {
        var itemInfo_ = FindItem(itemInfo.itemType, itemInfo.itemCode);

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

    protected int SelectItem(ItemType itemType, int number)
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

        return selectedItemNumbers[itemType] = number;
    }

    protected void SetCurrentItem(ItemType itemType, int number)
    {
        _inventoryInfo.currentItemNumbers[itemType] = number;

        currentItems[itemType] = items[_inventoryInfo.itemInfos[itemType][number].itemCode];

        currentItems[itemType].Initialize(_inventoryInfo.itemInfos[itemType][number]);
    }

    protected IEnumerator switchWeaponRoutine = null;

    protected IEnumerator SwitchWeaponRoutine(int number)
    {
        yield return currentItems[ItemType.weapon].Store();

        SetCurrentItem(ItemType.weapon, number);

        yield return currentItems[ItemType.weapon].Draw();

        switchWeaponRoutine = null;
    }

    protected void ItemSkill(ItemType itemType, int skillNumber)
    {
        currentItems[itemType].StartSkill(skillNumber);
    }

    public void ReloadWeapon()
    {
        StartCoroutine(currentItems[ItemType.weapon].Reload());
    }
}