
using System.Collections.Generic;

public sealed class SkillInfo
{
    public float range_Multiple { get; set; } = 1f;

    public float cooldownTimer { get; set; } = 0f;

    public float cooldownSpeed { get; set; } = 1f;

    public float castingSpeed { get; set; } = 1f;

    public float damage_Multiple { get; set; } = 1f;

    public List<StatusEffectInfo> statusEffectInfos { get; private set; } = null;

    public SkillInfo() { }

    public SkillInfo(List<StatusEffectInfo> statusEffectInfos)
    {
        this.statusEffectInfos = new List<StatusEffectInfo>(statusEffectInfos);
    }

    public SkillInfo(float range_Multiple, float cooldownSpeed, float castingSpeed, float damage_Multiple, List<StatusEffectInfo> statusEffectInfos)
    {
        this.range_Multiple = range_Multiple;

        this.cooldownSpeed = cooldownSpeed;

        this.castingSpeed = castingSpeed;

        this.damage_Multiple = damage_Multiple;

        if(statusEffectInfos != null)
        {
            this.statusEffectInfos = new List<StatusEffectInfo>(statusEffectInfos);
        }
    }

    public SkillInfo(SkillInfo skillInfo)
    {
        range_Multiple = skillInfo.range_Multiple;

        cooldownTimer = skillInfo.cooldownTimer;

        cooldownSpeed = skillInfo.cooldownSpeed;

        castingSpeed = skillInfo.castingSpeed;

        damage_Multiple = skillInfo.damage_Multiple;

        if (skillInfo.statusEffectInfos != null)
        {
            statusEffectInfos = new List<StatusEffectInfo>(skillInfo.statusEffectInfos);
        }
    }

    public void Recycling()
    {
        cooldownTimer = 0f;
    }

    public void LevelUp(SkillInfo_LevelUpData skillInfo_LevelUpData)
    {
        range_Multiple += skillInfo_LevelUpData.range_Multiple;

        cooldownSpeed += skillInfo_LevelUpData.cooldownSpeed;

        castingSpeed += skillInfo_LevelUpData.castingSpeed;

        damage_Multiple += skillInfo_LevelUpData.damage_Multiple;

        if (skillInfo_LevelUpData.statusEffectInfo_LevelUpDatas != null)
        {
            List<StatusEffectInfo_LevelUpData> statusEffectInfo_LevelUpDatas = skillInfo_LevelUpData.statusEffectInfo_LevelUpDatas;

            var count = statusEffectInfo_LevelUpDatas.Count;

            for (int index = 0; index < count; ++index)
            {
                statusEffectInfos[index].LevelUp(statusEffectInfo_LevelUpDatas[index]);
            }
        }
    }
}