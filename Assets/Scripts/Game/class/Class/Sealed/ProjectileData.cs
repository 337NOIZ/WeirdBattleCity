
using System.Collections.Generic;

public sealed class ProjectileData
{
    public float force { get; private set; }

    public float lifeTime { get; private set; }

    public float damage { get; private set; }

    public List<StatusEffectData> statusEffectDatas { get; private set; } = null;

    public ExplosionData explosionData { get; private set; } = null;

    public ProjectileData(float force, float lifeTime, float damage, List<StatusEffectData> statusEffectDatas, ExplosionData explosionData)
    {
        this.force = force;

        this.lifeTime = lifeTime;

        this.damage = damage;

        if (statusEffectDatas != null)
        {
            this.statusEffectDatas = new List<StatusEffectData>(statusEffectDatas);
        }

        if (explosionData != null)
        {
            this.explosionData = new ExplosionData(explosionData);
        }
    }

    public ProjectileData(ProjectileData projectileData)
    {
        force = projectileData.force;

        lifeTime = projectileData.lifeTime;

        damage = projectileData.damage;

        if (projectileData.statusEffectDatas != null)
        {
            statusEffectDatas = new List<StatusEffectData>(projectileData.statusEffectDatas);
        }

        if (projectileData.explosionData != null)
        {
            explosionData = new ExplosionData(projectileData.explosionData);
        }
    }
}