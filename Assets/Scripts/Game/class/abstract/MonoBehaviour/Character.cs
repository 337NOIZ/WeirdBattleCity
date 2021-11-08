
using System.Collections;

using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Space]

    [SerializeField] private Animator _animator = null;

    [Space]

    [SerializeField] private ImageFillAmountController healthPointBar = null;

    public Animator animator { get { return _animator; } }

    public new Rigidbody rigidbody { get; protected set; }

    public abstract CharacterType characterType { get; }

    public abstract CharacterCode characterCode { get; }

    public CharacterData characterData { get; protected set; }

    public CharacterInfo characterInfo { get; protected set; }

    public int healthPoint { get; private set; }

    public int healthPoint_Max { get; private set; }

    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public virtual void Initialize()
    {
        characterData = GameMaster.instance.gameData.levelData.characterDatas[characterCode];
    }

    public virtual void Initialize(int level) { }

    public void Caching()
    {
        healthPoint_Max = Mathf.FloorToInt(characterData.damageableData.healthPoint_Max * characterInfo.damageableInfo.healthPoint_Max_Multiple);
    }

    public void LevelUp(int level)
    {
        if(level == 1)
        {
            var characterInfo_LevelUpData = new CharacterInfo_LevelUpData(characterData.characterInfo_LevelUpData);

            characterInfo_LevelUpData.level = level;

            LevelUp(characterInfo_LevelUpData);
        }

        else
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        LevelUp(characterData.characterInfo_LevelUpData);
    }

    protected virtual void LevelUp(CharacterInfo_LevelUpData characterInfo_LevelUpData)
    {
        characterInfo.LevelUp(characterInfo_LevelUpData);
    }

    public void GainHealthPoint(int healthPoint)
    {
        this.healthPoint = characterInfo.damageableInfo.healthPoint += healthPoint;

        if (healthPoint > 0)
        {
            if (this.healthPoint > healthPoint_Max)
            {
                this.healthPoint = healthPoint_Max;
            }
        }

        else if (characterInfo.damageableInfo.invincibleTimer == 0f)
        {
            if (this.healthPoint <= 0)
            {
                this.healthPoint = 0;

                Dead();

                return;
            }
        }

        if (healthPointBar != null)
        {
            healthPointBar.Fill(this.healthPoint / healthPoint_Max, 0.1f);
        }
    }

    protected IEnumerator invincible = null;

    protected IEnumerator Invincible()
    {
        characterInfo.damageableInfo.invincibleTimer = characterData.damageableData.invincibleTime;

        while (characterInfo.damageableInfo.invincibleTimer > 0f)
        {
            characterInfo.damageableInfo.invincibleTimer -= Time.deltaTime;

            yield return null;
        }
    }

    protected virtual void Dead() { }

    protected IEnumerator SkillCooldown(int skillNumber)
    {
        var skillInfo = characterInfo.skillInfos[skillNumber];

        skillInfo.cooldownTimer = characterData.skillDatas[skillNumber].cooldownTime / characterInfo.skillInfos[skillNumber].cooldownSpeed;

        while (skillInfo.cooldownTimer > 0f)
        {
            yield return null;

            skillInfo.cooldownTimer -= Time.deltaTime;
        }

        skillInfo.cooldownTimer = 0f;
    }
}