using System.Collections.Generic;

public sealed class ProjectileInfo
{
    private float _force_Origin;

    private float _force;

    public float force
    {
        get => _force;

        set
        {
            _force_Origin = value;

            _force = _force_Origin * _force_Multiple;
        }
    }

    private float _force_Multiple;

    public float force_Multiple
    {
        get => _force_Multiple;

        set
        {
            _force_Multiple = value;

            _force = _force_Origin * _force_Multiple;
        }
    }

    private float _lifeTime_Origin;

    private float _lifeTime;

    public float lifeTime
    {
        get => _lifeTime;

        private set
        {
            _lifeTime_Origin = value;

            _lifeTime = _lifeTime_Origin * _lifeTime_Multiple;
        }
    }

    private float _lifeTime_Multiple;

    public float lifeTime_Multiple
    {
        get => _lifeTime_Multiple;

        set
        {
            _lifeTime_Multiple = value;

            _lifeTime = _lifeTime_Origin * _lifeTime_Multiple;
        }
    }

    private float _damage_Origin;

    private float _damage;

    public float damage
    {
        get => _damage;

        private set
        {
            _damage_Origin = value;

            _damage = _damage_Origin * _damage_Multiple;
        }
    }

    private float _damage_Multiple;

    public float damage_Multiple
    {
        get => _damage_Multiple;

        set
        {
            _damage_Multiple = value;

            _damage = _damage_Origin * _damage_Multiple;
        }
    }

    public ExplosionInfo explosionInfo { get; private set; } = null;

    public List<StatusEffectInfo> statusEffectInfos { get; private set; } = null;

    public ProjectileInfo(ProjectileData projectileData)
    {
        _force_Origin = projectileData.force;

        _force = projectileData.force;

        _force_Multiple = 1f;

        _lifeTime_Origin = projectileData.lifeTime;

        _lifeTime = projectileData.lifeTime;

        _lifeTime_Multiple = 1f;

        _damage_Origin = projectileData.damage;

        _damage = projectileData.damage;

        _damage_Multiple = 1f;

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

        _force = projectileInfo._force;

        _force_Multiple = projectileInfo._force_Multiple;

        _lifeTime_Origin = projectileInfo._lifeTime_Origin;

        _lifeTime = projectileInfo._lifeTime;

        _lifeTime_Multiple = projectileInfo._lifeTime_Multiple;

        _damage_Origin = projectileInfo._damage_Origin;

        _damage = projectileInfo._damage;

        _damage_Multiple = projectileInfo._damage_Multiple;

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
        private int _level;

        public int level
        {
            get => _level;

            set
            {
                _level = value;

                damage = _damage * _level;

                if (explosionInfo != null)
                {
                    explosionInfo.level = _level;
                }
            }
        }

        private float _damage;

        public float damage { get; private set; }

        public ExplosionInfo.LevelUpData explosionInfo { get; private set; } = null;

        public LevelUpData(float damage, ExplosionInfo.LevelUpData explosionInfo)
        {
            _level = 1;

            _damage = damage;

            this.damage = damage;

            if (explosionInfo != null)
            {
                this.explosionInfo = new ExplosionInfo.LevelUpData(explosionInfo);
            }
        }

        public LevelUpData(LevelUpData levelUpData)
        {
            _level = levelUpData._level;

            _damage = levelUpData._damage;

            damage = levelUpData.damage;

            if (levelUpData.explosionInfo != null)
            {
                explosionInfo = new ExplosionInfo.LevelUpData(levelUpData.explosionInfo);
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
    }
}