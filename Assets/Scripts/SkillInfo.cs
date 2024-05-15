using System.Collections.Generic;

public sealed class SkillInfo
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

    private float _cooldownTime_Origin;

    private float _cooldownTime;

    public float cooldownTime
    {
        get => _cooldownTime;

        private set
        {
            _cooldownTime_Origin = value;

            _cooldownTime = _cooldownTime_Origin / _cooldownSpeed;
        }
    }

    private float _cooldownSpeed;

    public float cooldownSpeed
    {
        get => _cooldownSpeed;

        set
        {
            _cooldownSpeed = value;

            _cooldownTime = _cooldownTime_Origin / _cooldownSpeed;
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

        public List<StatusEffectInfo> statusEffectInfos { get; private set; } = null;

        public MeleeInfo(SkillData.MeleeData meleeData)
        {
            _damage_Origin = meleeData.damage;

            _damage = meleeData.damage;

            _damage_Multiple = 1f;

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

            _damage = meleeInfo._damage;

            _damage_Multiple = meleeInfo._damage_Multiple;

            if (meleeInfo.statusEffectInfos != null)
            {
                statusEffectInfos = new List<StatusEffectInfo>(meleeInfo.statusEffectInfos);
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
                }
            }

            private float _damage;

            public float damage { get; private set; }

            public LevelUpData(float damage)
            {
                _level = 1;

                _damage = damage;

                this.damage = damage;
            }

            public LevelUpData(LevelUpData levelUpData)
            {
                _level = levelUpData._level;

                _damage = levelUpData._damage;

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

        private float _division;

        public float division
        {
            get => _division;

            private set
            {
                _division_Origin = value;

                _division = _division_Origin * _division_Multiple;
            }
        }

        private float _division_Multiple;

        public float division_Multiple
        {
            get => _division_Multiple;

            set
            {
                _division_Multiple = value;

                _division = _division_Origin * _division_Multiple;
            }
        }

        private float _diffusion_Origin;

        private float _diffusion;

        public float diffusion
        {
            get => _diffusion;

            private set
            {
                _diffusion_Origin = value;

                _diffusion = _diffusion_Origin * _diffusion_Multiple;
            }
        }

        private float _diffusion_Multiple;

        public float diffusion_Multiple
        {
            get => _diffusion_Multiple;

            set
            {
                _diffusion_Multiple = value;

                _diffusion = _diffusion_Origin * _diffusion_Multiple;
            }
        }

        public ProjectileInfo projectileInfo { get; private set; }

        public RangedInfo(SkillData.RangedData rangedData)
        {
            projectileCode = rangedData.projectileCode;

            _division_Origin = rangedData.division;

            _division = rangedData.division;

            _division_Multiple = 1f;

            _diffusion_Origin = rangedData.diffusion;

            _diffusion = rangedData.diffusion;

            _diffusion_Multiple = 1f;

            projectileInfo = new ProjectileInfo(rangedData.projectileData);
        }

        public RangedInfo(RangedInfo rangedInfo)
        {
            projectileCode = rangedInfo.projectileCode;

            _division_Origin = rangedInfo.division;

            _division = rangedInfo.division;

            _division_Multiple = rangedInfo._division_Multiple;

            _diffusion_Origin = rangedInfo.diffusion;

            _diffusion = rangedInfo.diffusion;

            _diffusion_Multiple = rangedInfo._diffusion_Multiple;

            projectileInfo = new ProjectileInfo(rangedInfo.projectileInfo);
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

                    projectileInfo.level = _level;
                }
            }

            public ProjectileInfo.LevelUpData projectileInfo { get; private set; }

            public LevelUpData(ProjectileInfo.LevelUpData projectileInfo)
            {
                _level = 1;

                this.projectileInfo = new ProjectileInfo.LevelUpData(projectileInfo);
            }

            public LevelUpData(LevelUpData levelUpData)
            {
                _level = levelUpData.level;

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

        _range = skillData.range;

        _range_Multiple = 1f;

        _cooldownTime_Origin = skillData.cooldownTime;

        _cooldownTime = skillData.cooldownTime;

        _cooldownSpeed = 1f;

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

        _range = skillInfo._range;

        _range_Multiple = skillInfo._range_Multiple;

        _cooldownTime_Origin = skillInfo._cooldownTime_Origin;

        _cooldownTime = skillInfo._cooldownTime;

        _cooldownSpeed = skillInfo._cooldownSpeed;

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
        cooldownTimer = _cooldownTime;
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

                if (meleeInfo != null)
                {
                    meleeInfo.level = _level;
                }

                if (rangedInfo != null)
                {
                    rangedInfo.level = _level;
                }
            }
        }

        public MeleeInfo.LevelUpData meleeInfo { get; private set; } = null;

        public RangedInfo.LevelUpData rangedInfo { get; private set; } = null;

        public LevelUpData(MeleeInfo.LevelUpData meleeInfo, RangedInfo.LevelUpData rangedInfo)
        {
            _level = 1;

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
            _level = levelUpData._level;

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