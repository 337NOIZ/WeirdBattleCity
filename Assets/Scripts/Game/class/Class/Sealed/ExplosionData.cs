
using System.Collections.Generic;

public sealed class ExplosionData
{
    public float range { get; private set; }

    public int damage { get; private set; }

    public float force { get; private set; }

    public List<StatusEffectData> statusEffectDatas { get; private set; } = null;

    public ExplosionData(float range, int damage, float force, List<StatusEffectData> statusEffectDatas)
    {
        this.range = range;

        this.damage = damage;

        this.force = force;

        if (statusEffectDatas != null)
        {
            this.statusEffectDatas = new List<StatusEffectData>(statusEffectDatas);
        }
    }

    public ExplosionData(ExplosionData explosionData)
    {
        range = explosionData.range;

        damage = explosionData.damage;

        force = explosionData.force;

        if (explosionData.statusEffectDatas != null)
        {
            statusEffectDatas = new List<StatusEffectData>(explosionData.statusEffectDatas);
        }
    }
}