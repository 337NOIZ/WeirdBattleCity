
using System.Collections.Generic;

public sealed class SkillInfo
{
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

    public float cooldownTime
    {
        get => cooldownTime_Calculated;

        private set
        {
            cooldownTime_Origin = value;

            cooldownTime_Calculated = cooldownTime_Origin / cooldownSpeed_Origin;
        }
    }

    private float cooldownTime_Origin;

    private float cooldownTime_Calculated;

    public float cooldownSpeed
    {
        get => cooldownSpeed_Origin;

        set
        {
            cooldownSpeed_Origin = value;

            cooldownTime_Calculated = cooldownTime_Origin / cooldownSpeed_Origin;
        }
    }

    private float cooldownSpeed_Origin;

    public float castingMotionTime
    {
        get => castingMotionTime_Calculated;

        private set
        {
            castingMotionTime_Origin = value;

            castingMotionTime_Calculated = castingMotionTime_Origin / castingMotionSpeed_Origin;
        }
    }

    private float castingMotionTime_Origin;

    private float castingMotionTime_Calculated;

    public float castingMotionSpeed
    {
        get => castingMotionSpeed_Origin;

        set
        {
            castingMotionSpeed_Origin = value;

            castingMotionTime_Calculated = castingMotionTime_Origin / castingMotionSpeed_Origin;
        }
    }

    private float castingMotionSpeed_Origin;

    public float skillMotionTime
    {
        get => skillMotionTime_Calculated;

        private set
        {
            skillMotionTime_Origin = value;

            skillMotionTime_Calculated = skillMotionTime_Origin / skillMotionSpeed_Origin;
        }
    }

    private float skillMotionTime_Origin;

    private float skillMotionTime_Calculated;

    public float skillMotionSpeed
    {
        get => skillMotionSpeed_Origin;

        set
        {
            skillMotionSpeed_Origin = value;

            skillMotionTime_Calculated = skillMotionTime_Origin / skillMotionSpeed_Origin;
        }
    }

    private float skillMotionSpeed_Origin;

    public float cooldownTimer { get; set; }

    public sealed class MeleeInfo
    {
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

