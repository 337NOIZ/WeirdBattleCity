
using System.Collections.Generic;

public sealed class ExplosionInfo
{
    public ParticleEffectCode particleEffectCode { get; private set; }

    private float _range_Origin;

    private float _range_;

    public float range
    {
        get => _range_;

        private set
        {
            _range_Origin = value;

            _range_ = _range_Origin * _range_Multiple_;
        }
    }

    private float _range_Multiple_;

    public float range_Multiple
    {
        get => _range_Multiple_;

        set
        {
            _range_Multiple_ = value;

            _range_ = _range_Origin * _range_Multiple_;
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

            _damage_ = _range_Origin * _damage_Multiple_;
        }
    }

    private float _force_Origin;

    private float _force_;

    public float force
    {
        get => _force_;

        private set
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

    public List<StatusEffectInfo> statusEffectInfos { get; private set; } = null;

    public ExplosionInfo(ExplosionData explosionData)
    {
        particleEffectCode = explosionData.particleEffectCode;

        _range_Origin = explosionData.range;

        _range_ = explosionData.range;

        _range_Multiple_ = 1f;

        _damage_Origin = explosionData.damage;

        _damage_ = explosionData.damage;

        _damage_Multiple_ = 1f;

        _force_Origin = explosionData.force;

        _force_ = explosionData.force;

        _force_Multiple_ = 1f;

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

        _range_Origin = explosionInfo._range_Origin;

        _range_ = explosionInfo._range_;

        _range_Multiple_ = explosionInfo._range_Multiple_;

        _damage_Origin = explosionInfo._damage_Origin;

        _damage_ = explosionInfo._damage_;

        _damage_Multiple_ = explosionInfo._damage_Multiple_;

        _force_Origin = explosionInfo._force_Origin;

        _force_ = explosionInfo._force_;

        _force_Multiple_ = explosionInfo._force_Multiple_;

        if (explosionInfo.statusEffectInfos != null)
        {
            statusEffectInfos = new List<StatusEffectInfo>(explosionInfo.statusEffectInfos);
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

                range = _range_ * _level_;

                damage = _damage_ * _level_;

                force = _force_ * _level_;

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

        private float _range_;

        public float range { get; private set; }

        private float _damage_;

        public float damage { get; private set; }

        private float _force_;

        public float force { get; private set; }

        public List<StatusEffectInfo.LevelUpData> statusEffectInfos { get; private set; } = null;

        public LevelUpData(float range, float damage, float force, List<StatusEffectInfo.LevelUpData> statusEffectInfos)
        {
            _level_ = 1;

            _range_ = range;

            this.range = range;

            _damage_ = damage;

            this.damage = damage;

            _force_ = force;

            this.force = force;

            if (statusEffectInfos != null)
            {
                this.statusEffectInfos = new List<StatusEffectInfo.LevelUpData>(statusEffectInfos);
            }
        }

        public LevelUpData(LevelUpData levelUpData)
        {
            _level_ = levelUpData._level_;

            _range_ = levelUpData._range_;

            range = levelUpData.range;

            _damage_ = levelUpData._damage_;

            damage = levelUpData.damage;

            _force_ = levelUpData._force_;

            force = levelUpData.force;

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