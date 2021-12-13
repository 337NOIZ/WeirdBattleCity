
using System.Collections.Generic;

public sealed class SkillInfo
{
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

    private float _cooldownTime_Origin;

    private float _cooldownTime_;

    public float cooldownTime
    {
        get => _cooldownTime_;

        private set
        {
            _cooldownTime_Origin = value;

            _cooldownTime_ = _cooldownTime_Origin / _cooldownSpeed_;
        }
    }

    private float _cooldownSpeed_;

    public float cooldownSpeed
    {
        get => _cooldownSpeed_;

        set
        {
            _cooldownSpeed_ = value;

            _cooldownTime_ = _cooldownTime_Origin / _cooldownSpeed_;
        }
    }

    public int castingMotionNumber { get; private set; }

    public float castingMotionTime { get; private set; }

    public float castingMotionSpeed { get; private set; }

    public float castingMotionLoopTime { get; private set; }

    public int skillMotionNumber { get; private set; }

    public float skillMotionTime { get; private set; }

    public float skillMotionSpeed { get; private set; }

    public float skillMotionLoopTime { get; private set; }

    public bool movable { get; private set; }

    public List<StatusEffectInfo> statusEffectInfos { get; private set; } = null;

    public sealed class MeleeInfo
    {
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

        public List<StatusEffectInfo> statusEffectInfos { get; private set; } = null;

        public MeleeInfo(SkillData.MeleeData meleeData)
        {
            _damage_Origin = meleeData.damage;

            _damage_ = meleeData.damage;

            _damage_Multiple_ = 1f;

            var statusEffectDatas = meleeData.statusEffectDatas;

            if (statusEffectDatas != null)
            {
                int count = statusEffectDatas.Count;

                for (int index = 0; index < count; ++index)
                {
                    statusEffectInfos[index] = new StatusEffectInfo(statusEffectDatas[index]);
                }
            }
        }

        public MeleeInfo(MeleeInfo meleeInfo)
        {
            _damage_Origin = meleeInfo._damage_Origin;

            _damage_ = meleeInfo._damage_;

            _damage_Multiple_ = meleeInfo._damage_Multiple_;

            if (meleeInfo.statusEffectInfos != null)
            {
                statusEffectInfos = new List<StatusEffectInfo>(meleeInfo.statusEffectInfos);
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
                }
            }

            private float _damage_;

            public float damage { get; private set; }

            public LevelUpData(float damage)
            {
                _level_ = 1;

                _damage_ = damage;

                this.damage = damage;
            }

            public LevelUpData(LevelUpData levelUpData)
            {
                _level_ = levelUpData._level_;

                _damage_ = levelUpData._damage_;

                damage = levelUpData.damage;
            }
        }

