
using System.Collections.Generic;

public sealed class SkillData
{
    public float range { get; private set; }

    public float cooldownTime { get; private set; }

    public float castingTime { get; private set; }

    public int damage { get; private set; }

    public List<StatusEffectData> statusEffectDatas { get; private set; } = null;

    public SkillData(float range, float cooldownTime, float castingTime, int damage, List<StatusEffectData> statusEffectDatas)
    {
        this.range = range;

        this.cooldownTime = cooldownTime;

        this.castingTime = castingTime;

        this.damage = damage;

        if (statusEffectDatas != null)
        {
            this.statusEffectDatas = new List<StatusEffectData>(statusEffectDatas);
        }
    }

    public SkillData(SkillData skillData)
    {
        range = skillData.range;

        cooldownTime = skillData.cooldownTime;

        castingTime = skillData.castingTime;

        damage = skillData.damage;

        if (skillData.statusEffectDatas != null)
        {
            statusEffectDatas = new List<StatusEffectData>(skillData.statusEffectDatas);
        }
    }
}