
using System.Collections.Generic;

public sealed class SkillInfo
{
    public float range { get; private set; }

    public float cooldownTime { get; private set; }

    public float castingMotionTime { get; private set; }

    public float skillMotionTime { get; private set; }

    private float _range_Multiple = 1;

    public float range_Multiple
    {
        get { return _range_Multiple; }

        set
        {
            if (value > 0f)
            {
                range /= _range_Multiple;

                _range_Multiple = value;

                range *= _range_Multiple;
            }
        }
    }

    private float _cooldownSpeed = 1;

    public float cooldownSpeed
    {
        get { return _cooldownSpeed; }

        set
        {
            if (value > 0f)
            {
                cooldownTime *= _cooldownSpeed;

                _cooldownSpeed = value;

                cooldownTime /= _cooldownSpeed;
            }
        }
    }

    private float _castingSpeed = 1;

    public float castingSpeed
    {
        get { return _castingSpeed; }

        set
        {
            if (value > 0f)
            {
                castingMotionTime *= _castingSpeed;

                _castingSpeed = value;

                castingMotionTime /= _castingSpeed;
            }
        }
    }

    private float _motionSpeed = 1;

    public float motionSpeed
    {
        get { return _motionSpeed; }

        set
        {
            if (value > 0f)
            {
                skillMotionTime *= _motionSpeed;

                _motionSpeed = value;

                skillMotionTime /= _motionSpeed;
            }
        }
    }

    public float cooldownTimer { get; set; } = 0f;

    public sealed class MeleeInfo
    {
        public float range { get; private set; }

        public float damage { get; private set; }

        private float _range_Extra = 1;

        public float range_Extra
        {
            get { return _range_Extra; }

            set
            {
                range /= _range_Extra;

                _range_Extra = value;

                _range_Multiple *= _range_Extra;
            }
        }

        private float _range_Multiple = 1;

        public float range_Multiple
        {
            get { return _range_Multiple; }

            set
            {
                range /= _range_Multiple;

                _range_Multiple = value;

                _range_Multiple *= _range_Multiple;
            }
        }

        private float _damage_Extra = 1;

        public float damage_Extra
        {
            get { return _damage_Extra; }

            set
            {
                damage /= _damage_Extra;

                _damage_Extra = value;

                damage *= _damage_Extra;
            }
        }

        private float _damage_Multiple = 1;

        public float damage_Multiple
        {
            get { return _damage_Multiple; }

            set
            {
                damage /= _damage_Multiple;

                _damage_Multiple = value;

                damage *= _damage_Multiple;
            }
        }

        public ExplosionInfo explosionInfo { get; private set; } = null;

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
                        damage_Extra /= _level;

                        _level = value;

                        damage_Extra *= _level;

                        if (explosionInfo != null)
                        {
                            explosionInfo.level = _level;
                        }

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

            public float damage_Extra { get; private set; }

            public ExplosionInfo.LevelUpData explosionInfo { get; private set; }

            public List<StatusEffectInfo.LevelUpData> statusEffectInfos { get; private set; } = null;

