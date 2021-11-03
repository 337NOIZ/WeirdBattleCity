
using System.Collections.Generic;

public class SkillInfo
{
    public float skillRange_Multiple { get;  set; }

    public float skillCooldownTimer { get; set; }

    public float skillSpeed { get; set; }

    public int skillDamage_Multiple { get; set; }

    public List<float> skillConditionEffects_Power_Multiple { get; private set; } = null;

    public List<float> skillConditionEffects_Duration_Multiple { get; private set; } = null;

    public SkillInfo()
    {
        skillRange_Multiple = 1f;

        skillSpeed = 1f;

        skillCooldownTimer = 0f;

        skillDamage_Multiple = 1;
    }
    public SkillInfo(float skillRange_Multiple, float skillSpeed, float skillCooldownTimer, int skillDamage_Multiple)
    {
        this.skillRange_Multiple = skillRange_Multiple;

        this.skillSpeed = skillSpeed;

        this.skillCooldownTimer = skillCooldownTimer;

        this.skillDamage_Multiple = skillDamage_Multiple;
    }
    public SkillInfo(float skillRange_Multiple, float skillSpeed, float skillCooldownTimer, int skillDamage_Multiple, List<float> skillConditionEffects_Power_Multiple, List<float> skillConditionEffects_Duration_Multiple)
    {
        this.skillRange_Multiple = skillRange_Multiple;

        this.skillSpeed = skillSpeed;

        this.skillCooldownTimer = skillCooldownTimer;

        this.skillDamage_Multiple = skillDamage_Multiple;

        this.skillConditionEffects_Power_Multiple = new List<float>(skillConditionEffects_Power_Multiple);

        this.skillConditionEffects_Duration_Multiple = new List<float>(skillConditionEffects_Duration_Multiple);
    }
    public SkillInfo(SkillInfo skillInfo)
    {
        skillRange_Multiple = skillInfo.skillRange_Multiple;

        skillSpeed = skillInfo.skillSpeed;

        skillCooldownTimer = skillInfo.skillCooldownTimer;

        skillDamage_Multiple = skillInfo.skillDamage_Multiple;

        if(skillConditionEffects_Power_Multiple != null)
        {
            skillConditionEffects_Power_Multiple = new List<float>(skillInfo.skillConditionEffects_Power_Multiple);
        }
        if(skillConditionEffects_Duration_Multiple != null)
        {
            skillConditionEffects_Duration_Multiple = new List<float>(skillInfo.skillConditionEffects_Duration_Multiple);
        }
    }
}