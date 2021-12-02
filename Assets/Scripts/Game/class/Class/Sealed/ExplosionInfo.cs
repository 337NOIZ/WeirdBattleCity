
using System.Collections.Generic;

public sealed class ExplosionInfo
{
    public ParticleEffectCode particleEffectCode { get; private set; }

    public float range
    {
        get => range_Calculated;

        private set
        {
            range_Origin = value;

            range_Calculated = range_Origin * range_Multiple_Origin;
        }
    }

    private float range_Origin;

    private float range_Calculated;

    public float range_Multiple
    {
        get => range_Multiple_Origin;

        set
        {
            range_Multiple_Origin = value;

            range_Calculated = range_Origin * range_Multiple_Origin;
        }
    }

    private float range_Multiple_Origin;

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

            damage_Calculated = range_Origin * damage_Multiple_Origin;
        }
    }

    private float damage_Multiple_Origin;
    public float force
    {
        get => force_Calculated;

        private set
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

    public List<StatusEffectInfo> statusEffectInfos { get; private set; } = null;

    public ExplosionInfo(ExplosionData explosionData)
    {
        particleEffectCode = explosionData.particleEffectCode;

        range_Origin = explosionData.range;

        range_Calculated = explosionData.range;

        range_Multiple_Origin = 1f;

        damage_Origin = explosionData.damage;

        damage_Calculated = explosionData.damage;

        damage_Multiple_Origin = 1f;

        force_Origin = explosionData.force;

        force_Calculated = explosionData.force;

        force_Multiple_Origin = 1f;

        var statusEffectDatas = explosionData.statusEffectDatas;

        if (statusEffectDatas != null)
        {
            int count = statusEffectDatas.Count;

            for(int index = 0; index < count; ++index)
            {
                statusEffectInfos[index] = new StatusEffectInfo(statusEffectDatas[index]);
            }
        }
    }

    public ExplosionInfo(ExplosionInfo explosionInfo)
    {
        particleEffectCode = explosionInfo.particleEffectCode;

        range_Origin = explosionInfo.range_Origin;

        range_Calculated = explosionInfo.range_Calculated;

        range_Multiple_Origin = explosionInfo.range_Multiple_Origin;

        damage_Origin = explosionInfo.damage_Origin;

        damage_Calculated = explosionInfo.damage_Calculated;

        damage_Multiple_Origin = explosionInfo.damage_Multiple_Origin;

        force_Origin = explosionInfo.force_Origin;

        force_Calculated = explosionInfo.force_Calculated;

        force_Multiple_Origin = explosionInfo.force_Multiple_Origin;

        if (explosionInfo.statusEffectInfos != null)
        {
            statusEffectInfos = new List<StatusEffectInfo>(explosionInfo.statusEffectInfos);
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

                range = range_Origin * level_Origin;

                damage = damage_Origin * level_Origin;

                force = force_Origin * level_Origin;

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

        public float range { get; private set; }

        private float range_Origin;

        public float damage { get; private set; }

        private float damage_Origin;

        public float force { get; private set; }

        private float force_Origin;

        public List<StatusEffectInfo.LevelUpData> statusEffectInfos { get; private set; } = null;

        public LevelUpData(float range, float damage, float force, List<StatusEffectInfo.LevelUpData> statusEffectInfos)
        {
            level_Origin = 1;

            this.range = range;

            range_Origin = range;

            this.damage = damage;

            damage_Origin = damage;

            this.force = force;

            force_Origin = force;

            if (statusEffectInfos != null)
            {
                this.statusEffectInfos = new List<StatusEffectInfo.LevelUpData>(statusEffectInfos);
            }
        }

        public LevelUpData(LevelUpData levelUpData)
        {
            level_Origin = levelUpData.level_Origin;

            range = levelUpData.range;

            range_Origin = levelUpData.range_Origin;

            damage = levelUpData.damage;

            damage_Origin = levelUpData.damage_Origin;

            force = levelUpData.force;

            force_Origin = levelUpData.force_Origin;

            if (levelUpData.statusEffectInfos != null)
            {
                statusEffectInfos = new List<StatusEffectInfo.LevelUpData>(levelUpData.statusEffectInfos);
            }
        }
    }

    public void LevelUp(LevelUpData levelUpData)
    {
        range += levelUpData.range;

        damage += levelUpData.damage;

        force += levelUpData.force;

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