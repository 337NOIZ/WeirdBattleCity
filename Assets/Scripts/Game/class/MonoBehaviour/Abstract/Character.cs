
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Space]

    [SerializeField] private Transform _aim = null;

    [Space]

    [SerializeField] private Transform _aimTarget = null;

    [Space]

    [SerializeField] private GameObject model = null;

    [Space]

    [SerializeField] private LayerMask _hostileLayer = default;

    [Space]

    [SerializeField] protected ImageFillAmountController _healthPointBar = null;

    [SerializeField] protected ImageFillAmountController _experiencePointBar = null;

    protected Transform aim { get; private set; }

    public Transform aimTarget { get; private set; }

    public Animator animator { get; private set; }

    protected AnimationTools animationTools { get; private set; }

    public LayerMask hostileLayer { get; set; }

    protected ImageFillAmountController healthPointBar { get; private set; }

    protected ImageFillAmountController experiencePointBar { get; private set; }

    protected new Rigidbody rigidbody { get; private set; }

    public abstract CharacterType characterType { get; }

    public abstract CharacterCode characterCode { get; }

    public CharacterData characterData { get; protected set; }

    public CharacterInfo characterInfo { get; protected set; }

    protected int skillCount;

    protected List<float> skillMotionTimes;

    protected List<float> skillMotionSpeeds;

    protected List<string> skillMotionNames;

    protected int skillNumber;

    protected Character attacker = null;

    public virtual void Initialize()
    {
        aim = _aim;

        aimTarget = _aimTarget;

        animator = model.GetComponentInChildren<Animator>();

        animationTools = model.GetComponentInChildren<AnimationTools>();

        hostileLayer = _hostileLayer;

        healthPointBar = _healthPointBar;

        experiencePointBar = _experiencePointBar;

        rigidbody = GetComponent<Rigidbody>();
    }

    public virtual void Initialize(int level) { }

    public void LevelUp(int level)
    {
        if(level == 1)
        {
            LevelUp();
        }

        else
        {
            var characterInfo_LevelUpData = new CharacterInfo.LevelUpData(characterData.levelUpData);

            characterInfo_LevelUpData.level = level;

            LevelUp(characterInfo_LevelUpData);
        }
    }

    public void LevelUp()
    {
        LevelUp(characterData.levelUpData);
    }

    protected virtual void LevelUp(CharacterInfo.LevelUpData characterInfo_LevelUpData)
    {
        characterInfo.LevelUp(characterInfo_LevelUpData);
    }

    protected virtual void Caching()
    {
        for (int index = 0; index < skillCount; ++index)
        {
            skillMotionSpeeds[index] = skillMotionTimes[index] / characterInfo.skillInfos[index].skillMotionTime;
        }
    }

    public void RefreshHealthPointBar()
    {
        if (healthPointBar != null)
        {
            healthPointBar.Fill(characterInfo.damageableInfo.healthPoint / characterInfo.damageableInfo.healthPoint_Max, 0.1f);
        }
    }

    public void RefreshExperiencePointBar()
    {
        if (experiencePointBar != null)
        {
            experiencePointBar.Fill(characterInfo.experienceInfo.experiencePoint / characterInfo.experienceInfo.experiencePoint_Max, 0.1f);
        }
    }

    public void TakeAttack(Character attacker, float damage, List<StatusEffectInfo> statusEffectInfos)
    {
        this.attacker = attacker;

        GainHealthPoint(-damage);
    }

    public void GainHealthPoint(float healthPoint)
    {
        characterInfo.damageableInfo.healthPoint += healthPoint;

        if (healthPoint > 0)
        {
            if (characterInfo.damageableInfo.healthPoint > characterInfo.damageableInfo.healthPoint_Max)
            {
                characterInfo.damageableInfo.healthPoint = characterInfo.damageableInfo.healthPoint_Max;
            }
        }

        else if (Invincible() == false)
        {
            if (characterInfo.damageableInfo.healthPoint <= 0)
            {
                characterInfo.damageableInfo.healthPoint = 0;

                Dead();
            }
        }

        RefreshHealthPointBar();
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
        characterInfo.damageableInfo.SetInvincibleTimer();

        while (characterInfo.damageableInfo.invincibleTimer > 0f)
        {
            characterInfo.damageableInfo.invincibleTimer -= Time.deltaTime;

            yield return null;
        }

        _invincible = null;
    }

    public virtual void GainExperiencePoint(float experiencePoint)
    {
        if(gainExperiencePointRoutine != null)
        {
            StopCoroutine(gainExperiencePointRoutine);
        }

        gainExperiencePointRoutine = GainExperiencePointRoutine(experiencePoint);

        StartCoroutine(gainExperiencePointRoutine);
    }

    private IEnumerator gainExperiencePointRoutine = null;

    private IEnumerator GainExperiencePointRoutine(float experiencePoint)
    {
        characterInfo.experienceInfo.experiencePoint += experiencePoint;

        yield return experiencePointBar.FillRoutine(characterInfo.experienceInfo.experiencePoint / characterInfo.experienceInfo.experiencePoint_Max, 0.1f);

        while (characterInfo.experienceInfo.experiencePoint >= characterInfo.experienceInfo.experiencePoint_Max)
        {
            characterInfo.experienceInfo.experiencePoint -= characterInfo.experienceInfo.experiencePoint_Max;

            LevelUp();

            experiencePointBar.fillAmount = 0f;

            if (characterInfo.experienceInfo.experiencePoint > 0f)
            {
                yield return experiencePointBar.FillRoutine(characterInfo.experienceInfo.experiencePoint / characterInfo.experienceInfo.experiencePoint_Max, 0.1f);
            }
        }

        gainExperiencePointRoutine = null;
    }

    protected virtual IEnumerator SkillCooldown(int skillNumber)
    {
        var skillInfo = characterInfo.skillInfos[skillNumber];

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