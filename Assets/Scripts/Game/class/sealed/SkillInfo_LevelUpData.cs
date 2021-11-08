
using System.Collections.Generic;

public sealed class SkillInfo_LevelUpData
{
    private int _level = 1;

    public int level
    {
        get
        {
            return _level;
        }

        set
        {
            if (value != _level)
            {
                if (_level > 1)
                {
                    range_Multiple /= _level;

                    cooldownSpeed /= _level;

                    castingSpeed /= _level;

                    damage_Multiple /= _level;
                }

                _level = value;

                range_Multiple *= _level;

                cooldownSpeed *= _level;

                castingSpeed *= _level;

                damage_Multiple *= _level;

                if (statusEffectInfo_LevelUpDatas != null)
                {
                    var count = statusEffectInfo_LevelUpDatas.Count;

                    for (int index = 0; index < count; ++index)
                    {
                        statusEffectInfo_LevelUpDatas[index].level = _level;
                    }
                }
            }
        }
    }

    public float range_Multiple { get; set; }

    public float cooldownSpeed { get; set; }

    public float castingSpeed { get; set; }

    public float damage_Multiple { get; set; }

    public List<StatusEffectInfo_LevelUpData> statusEffectInfo_LevelUpDatas { get; private set; } = null;

    public SkillInfo_LevelUpData(float range_Multiple, float cooldownSpeed, float castingSpeed, float damage_Multiple, List<StatusEffectInfo_LevelUpData> statusEffectInfo_LevelUpDatas)
    {
        this.range_Multiple = range_Multiple;

        this.cooldownSpeed = cooldownSpeed;

        this.castingSpeed = castingSpeed;

        this.damage_Multiple = damage_Multiple;

        if (statusEffectInfo_LevelUpDatas != null)
        {
            this.statusEffectInfo_LevelUpDatas = new List<StatusEffectInfo_LevelUpData>(statusEffectInfo_LevelUpDatas);
        }
    }

    public SkillInfo_LevelUpData(SkillInfo_LevelUpData skillInfo_LevelUpData)
    {
        _level = skillInfo_LevelUpData._level;

        range_Multiple = skillInfo_LevelUpData.range_Multiple;

        cooldownSpeed = skillInfo_LevelUpData.cooldownSpeed;

        castingSpeed = skillInfo_LevelUpData.castingSpeed;

        damage_Multiple = skillInfo_LevelUpData.damage_Multiple;

        if (skillInfo_LevelUpData.statusEffectInfo_LevelUpDatas != null)
        {
            statusEffectInfo_LevelUpDatas = new List<StatusEffectInfo_LevelUpData>(skillInfo_LevelUpData.statusEffectInfo_LevelUpDatas);
        }
    }
}