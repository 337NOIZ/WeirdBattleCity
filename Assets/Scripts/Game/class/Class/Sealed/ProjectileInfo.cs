
using System.Collections.Generic;

public sealed class ProjectileInfo
{
    private float _force_Origin;

    private float _force_;

    public float force
    {
        get => _force_;

        set
        {
            _force_Origin = value;

            _force_ = _force_Origin * _force_Multiple_;
        }
    }

    private float _force_Multiple_;

    public float force_Multiple
    {
        get => _force_Multiple_;

        set
        {
            _force_Multiple_ = value;

            _force_ = _force_Origin * _force_Multiple_;
        }
    }

    private float _lifeTime_Origin;

    private float _lifeTime_;

    public float lifeTime
    {
        get => _lifeTime_;

        private set
        {
            _lifeTime_Origin = value;

            _lifeTime_ = _lifeTime_Origin * _lifeTime_Multiple_;
        }
    }

    private float _lifeTime_Multiple_;

    public float lifeTime_Multiple
    {
        get => _lifeTime_Multiple_;

        set
        {
            _lifeTime_Multiple_ = value;

            _lifeTime_ = _lifeTime_Origin * _lifeTime_Multiple_;
        }
    }

    private float _damage_Origin;

    private float _damage_;

    public float damage
    {
        get => _damage_;

        private set
        {
            _damage_Origin = value;

            _damage_ = _damage_Origin * _damage_Multiple_;
        }
    }

    private float _damage_Multiple_;

    public float damage_Multiple
    {
        get => _damage_Multiple_;

        set
        {
            _damage_Multiple_ = value;

            _damage_ = _damage_Origin * _damage_Multiple_;
        }
    }

    public DamageableInfo damageableInfo { get; private set; } = null;

    public ExplosionInfo explosionInfo { get; private set; } = null;

    public List<StatusEffectInfo> statusEffectInfos { get; private set; } = null;

    public ProjectileInfo(ProjectileData projectileData)
    {
        _force_Origin = projectileData.force;

        _force_ = projectileData.force;

        _force_Multiple_ = 1f;

        _lifeTime_Origin = projectileData.lifeTime;

        _lifeTime_ = projectileData.lifeTime;

        _lifeTime_Multiple_ = 1f;

        _damage_Origin = projectileData.damage;

        _damage_ = projectileData.damage;

        _damage_Multiple_ = 1f;

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
        _force_Origin = projectileInfo._force_Origin;

        _force_ = projectileInfo._force_;

        _force_Multiple_ = projectileInfo._force_Multiple_;

        _lifeTime_Origin = projectileInfo._lifeTime_Origin;

        _lifeTime_ = projectileInfo._lifeTime_;

        _lifeTime_Multiple_ = projectileInfo._lifeTime_Multiple_;

        _damage_Origin = projectileInfo._damage_Origin;

        _damage_ = projectileInfo._damage_;

        _damage_Multiple_ = projectileInfo._damage_Multiple_;

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
        private int _level_;

        public int level
        {
            get => _level_;

            set
            {
                _level_ = value;

                damage = _damage_ * _level_;

                if (damageableInfo != null)
                {
                    damageableInfo.level = _level_;
                }

                if (explosionInfo != null)
                {
                    explosionInfo.level = _level_;
                }

                if (statusEffectInfos != null)
                {
                    int count = statusEffectInfos.Count;

                    for (int index = 0; index < count; ++index)
                    {
                        statusEffectInfos[index].level = _level_;
                    }
                }
            }
        }

        private float _damage_;

        public float damage { get; private set; }

        public DamageableInfo.LevelUpData damageableInfo { get; private set; } = null;

        public ExplosionInfo.LevelUpData explosionInfo { get; private set; } = null;

        public List<StatusEffectInfo.LevelUpData> statusEffectInfos { get; private set; } = null;

        public LevelUpData(float damage, DamageableInfo.LevelUpData damageableInfo, ExplosionInfo.LevelUpData explosionInfo, List<StatusEffectInfo.LevelUpData> statusEffectInfos)
        {
            _level_ = 1;

            _damage_ = damage;

            this.damage = damage;

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
            _level_ = levelUpData._level_;

            _damage_ = levelUpData._damage_;

            damage = levelUpData.damage;

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