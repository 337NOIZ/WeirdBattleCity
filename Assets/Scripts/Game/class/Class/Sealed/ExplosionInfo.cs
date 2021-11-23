using System.Collections.Generic;

public sealed class ExplosionInfo
{
    public float range { get; private set; }

    public int damage { get; private set; }

    public float force { get; private set; }

    public List<StatusEffectData> statusEffectDatas { get; private set; } = null;

    public float range_Multiple { get; private set; } = 1;

    public int damage_Multiple { get; private set; } = 1;

    public float force_Multiple { get; private set; } = 1;

    public List<StatusEffectInfo> statusEffectInfos { get; private set; } = null;

    public sealed class LevelUpData
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
                if (value > 0 && value != _level)
                {
                    range_Multiple /= _level;

                    damage_Multiple /= _level;

                    force_Multiple /= _level;

                    _level = value;

                    range_Multiple *= _level;

                    damage_Multiple *= _level;

                    force_Multiple *= _level;

                    if (statusEffectInfos != null)
                    {
                        int count = statusEffectInfos.Count;

                        for (int index = 0; index < count; ++index)
                        {
                            statusEffectInfos[index].level = _level;
                        }
                    }
                }
            }
        }

        public float range_Multiple { get; private set; }

        public int damage_Multiple { get; private set; }

        public float force_Multiple { get; private set; }

        public List<StatusEffectInfo.LevelUpData> statusEffectInfos { get; private set; } = null;

        public LevelUpData(float range_Multiple, int damage_Multiple, float force_Multiple, List<StatusEffectInfo.LevelUpData> statusEffectInfos)
        {
            this.range_Multiple = range_Multiple;

            this.damage_Multiple = damage_Multiple;

            this.force_Multiple = force_Multiple;

            if (statusEffectInfos != null)
            {
                this.statusEffectInfos = new List<StatusEffectInfo.LevelUpData>(statusEffectInfos);
            }
        }

        public LevelUpData(LevelUpData levelUpData)
        {
            _level = levelUpData._level;

            range_Multiple = levelUpData.range_Multiple;

            damage_Multiple = levelUpData.damage_Multiple;

            force_Multiple = levelUpData.force_Multiple;

            if (levelUpData.statusEffectInfos != null)
            {
                statusEffectInfos = new List<StatusEffectInfo.LevelUpData>(levelUpData.statusEffectInfos);
            }
        }
    }

    public ExplosionInfo(ExplosionData explosionData)
    {
        range = explosionData.range;

        damage = explosionData.damage;

        force = explosionData.force;

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
        range = explosionInfo.range;

        damage = explosionInfo.damage;

        force = explosionInfo.force;

        range_Multiple = explosionInfo.range_Multiple;

        damage_Multiple = explosionInfo.damage_Multiple;

        force_Multiple = explosionInfo.force_Multiple;

        if (explosionInfo.statusEffectInfos != null)
        {
            statusEffectInfos = new List<StatusEffectInfo>(explosionInfo.statusEffectInfos);
        }
    }

    public void LevelUp(LevelUpData levelUpData)
    {
        range_Multiple += levelUpData.range_Multiple;

        damage_Multiple += levelUpData.damage_Multiple;

        force_Multiple += levelUpData.force_Multiple;

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