            public LevelUpData(float damage_Extra, ExplosionInfo.LevelUpData explosionInfo, List<StatusEffectInfo.LevelUpData> statusEffectInfos)
            {
                this.damage_Extra = damage_Extra;

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
                _level = levelUpData._level;

                damage_Extra = levelUpData.damage_Extra;

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

        public MeleeInfo(SkillData.MeleeData meleeData)
        {
            range = meleeData.range;

            damage = meleeData.damage;

            if(meleeData.explosionData != null)
            {
                explosionInfo = new ExplosionInfo(meleeData.explosionData);
            }

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
            range = meleeInfo.range;

            damage = meleeInfo.damage;

            _range_Extra = meleeInfo._range_Extra;

            _range_Multiple = meleeInfo._range_Multiple;

            _damage_Extra = meleeInfo._damage_Extra;

            _damage_Multiple = meleeInfo._damage_Multiple;

            if (meleeInfo.explosionInfo != null)
            {
                explosionInfo = new ExplosionInfo(meleeInfo.explosionInfo);
            }

            if (meleeInfo.statusEffectInfos != null)
            {
                statusEffectInfos = new List<StatusEffectInfo>(meleeInfo.statusEffectInfos);
            }
        }

        public void LevelUp(LevelUpData levelUpData)
        {
            damage_Extra += levelUpData.damage_Extra;

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

    public MeleeInfo meleeInfo { get; private set; } = null;

    public sealed class RangedInfo
    {
        public ProjectileCode projectileCode { get; private set; }

        public float division { get; private set; }

        public float diffusion { get; private set; }

        public float force { get; private set; }

        public float lifeTime { get; private set; }

        public float damage { get; private set; }

        private float _division_Multiple = 1f;

        public float division_Multiple
        {
            get { return _division_Multiple; }

            set
            {
                if (value > 0f)
                {
                    division /= _diffusion_Multiple;

                    _diffusion_Multiple = value;

                    division *= _diffusion_Multiple;
                }
            }
        }

        private float _diffusion_Multiple = 1f;

        public float diffusion_Multiple
        {
            get { return _diffusion_Multiple; }

            set
            {
                if (value > 0f)
                {
                    diffusion /= _diffusion_Multiple;

                    _diffusion_Multiple = value;

                    diffusion *= _diffusion_Multiple;
                }
            }
        }

        private float _force_Multiple = 1f;

        public float force_Multiple
        {
            get { return _force_Multiple; }

            set
            {
                if (value > 0f)
                {
                    force /= _force_Multiple;

                    _force_Multiple = value;

                    force *= _force_Multiple;
                }
            }
        }

        private float _lifeTime_Multiple = 1f;

        public float lifeTime_Multiple
        {
            get { return _lifeTime_Multiple; }

            set
            {
                if (value > 0f)
                {
                    lifeTime /= _lifeTime_Multiple;

                    _lifeTime_Multiple = value;

                    lifeTime *= _lifeTime_Multiple;
                }
            }
        }

        private float _damage_Extra = 1f;

        public float damage_Extra
        {
            get { return _damage_Extra; }

            set
            {
                if (value > 0f)
                {
                    damage -= _damage_Extra;

                    _damage_Extra = value;

                    damage += _damage_Extra;
                }
            }
        }

        private float _damage_Multiple = 1f;

        public float damage_Multiple
        {
            get { return _damage_Multiple; }

            set
            {
                if (value > 0f)
                {
                    damage /= _damage_Multiple;

                    _damage_Multiple = value;

                    damage *= _damage_Multiple;
                }
            }
        }

        public ExplosionInfo explosionInfo { get; private set; } = null;

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
                        damage_Extra /= _level;

                        _level = value;

                        damage_Extra *= _level;

                        if (explosionInfo != null)
                        {
                            explosionInfo.level = _level;
                        }

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

            public float damage_Extra { get; private set; }

            public ExplosionInfo.LevelUpData explosionInfo { get; private set; }

            public List<StatusEffectInfo.LevelUpData> statusEffectInfos { get; private set; } = null;

            public LevelUpData(float damage_Extra, ExplosionInfo.LevelUpData explosionInfo, List<StatusEffectInfo.LevelUpData> statusEffectInfos)
            {
                this.damage_Extra = damage_Extra;

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
                _level = levelUpData.level;

                damage_Extra = levelUpData.damage_Extra;

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

        public RangedInfo(SkillData.RangedData rangedData)
        {
            projectileCode = rangedData.projectileCode;

            division = rangedData.division;

            diffusion = rangedData.diffusion;

            force = rangedData.force;

            lifeTime = rangedData.lifeTime;

            damage = rangedData.damage;

            if(rangedData.explosionData != null)
            {
                explosionInfo = new ExplosionInfo(rangedData.explosionData);
            }

            var statusEffectDatas = rangedData.statusEffectDatas;

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

        public RangedInfo(RangedInfo rangedInfo)
        {
            division = rangedInfo.division;

            diffusion = rangedInfo.diffusion;

            force = rangedInfo.force;

            lifeTime = rangedInfo.lifeTime;

            damage = rangedInfo.damage;

            _division_Multiple = rangedInfo._division_Multiple;

            _diffusion_Multiple = rangedInfo._diffusion_Multiple;

            _force_Multiple = rangedInfo._force_Multiple;

            _lifeTime_Multiple = rangedInfo._lifeTime_Multiple;

            _damage_Extra = rangedInfo._damage_Extra;

            _damage_Multiple = rangedInfo._damage_Multiple;

            if (rangedInfo.explosionInfo != null)
            {
                explosionInfo = new ExplosionInfo(rangedInfo.explosionInfo);
            }

            if (rangedInfo.statusEffectInfos != null)
            {
                statusEffectInfos = new List<StatusEffectInfo>(rangedInfo.statusEffectInfos);
            }
        }

        public void LevelUp(LevelUpData levelUpData)
        {
            damage_Extra += levelUpData.damage_Extra;

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

    public RangedInfo rangedInfo { get; private set; } = null;

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
                    _level = value;

                    if (meleeInfo != null)
                    {
                        meleeInfo.level = _level;
                    }

                    if (rangedInfo != null)
                    {
                        rangedInfo.level = _level;
                    }

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

        public MeleeInfo.LevelUpData meleeInfo { get; private set; } = null;

        public RangedInfo.LevelUpData rangedInfo { get; private set; } = null;

        public List<StatusEffectInfo.LevelUpData> statusEffectInfos { get; private set; } = null;

        public LevelUpData(MeleeInfo.LevelUpData meleeInfo, RangedInfo.LevelUpData rangedInfo, List<StatusEffectInfo.LevelUpData> statusEffectInfos)
        {
            if (meleeInfo != null)
            {
                this.meleeInfo = new MeleeInfo.LevelUpData(meleeInfo);
            }

            if (rangedInfo != null)
            {
                this.rangedInfo = new RangedInfo.LevelUpData(rangedInfo);
            }

            if (statusEffectInfos != null)
            {
                this.statusEffectInfos = new List<StatusEffectInfo.LevelUpData>(statusEffectInfos);
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

            if (levelUpData.statusEffectInfos != null)
            {
                statusEffectInfos = new List<StatusEffectInfo.LevelUpData>(levelUpData.statusEffectInfos);
            }
        }
    }

    public SkillInfo(SkillData skillData)
    {
        range = skillData.range;

        cooldownTime = skillData.cooldownTime;

        castingMotionTime = skillData.castingMotionTime;

        skillMotionTime = skillData.skillMotionTime;

        if (skillData.meleeData != null)
        {
            meleeInfo = new MeleeInfo(skillData.meleeData);
        }

        if (skillData.rangedData != null)
        {
            rangedInfo = new RangedInfo(skillData.rangedData);
        }

        var statusEffectDatas = skillData.statusEffectDatas;

        if (statusEffectDatas != null)
        {
            int count = statusEffectDatas.Count;

            for(int index = 0; index < count; ++index)
            {
                statusEffectInfos[index] = new StatusEffectInfo(statusEffectDatas[index]);
            }
        }
    }

    public SkillInfo(MeleeInfo meleeInfo, RangedInfo rangedInfo, List<StatusEffectInfo> statusEffectInfos)
    {
        if (meleeInfo != null)
        {
            this.meleeInfo = new MeleeInfo(meleeInfo);
        }

        if (rangedInfo != null)
        {
            this.rangedInfo = new RangedInfo(rangedInfo);
        }

        if (statusEffectInfos != null)
        {
            this.statusEffectInfos = new List<StatusEffectInfo>(statusEffectInfos);
        }
    }

    public SkillInfo(SkillInfo skillData)
    {
        range_Multiple = skillData.range_Multiple;

        cooldownTimer = skillData.cooldownTimer;

        cooldownSpeed = skillData.cooldownSpeed;

        castingSpeed = skillData.castingSpeed;

        motionSpeed = skillData.motionSpeed;

        if (skillData.meleeInfo != null)
        {
            meleeInfo = new MeleeInfo(skillData.meleeInfo);
        }

        if (skillData.rangedInfo != null)
        {
            rangedInfo = new RangedInfo(skillData.rangedInfo);
        }

        if (skillData.statusEffectInfos != null)
        {
            statusEffectInfos = new List<StatusEffectInfo>(skillData.statusEffectInfos);
        }
    }

    public void Initialize()
    {
        cooldownTimer = 0f;
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

        List<StatusEffectInfo.LevelUpData> statusEffectInfos = levelUpData.statusEffectInfos;

        if (statusEffectInfos != null)
        {
            int count = statusEffectInfos.Count;

            for (int index = 0; index < count; ++index)
            {
                this.statusEffectInfos[index].LevelUp(statusEffectInfos[index]);
            }
        }
    }

    public void SetCoolTimer()
    {
        cooldownTimer = cooldownTime;
    }
}