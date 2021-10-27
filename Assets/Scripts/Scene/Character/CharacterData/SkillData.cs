
using System.Collections.Generic;

public class SkillData
{
    public float skillRange { get; private set; }

    public float skillCooldownTime { get; private set; }

    public float skillTime { get; private set; }

    public int skillDamage { get; private set; }

    public List<ConditionEffect> skillConditionEffects { get; private set; } = null;

    public List<float> skillConditionEffects_Power { get; private set; } = null;

    public List<float> skillConditionEffects_Duration { get; private set; } = null;

    public SkillData(float skillRange, float skillTime, float skillCooldownTime, int skillDamage)
    {
        this.skillRange = skillRange;

        this.skillCooldownTime = skillCooldownTime;

        this.skillTime = skillTime;

        this.skillDamage = skillDamage;
    }
    public SkillData(float skillRange, float skillTime, float skillCooldownTime, int skillDamage, List<ConditionEffect> skillConditionEffects, List<float> skillConditionEffects_Power, List<float> skillConditionEffects_Duration)
    {
        this.skillRange = skillRange;

        this.skillCooldownTime = skillCooldownTime;

        this.skillTime = skillTime;

        this.skillDamage = skillDamage;

        this.skillConditionEffects = new List<ConditionEffect>(skillConditionEffects);

        this.skillConditionEffects_Power = new List<float>(skillConditionEffects_Power);

        this.skillConditionEffects_Duration = new List<float>(skillConditionEffects_Duration);
    }
    public SkillData(SkillData skillData)
    {
        skillRange = skillData.skillRange;

        skillCooldownTime = skillData.skillCooldownTime;

        skillTime = skillData.skillTime;

        skillDamage = skillData.skillDamage;

        if(skillData.skillConditionEffects != null)
        {
            skillConditionEffects = new List<ConditionEffect>(skillData.skillConditionEffects);
        }
        if(skillConditionEffects_Power != null)
        {
            skillConditionEffects_Power = new List<float>(skillData.skillConditionEffects_Power);
        }
        if(skillConditionEffects_Duration != null)
        {
            skillConditionEffects_Duration = new List<float>(skillData.skillConditionEffects_Duration);
        }
    }
}