        public void LevelUp(LevelUpData levelUpData)
        {
            damage += levelUpData.damage;
        }
    }

    public MeleeInfo meleeInfo { get; private set; } = null;

    public sealed class RangedInfo
    {
        public ProjectileCode projectileCode { get; private set; }

        private float _division_Origin;

        private float _division_;

        public float division
        {
            get => _division_;

            private set
            {
                _division_Origin = value;

                _division_ = _division_Origin * _division_Multiple_;
            }
        }

        private float _division_Multiple_;

        public float division_Multiple
        {
            get => _division_Multiple_;

            set
            {
                _division_Multiple_ = value;

                _division_ = _division_Origin * _division_Multiple_;
            }
        }

        private float _diffusion_Origin;

        private float _diffusion_;

        public float diffusion
        {
            get => _diffusion_;

            private set
            {
                _diffusion_Origin = value;

                _diffusion_ = _diffusion_Origin * _diffusion_Multiple_;
            }
        }

        private float _diffusion_Multiple_;

        public float diffusion_Multiple
        {
            get => _diffusion_Multiple_;

            set
            {
                _diffusion_Multiple_ = value;

                _diffusion_ = _diffusion_Origin * _diffusion_Multiple_;
            }
        }

        public ProjectileInfo projectileInfo { get; private set; }

        public RangedInfo(SkillData.RangedData rangedData)
        {
            projectileCode = rangedData.projectileCode;

            _division_Origin = rangedData.division;

            _division_ = rangedData.division;

            _division_Multiple_ = 1f;

            _diffusion_Origin = rangedData.diffusion;

            _diffusion_ = rangedData.diffusion;

            _diffusion_Multiple_ = 1f;

            projectileInfo = new ProjectileInfo(rangedData.projectileData);
        }

        public RangedInfo(RangedInfo rangedInfo)
        {
            projectileCode = rangedInfo.projectileCode;

            _division_Origin = rangedInfo.division;

            _division_ = rangedInfo.division;

            _division_Multiple_ = rangedInfo._division_Multiple_;

            _diffusion_Origin = rangedInfo.diffusion;

            _diffusion_ = rangedInfo.diffusion;

            _diffusion_Multiple_ = rangedInfo._diffusion_Multiple_;

            projectileInfo = new ProjectileInfo(rangedInfo.projectileInfo);
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

                    projectileInfo.level = _level_;
                }
            }

            public ProjectileInfo.LevelUpData projectileInfo { get; private set; }

            public LevelUpData(ProjectileInfo.LevelUpData projectileInfo)
            {
                _level_ = 1;

                this.projectileInfo = new ProjectileInfo.LevelUpData(projectileInfo);
            }

            public LevelUpData(LevelUpData levelUpData)
            {
                _level_ = levelUpData.level;

                projectileInfo = new ProjectileInfo.LevelUpData(levelUpData.projectileInfo);
            }
        }

        public void LevelUp(LevelUpData levelUpData)
        {
            projectileInfo.LevelUp(levelUpData.projectileInfo);
        }
    }

    public RangedInfo rangedInfo { get; private set; } = null;

    public float cooldownTimer { get; set; }

    public SkillInfo(SkillData skillData)
    {
        _range_Origin = skillData.range;

        _range_ = skillData.range;

        _range_Multiple_ = 1f;

        _cooldownTime_Origin = skillData.cooldownTime;

        _cooldownTime_ = skillData.cooldownTime;

        _cooldownSpeed_ = 1f;

        castingMotionNumber = skillData.castingMotionNumber;

        castingMotionTime = skillData.castingMotionTime;

        castingMotionSpeed = skillData.castingMotionSpeed;

        castingMotionLoopTime = skillData.castingMotionLoopTime;

        skillMotionNumber = skillData.skillMotionNumber;

        skillMotionTime = skillData.skillMotionTime;

        skillMotionSpeed = skillData.castingMotionSpeed;

        skillMotionLoopTime = skillData.skillMotionLoopTime;

        var statusEffectDatas = skillData.statusEffectDatas;

        if (statusEffectDatas != null)
        {
            statusEffectInfos = new List<StatusEffectInfo>();

            int index_Max = statusEffectDatas.Count;

            for (int index = 0; index < index_Max; ++index)
            {
                statusEffectInfos.Add(new StatusEffectInfo(statusEffectDatas[index]));
            }
        }

        if (skillData.meleeData != null)
        {
            meleeInfo = new MeleeInfo(skillData.meleeData);
        }

        if (skillData.rangedData != null)
        {
            rangedInfo = new RangedInfo(skillData.rangedData);
        }

        Initialize();
    }

    public SkillInfo(SkillInfo skillInfo)
    {
        _range_Origin = skillInfo._range_Origin;

        _range_ = skillInfo._range_;

        _range_Multiple_ = skillInfo._range_Multiple_;

        _cooldownTime_Origin = skillInfo._cooldownTime_Origin;

        _cooldownTime_ = skillInfo._cooldownTime_;

        _cooldownSpeed_ = skillInfo._cooldownSpeed_;

        castingMotionNumber = skillInfo.castingMotionNumber;

        castingMotionTime = skillInfo.castingMotionTime;

        castingMotionSpeed = skillInfo.castingMotionSpeed;

        skillMotionNumber = skillInfo.skillMotionNumber;

        skillMotionTime = skillInfo.skillMotionTime;

        skillMotionSpeed = skillInfo.skillMotionSpeed;

        skillMotionLoopTime = skillInfo.skillMotionLoopTime;

        if (skillInfo.statusEffectInfos != null)
        {
            statusEffectInfos = new List<StatusEffectInfo>(skillInfo.statusEffectInfos);
        }

        if (skillInfo.meleeInfo != null)
        {
            meleeInfo = new MeleeInfo(skillInfo.meleeInfo);
        }

        if (skillInfo.rangedInfo != null)
        {
            rangedInfo = new RangedInfo(skillInfo.rangedInfo);
        }

        cooldownTimer = skillInfo.cooldownTimer;
    }

    public void Initialize()
    {
        cooldownTimer = 0f;
    }

    public void SetCoolTimer()
    {
        cooldownTimer = _cooldownTime_;
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

                if (meleeInfo != null)
                {
                    meleeInfo.level = _level_;
                }

                if (rangedInfo != null)
                {
                    rangedInfo.level = _level_;
                }
            }
        }

        public MeleeInfo.LevelUpData meleeInfo { get; private set; } = null;

        public RangedInfo.LevelUpData rangedInfo { get; private set; } = null;

        public LevelUpData(MeleeInfo.LevelUpData meleeInfo, RangedInfo.LevelUpData rangedInfo)
        {
            _level_ = 1;

            if (meleeInfo != null)
            {
                this.meleeInfo = new MeleeInfo.LevelUpData(meleeInfo);
            }

            if (rangedInfo != null)
            {
                this.rangedInfo = new RangedInfo.LevelUpData(rangedInfo);
            }
        }

        public LevelUpData(LevelUpData levelUpData)
        {
            _level_ = levelUpData._level_;

            if (levelUpData.meleeInfo != null)
            {
                meleeInfo = new MeleeInfo.LevelUpData(levelUpData.meleeInfo);
            }

            if (levelUpData.rangedInfo != null)
            {
                rangedInfo = new RangedInfo.LevelUpData(levelUpData.rangedInfo);
            }
        }
    }

    public void LevelUp(LevelUpData levelUpData)
    {
        if (levelUpData.meleeInfo != null)
        {
            meleeInfo.LevelUp(levelUpData.meleeInfo);
        }

        if (levelUpData.rangedInfo != null)
        {
            rangedInfo.LevelUp(levelUpData.rangedInfo);
        }
    }
}