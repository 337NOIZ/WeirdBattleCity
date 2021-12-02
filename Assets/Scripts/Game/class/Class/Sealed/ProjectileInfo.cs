
using System.Collections.Generic;

public sealed class ProjectileInfo
{
    public float force
    {
        get => force_Calculated;

        set
        {
            force_Origin = value;

            force_Calculated = force_Origin * force_Multiple_Origin;
        }
    }

    private float force_Origin;

    private float force_Calculated;

    public float force_Multiple
    {
        get => force_Multiple_Origin;

        set
        {
            force_Multiple_Origin = value;

            force_Calculated = force_Origin * force_Multiple_Origin;
        }
    }

    private float force_Multiple_Origin;

    public float lifeTime
    {
        get => lifeTime_Calculated;

        private set
        {
            lifeTime_Origin = value;

            lifeTime_Calculated = lifeTime_Origin * lifeTime_Multiple_Origin;
        }
    }

    private float lifeTime_Origin;

    private float lifeTime_Calculated;

    public float lifeTime_Multiple
    {
        get => lifeTime_Multiple_Origin;

        set
        {
            lifeTime_Multiple_Origin = value;

            lifeTime_Calculated = lifeTime_Origin * lifeTime_Multiple_Origin;
        }
    }

    private float lifeTime_Multiple_Origin;

    public float damage
    {
        get => damage_Calculated;

        private set
        {
            damage_Origin = value;

            damage_Calculated = damage_Origin * damage_Multiple_Origin;
        }
    }

    private float damage_Origin;

    private float damage_Calculated;

    public float damage_Multiple
    {
        get => damage_Multiple_Origin;

        set
        {
            damage_Multiple_Origin = value;

            damage_Calculated = damage_Origin * damage_Multiple_Origin;
        }
    }

    private float damage_Multiple_Origin;

    public DamageableInfo damageableInfo { get; private set; } = null;

    public ExplosionInfo explosionInfo { get; private set; } = null;

    public List<StatusEffectInfo> statusEffectInfos { get; private set; } = null;

    public ProjectileInfo(ProjectileData projectileData)
    {
        force_Origin = projectileData.force;

        force_Calculated = projectileData.force;

        force_Multiple_Origin = 1f;

        lifeTime_Origin = projectileData.lifeTime;

        lifeTime_Calculated = projectileData.lifeTime;

        lifeTime_Multiple_Origin = 1f;

        damage_Origin = projectileData.damage;

        damage_Calculated = projectileData.damage;

        damage_Multiple_Origin = 1f;

        if (projectileData.damageableData != null)
        {
            damageableInfo = new DamageableInfo(projectileData.damageableData);
        }

        if (projectileData.explosionData != null)
        {
            explosionInfo = new ExplosionInfo(projectileData.explosionData);
        }

        var statusEffectDatas = projectileData.statusEffectDatas;

        if (statusEffectDatas != null)
        {
            statusEffectInfos = new List<StatusEffectInfo>();

            int count = statusEffectDatas.Count;

            for (int index = 0; index < count; ++index)
            {
                statusEffectInfos.Add(new StatusEffectInfo(statusEffectDatas[index]));
            }
        }
    }

    public ProjectileInfo(ProjectileInfo projectileInfo)
    {
        force_Origin = projectileInfo.force_Origin;

        force_Calculated = projectileInfo.force_Calculated;

        force_Multiple_Origin = projectileInfo.force_Multiple_Origin;

        lifeTime_Origin = projectileInfo.lifeTime_Origin;

        lifeTime_Calculated = projectileInfo.lifeTime_Calculated;

        lifeTime_Multiple_Origin = projectileInfo.lifeTime_Multiple_Origin;

        damage_Origin = projectileInfo.damage_Origin;

        damage_Calculated = projectileInfo.damage_Calculated;

        damage_Multiple_Origin = projectileInfo.damage_Multiple_Origin;

        if (projectileInfo.explosionInfo != null)
        {
            explosionInfo = new ExplosionInfo(projectileInfo.explosionInfo);
        }

        if (projectileInfo.statusEffectInfos != null)
        {
            statusEffectInfos = new List<StatusEffectInfo>(projectileInfo.statusEffectInfos);
        }
    }

    public sealed class LevelUpData
    {
        public int level
        {
            get => level_Origin;

            set
            {
                level_Origin = value;

                damage = damage_Origin * level_Origin;

                if (damageableInfo != null)
                {
                    damageableInfo.level = level_Origin;
                }

                if (explosionInfo != null)
                {
                    explosionInfo.level = level_Origin;
                }

                if (statusEffectInfos != null)
                {
                    int count = statusEffectInfos.Count;

                    for (int index = 0; index < count; ++index)
                    {
                        statusEffectInfos[index].level = level_Origin;
                    }
                }
            }
        }

        private int level_Origin;

        public float damage { get; private set; }

        private float damage_Origin;

        public DamageableInfo.LevelUpData damageableInfo { get; private set; } = null;

        public ExplosionInfo.LevelUpData explosionInfo { get; private set; } = null;

        public List<StatusEffectInfo.LevelUpData> statusEffectInfos { get; private set; } = null;

        public LevelUpData(float damage, DamageableInfo.LevelUpData damageableInfo, ExplosionInfo.LevelUpData explosionInfo, List<StatusEffectInfo.LevelUpData> statusEffectInfos)
        {
            level_Origin = 1;

            this.damage = damage;

            damage_Origin = damage;

            if (damageableInfo != null)
            {
                this.damageableInfo = new DamageableInfo.LevelUpData(damageableInfo);
            }

            if (explosionInfo != null)
            {
                this.explosionInfo = new ExplosionInfo.LevelUpData(explosionInfo);
            }

            if (statusEffectInfos != null)
            {
                this.statusEffectInfos = new List<StatusEffectInfo.LevelUpData>(statusEffectInfos);
            }
        }

        public LevelUpData(LevelUpData levelUpData)
        {
            level_Origin = levelUpData.level_Origin;

            damage = levelUpData.damage;

            damage_Origin = levelUpData.damage_Origin;

            if (levelUpData.damageableInfo != null)
            {
                damageableInfo = new DamageableInfo.LevelUpData(levelUpData.damageableInfo);
            }

            if (levelUpData.explosionInfo != null)
            {
                explosionInfo = new ExplosionInfo.LevelUpData(levelUpData.explosionInfo);
            }

            if (levelUpData.statusEffectInfos != null)
            {
                statusEffectInfos = new List<StatusEffectInfo.LevelUpData>(levelUpData.statusEffectInfos);
            }
        }
    }

    public void LevelUp(LevelUpData levelUpData)
    {
        damage += levelUpData.damage;

        if (levelUpData.explosionInfo != null)
        {
            explosionInfo.LevelUp(levelUpData.explosionInfo);
        }

        var statusEffectInfos = levelUpData.statusEffectInfos;

        if (statusEffectInfos != null)
        {
            int count = statusEffectInfos.Count;

            for (int index = 0; index < count; ++index)
            {
                this.statusEffectInfos[index].LevelUp(statusEffectInfos[index]);
            }
        }
    }
}