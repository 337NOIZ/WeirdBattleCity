
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public abstract CharacterType characterType { get; }

    public abstract CharacterCode characterCode { get; }

    [SerializeField] private LayerMask _attackableLayer = default;

    [SerializeField] protected Transform _aim = null;

    [SerializeField] private Transform _aimTarget = null;

    [SerializeField] protected TransformTools _model = null;

    [SerializeField] protected TransformTools _ragDoll = null;

    [SerializeField] protected ImageFillAmountController _healthPointBar = null;

    [SerializeField] protected ImageFillAmountController _experiencePointBar = null;

    public LayerMask attackableLayer { get; protected set; }

    public Transform aimTarget => _aimTarget;

    public Animator animator { get; private set; }

    public AnimationTools animationTools { get; private set; }

    protected new Rigidbody rigidbody;

    public CharacterData characterData { get; protected set; }

    public CharacterInfo characterInfo { get; protected set; }

    public DamageableInfo damageableInfo { get; protected set; } = null;

    public ExperienceInfo experienceInfo { get; protected set; } = null;

    public MovementInfo movementInfo { get; protected set; } = null;

    public List<SkillInfo> skillInfos { get; protected set; } = null;

    public TransformInfo transformInfo { get; protected set; } = null;

    protected int skillCount = 0;

    protected List<float> skillMotionTimes;

    protected List<float> skillMotionSpeeds;

    protected int skillNumber;

    protected Character attacker = null;

    public virtual void Initialize()
    {
        attackableLayer = _attackableLayer;

        animator = _model.GetComponentInChildren<Animator>();

        animationTools = _model.GetComponentInChildren<AnimationTools>();

        rigidbody = GetComponent<Rigidbody>();
    }

    public virtual void Initialize(int characterLevel) { }

    public void LevelUp(int characterLevel)
    {
        var characterInfo_LevelUpData = new CharacterInfo.LevelUpData(characterData.levelUpData);

        if (characterLevel != 1)
        {
            characterInfo_LevelUpData.level = characterLevel;
        }

        characterInfo.LevelUp(characterInfo_LevelUpData);

        Caching();

        StartCoroutine(RefreshHealthPointBar());
    }

    protected virtual void Caching()
    {
        if (skillInfos != null)
        {
            skillCount = skillInfos.Count;
        }

        for (int index = 0; index < skillCount; ++index)
        {
            skillMotionSpeeds[index] = skillMotionTimes[index] / skillInfos[index].skillMotionTime;
        }
    }

    public IEnumerator RefreshHealthPointBar()
    {
        if (_healthPointBar != null)
        {
            yield return _healthPointBar.FillByLerp(1f - damageableInfo.healthPoint / damageableInfo.healthPoint_Max, 0.1f);
        }
    }

    public IEnumerator RefreshExperiencePointBar()
    {
        if (_experiencePointBar != null && refreshExperiencePointBarRoutine != null)
        {
            refreshExperiencePointBarRoutine = RefreshExperiencePointBarRoutine();

            yield return refreshExperiencePointBarRoutine;
        }
    }

    private IEnumerator refreshExperiencePointBarRoutine = null;

    private IEnumerator RefreshExperiencePointBarRoutine()
    {
        yield return _experiencePointBar.FillByLerp(1f - experienceInfo.experiencePoint / experienceInfo.experiencePoint_Max, 0.1f);
    }

    public void TakeAttack(Character attacker, float damage, List<StatusEffectInfo> statusEffectInfos)
    {
        this.attacker = attacker;

        GetHealthPoint(-damage);
    }

    public void TakeForce(Vector3 position, float force)
    {
        var velocity = (transform.position - position).normalized * force;

        rigidbody.velocity += velocity;
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
        if (healthPoint > 0f || Invincible() == false)
        {
            damageableInfo.healthPoint += healthPoint;

            if (damageableInfo.healthPoint == 0f)
            {
                Dead();
            }

            else
            {
                StartCoroutine(RefreshHealthPointBar());
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
        if (getExperiencePointRoutine != null)
        {
            experienceInfo.experiencePoint += experiencePoint;
        }

        else
        {
            getExperiencePointRoutine = GetExperiencePointRoutine(experiencePoint);

            StartCoroutine(getExperiencePointRoutine);
        }
    }

    private IEnumerator getExperiencePointRoutine = null;

    private IEnumerator GetExperiencePointRoutine(float experiencePoint)
    {
        experienceInfo.experiencePoint += experiencePoint;

        yield return RefreshExperiencePointBar();

        while (experienceInfo.experiencePoint >= experienceInfo.experiencePoint_Max)
        {
            experienceInfo.experiencePoint -= experienceInfo.experiencePoint_Max;

            LevelUp(1);

            _experiencePointBar.fillAmount = 1f;

            if (experienceInfo.experiencePoint > 0f)
            {
                yield return RefreshExperiencePointBar();
            }
        }

        getExperiencePointRoutine = null;
    }

    public virtual void GetMoney(float moneyAmount) { }

    protected virtual IEnumerator SkillCooldown(int skillNumber)
    {
        var skillInfo = skillInfos[skillNumber];

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