using System.Collections.Generic;

public sealed class ExplosionInfo
{
    private float _range_Origin;

    private float _range;

    public float range
    {
        get => _range;

        private set
        {
            _range_Origin = value;

            _range = _range_Origin * _range_Multiple;
        }
    }

    private float _range_Multiple;

    public float range_Multiple
    {
        get => _range_Multiple;

        set
        {
            _range_Multiple = value;

            _range = _range_Origin * _range_Multiple;
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

            _damage = _range_Origin * _damage_Multiple;
        }
    }

    private float _force_Origin;

    private float _force;

    public float force
    {
        get => _force;

        private set
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

    public List<StatusEffectInfo> statusEffectInfos { get; private set; } = null;

    public ExplosionInfo(ExplosionData explosionData)
    {
        _range_Origin = explosionData.range;

        _range = explosionData.range;

        _range_Multiple = 1f;

        _damage_Origin = explosionData.damage;

        _damage = explosionData.damage;

        _damage_Multiple = 1f;

        _force_Origin = explosionData.force;

        _force = explosionData.force;

        _force_Multiple = 1f;

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
        _range_Origin = explosionInfo._range_Origin;

        _range = explosionInfo._range;

        _range_Multiple = explosionInfo._range_Multiple;

        _damage_Origin = explosionInfo._damage_Origin;

        _damage = explosionInfo._damage;

        _damage_Multiple = explosionInfo._damage_Multiple;

        _force_Origin = explosionInfo._force_Origin;

        _force = explosionInfo._force;

        _force_Multiple = explosionInfo._force_Multiple;

        if (explosionInfo.statusEffectInfos != null)
        {
            statusEffectInfos = new List<StatusEffectInfo>(explosionInfo.statusEffectInfos);
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

                range = _range * _level;

                damage = _damage * _level;

                force = _force * _level;
            }
        }

        private float _range;

        public float range { get; private set; }

        private float _damage;

        public float damage { get; private set; }

        private float _force;

        public float force { get; private set; }

        public LevelUpData(float range, float damage, float force)
        {
            _level = 1;

            _range = range;

            this.range = range;

            _damage = damage;

            this.damage = damage;

            _force = force;

            this.force = force;
        }

        public LevelUpData(LevelUpData levelUpData)
        {
            _level = levelUpData._level;

            _range = levelUpData._range;

            range = levelUpData.range;

            _damage = levelUpData._damage;

            damage = levelUpData.damage;

            _force = levelUpData._force;

            force = levelUpData.force;
        }
    }

    public void LevelUp(LevelUpData levelUpData)
    {
        range += levelUpData.range;

        damage += levelUpData.damage;

        force += levelUpData.force;
    }
}