                damage_Calculated = damage_Origin * damage_Multiple_Origin;
            }
        }

        private float damage_Multiple_Origin;

        public ExplosionInfo explosionInfo { get; private set; } = null;

        public List<StatusEffectInfo> statusEffectInfos { get; private set; } = null;

        public MeleeInfo(SkillData.MeleeData meleeData)
        {
            range_Origin = meleeData.range;

            range_Calculated = meleeData.range;

            range_Multiple_Origin = 1f;

            damage_Origin = meleeData.damage;

            damage_Calculated = meleeData.damage;

            damage_Multiple_Origin = 1f;

            if (meleeData.explosionData != null)
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
            range_Origin = meleeInfo.range_Origin;

            range_Calculated = meleeInfo.range_Calculated;

            range_Multiple_Origin = meleeInfo.range_Multiple_Origin;

            damage_Origin = meleeInfo.damage_Origin;

            damage_Calculated = meleeInfo.damage_Calculated;

            damage_Multiple_Origin = meleeInfo.damage_Multiple_Origin;

            if (meleeInfo.explosionInfo != null)
            {
                explosionInfo = new ExplosionInfo(meleeInfo.explosionInfo);
            }

            if (meleeInfo.statusEffectInfos != null)
            {
                statusEffectInfos = new List<StatusEffectInfo>(meleeInfo.statusEffectInfos);
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

            public ExplosionInfo.LevelUpData explosionInfo { get; private set; }

            public List<StatusEffectInfo.LevelUpData> statusEffectInfos { get; private set; } = null;

            public LevelUpData(float damage, ExplosionInfo.LevelUpData explosionInfo, List<StatusEffectInfo.LevelUpData> statusEffectInfos)
            {
                level_Origin = 1;

                this.damage = damage;

                damage_Origin = damage;

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

    public MeleeInfo meleeInfo { get; private set; } = null;

    public sealed class RangedInfo
    {
        public ProjectileCode projectileCode { get; private set; }

        public float division
        {
            get => division_Calculated;

            private set
            {
                division_Origin = value;

                division_Calculated = division_Origin * division_Multiple_Origin;
            }
        }

        private float division_Origin;

        private float division_Calculated;

        public float division_Multiple
        {
            get => division_Multiple_Origin;

            set
            {
                division_Multiple_Origin = value;

                division_Calculated = division_Origin * division_Multiple_Origin;
            }
        }

        private float division_Multiple_Origin;

        public float diffusion
        {
            get => diffusion_Calculated;

            private set
            {
                diffusion_Origin = value;

                diffusion_Calculated = diffusion_Origin * diffusion_Multiple_Origin;
            }
        }

        private float diffusion_Origin;

        private float diffusion_Calculated;

        public float diffusion_Multiple
        {
            get => diffusion_Multiple_Origin;

            set
            {
                diffusion_Multiple_Origin = value;

                diffusion_Calculated = diffusion_Origin * diffusion_Multiple_Origin;
            }
        }

        private float diffusion_Multiple_Origin;

        public ProjectileInfo projectileInfo { get; private set; }

        public RangedInfo(SkillData.RangedData rangedData)
        {
            projectileCode = rangedData.projectileCode;

            division_Origin = rangedData.division;

            division_Calculated = rangedData.division;

            division_Multiple_Origin = 1f;

            diffusion_Origin = rangedData.diffusion;

            diffusion_Calculated = rangedData.diffusion;

            diffusion_Multiple_Origin = 1f;

            projectileInfo = new ProjectileInfo(rangedData.projectileData);
        }

        public RangedInfo(RangedInfo rangedInfo)
        {
            projectileCode = rangedInfo.projectileCode;

            division_Origin = rangedInfo.division;

            division_Calculated = rangedInfo.division;

            division_Multiple_Origin = rangedInfo.division_Multiple_Origin;

            diffusion_Origin = rangedInfo.diffusion;

            diffusion_Calculated = rangedInfo.diffusion;

            diffusion_Multiple_Origin = rangedInfo.diffusion_Multiple_Origin;

            projectileInfo = new ProjectileInfo(rangedInfo.projectileInfo);
        }

        public sealed class LevelUpData
        {
            public int level
            {
                get => level_Origin;

                set
                {
                    level_Origin = value;

                    projectileInfo.level = level_Origin;
                }
            }

            private int level_Origin;

            public ProjectileInfo.LevelUpData projectileInfo { get; private set; }

            public LevelUpData(ProjectileInfo.LevelUpData projectileInfo)
            {
                level_Origin = 1;

                this.projectileInfo = new ProjectileInfo.LevelUpData(projectileInfo);
            }

            public LevelUpData(LevelUpData levelUpData)
            {
                level_Origin = levelUpData.level;

                projectileInfo = new ProjectileInfo.LevelUpData(levelUpData.projectileInfo);
            }
        }

        public void LevelUp(LevelUpData levelUpData)
        {
            projectileInfo.LevelUp(levelUpData.projectileInfo);
        }
    }

    public RangedInfo rangedInfo { get; private set; } = null;

    public List<StatusEffectInfo> statusEffectInfos { get; private set; } = null;

    public SkillInfo(SkillData skillData)
    {
        range_Origin = skillData.range;

        range_Calculated = skillData.range;

        range_Multiple_Origin = 1f;

        cooldownTime_Origin = skillData.cooldownTime;

        cooldownTime_Calculated = skillData.cooldownTime;

        cooldownSpeed_Origin = 1f;

        castingMotionTime_Origin = skillData.castingMotionTime;

        castingMotionTime_Calculated = skillData.castingMotionTime;

        castingMotionSpeed_Origin = 1f;

        skillMotionTime_Origin = skillData.skillMotionTime;

        skillMotionTime_Calculated = skillData.skillMotionTime;

        skillMotionSpeed_Origin = 1f;

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

        Initialize();
    }

    public SkillInfo(SkillInfo skillInfo)
    {
        range_Origin = skillInfo.range_Origin;

        range_Calculated = skillInfo.range_Calculated;

        range_Multiple_Origin = skillInfo.range_Multiple_Origin;

        cooldownTime_Origin = skillInfo.cooldownTime_Origin;

        cooldownTime_Calculated = skillInfo.cooldownTime_Calculated;

        cooldownSpeed_Origin = skillInfo.cooldownSpeed_Origin;

        castingMotionTime_Origin = skillInfo.castingMotionTime_Origin;

        castingMotionTime_Calculated = skillInfo.castingMotionTime_Calculated;

        castingMotionSpeed_Origin = skillInfo.castingMotionSpeed_Origin;

        skillMotionTime_Origin = skillInfo.skillMotionTime_Origin;

        skillMotionTime_Calculated = skillInfo.skillMotionTime_Calculated;

        skillMotionSpeed_Origin = skillInfo.skillMotionSpeed_Origin;

        if (skillInfo.meleeInfo != null)
        {
            meleeInfo = new MeleeInfo(skillInfo.meleeInfo);
        }

        if (skillInfo.rangedInfo != null)
        {
            rangedInfo = new RangedInfo(skillInfo.rangedInfo);
        }

        if (skillInfo.statusEffectInfos != null)
        {
            statusEffectInfos = new List<StatusEffectInfo>(skillInfo.statusEffectInfos);
        }

        cooldownTimer = skillInfo.cooldownTimer;
    }

    public void Initialize()
    {
        cooldownTimer = 0f;
    }

    public void SetCoolTimer()
    {
        cooldownTimer = cooldownTime_Calculated;
    }

    public sealed class LevelUpData
    {
        public int level
        {
            get => level_Origin;

            set
            {
                level_Origin = value;

                if (meleeInfo != null)
                {
                    meleeInfo.level = level_Origin;
                }

                if (rangedInfo != null)
                {
                    rangedInfo.level = level_Origin;
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

        public MeleeInfo.LevelUpData meleeInfo { get; private set; } = null;

        public RangedInfo.LevelUpData rangedInfo { get; private set; } = null;

        public List<StatusEffectInfo.LevelUpData> statusEffectInfos { get; private set; } = null;

        public LevelUpData(MeleeInfo.LevelUpData meleeInfo, RangedInfo.LevelUpData rangedInfo, List<StatusEffectInfo.LevelUpData> statusEffectInfos)
        {
            level_Origin = 1;

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
            level_Origin = levelUpData.level_Origin;

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