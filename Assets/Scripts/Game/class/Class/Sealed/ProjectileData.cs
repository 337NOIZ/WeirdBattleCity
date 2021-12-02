
using System.Collections.Generic;

public sealed class ProjectileData
{
    public float force { get; private set; }

    public float lifeTime { get; private set; }

    public float damage { get; private set; }

    public DamageableData damageableData { get; private set; } = null;

    public ExplosionData explosionData { get; private set; } = null;

    public List<StatusEffectData> statusEffectDatas { get; private set; } = null;

    public ProjectileData(float force, float lifeTime, float damage, DamageableData damageableData, ExplosionData explosionData, List<StatusEffectData> statusEffectDatas)
    {
        this.force = force;

        this.lifeTime = lifeTime;

        this.damage = damage;

        if(damageableData != null)
        {
            this.damageableData = new DamageableData(damageableData);
        }

        if (explosionData != null)
        {
            this.explosionData = new ExplosionData(explosionData);
        }

        if (statusEffectDatas != null)
        {
            this.statusEffectDatas = new List<StatusEffectData>(statusEffectDatas);
        }
    }

    public ProjectileData(ProjectileData projectileData)
    {
        force = projectileData.force;

        lifeTime = projectileData.lifeTime;

        damage = projectileData.damage;

        if (projectileData.damageableData != null)
        {
            damageableData = new DamageableData(projectileData.damageableData);
        }

        if (projectileData.explosionData != null)
        {
            explosionData = new ExplosionData(projectileData.explosionData);
        }

        if (projectileData.statusEffectDatas != null)
        {
            statusEffectDatas = new List<StatusEffectData>(projectileData.statusEffectDatas);
        }
    }
}