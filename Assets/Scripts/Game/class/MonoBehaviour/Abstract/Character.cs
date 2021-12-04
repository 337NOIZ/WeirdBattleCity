
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public abstract CharacterType characterType { get; }

    public abstract CharacterCode characterCode { get; }

    [SerializeField] protected LayerMask _attackable = default;

    [SerializeField] protected Transform _target_Aim = null;

    [SerializeField] protected Transform _aim = null;

    [SerializeField] protected TransformTools _model = null;

    [SerializeField] protected TransformTools _ragDoll = null;

    [SerializeField] protected ImageFillAmountController _healthPointBar = null;

    [SerializeField] protected ImageFillAmountController _experiencePointBar = null;

    public LayerMask attackable { get; protected set; }

    public Transform target_Aim { get; private set; }

    public Animator animator { get; private set; }

    public AnimationTools animationTools { get; private set; }

    protected Rigidbody _rigidbody { get; private set; }

    public CharacterData characterData { get; protected set; }

    public CharacterInfo characterInfo { get; protected set; }

    public DamageableInfo damageableInfo { get; protected set; } = null;

    public ExperienceInfo experienceInfo { get; protected set; } = null;

    public MovementInfo movementInfo { get; protected set; } = null;

    public List<SkillInfo> skillInfos { get; protected set; } = null;

    protected int skillInfos_Count;

    public TransformInfo transformInfo { get; protected set; } = null;

    protected SkillInfo skillInfo = null;

    protected Character attacker = null;

    public virtual void Initialize()
    {
        attackable = _attackable;

        target_Aim = _target_Aim;

        animator = _model.GetComponentInChildren<Animator>();

        animationTools = _model.GetComponentInChildren<AnimationTools>();

        _rigidbody = GetComponent<Rigidbody>();
    }

    public virtual void Initialize(int characterLevel) { }

    public virtual void LevelUp(int characterLevel)
    {
        var characterInfo_LevelUpData = new CharacterInfo.LevelUpData(characterData.levelUpData);

        if (characterLevel != 1)
        {
            characterInfo_LevelUpData.level = characterLevel;
        }

        characterInfo.LevelUp(characterInfo_LevelUpData);
    }

    public void TakeAttack(Character attacker, float damage, List<StatusEffectInfo> statusEffectInfos)
    {
        this.attacker = attacker;
        
        GetHealthPoint(-damage);
    }

    public void TakeForce(Vector3 position, float force)
    {
        var velocity = (transform.position - position).normalized * force;

        _rigidbody.velocity += velocity;
    }

    public void TakeStatusEffect(StatusEffectInfo statusEffectInfo)
    {

    }

    private Dictionary<StatusEffectCode, IEnumerator> statusEffect = new Dictionary<StatusEffectCode, IEnumerator>();

    //private IEnumerator StatusEffect(StatusEffectInfo statusEffectInfo)
    //{

    //}

    public void GetHealthPoint(float healthPoint)
    {
        if (healthPoint != 0f)
        {
            if (healthPoint > 0f || Invincible() == false)
            {
                damageableInfo.healthPoint += healthPoint;

                if (damageableInfo.healthPoint == 0f)
                {
                    Dead();
                }

                else
                {
                    StartCoroutine(_healthPointBar.FillByLerp(1f - damageableInfo.healthPoint / damageableInfo.healthPoint_Max, 0.1f));
                }
            }
        }
    }

    protected virtual bool Invincible()
    {
        if(_invincible == null)
        {
            _invincible = _Invincible();

            StartCoroutine(_invincible);

            return false;
        }

        return true;
    }

    private IEnumerator _invincible = null;

    private IEnumerator _Invincible()
    {
        damageableInfo.SetInvincibleTimer();

        while (damageableInfo.invincibleTimer > 0f)
        {
            yield return null;

            damageableInfo.invincibleTimer -= Time.deltaTime;
        }

        _invincible = null;
    }

    public void GetExperiencePoint(float experiencePoint)
    {
        if (_getExperiencePoint_ != null)
        {
            experienceInfo.experiencePoint += experiencePoint;
        }

        else
        {
            _getExperiencePoint_ = _GetExperiencePointRoutine_(experiencePoint);

            StartCoroutine(_getExperiencePoint_);
        }
    }

    protected IEnumerator _getExperiencePoint_ { get; private set; } = null;

    private IEnumerator _GetExperiencePointRoutine_(float experiencePoint)
    {
        experienceInfo.experiencePoint += experiencePoint;

        yield return _experiencePointBar.FillByLerp(1f - experienceInfo.experiencePoint / experienceInfo.experiencePoint_Max, 0.1f);

        while (experienceInfo.experiencePoint >= experienceInfo.experiencePoint_Max)
        {
            experienceInfo.experiencePoint -= experienceInfo.experiencePoint_Max;

            LevelUp(1);

            _experiencePointBar.fillAmount = 1f;

            if (experienceInfo.experiencePoint > 0f)
            {
                yield return _experiencePointBar.FillByLerp(1f - experienceInfo.experiencePoint / experienceInfo.experiencePoint_Max, 0.1f);
            }
        }

        _getExperiencePoint_ = null;
    }

    public virtual void GetMoney(float moneyAmount) { }

    protected virtual IEnumerator SkillCooldown(SkillInfo skillInfo)
    {
        skillInfo.SetCoolTimer();

        while (skillInfo.cooldownTimer > 0f)
        {
            yield return null;

            skillInfo.cooldownTimer -= Time.deltaTime;
        }

        skillInfo.cooldownTimer = 0f;
    }

    protected virtual void Dead() { }
}