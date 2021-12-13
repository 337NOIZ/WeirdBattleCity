
using System.Collections.Generic;

public sealed class SkillData
{
    public float range { get; private set; }

    public float cooldownTime { get; private set; }

    public int castingMotionNumber { get; private set; }

    public float castingMotionTime_Origin { get; private set; }

    public float castingMotionTime { get; private set; }

    public float castingMotionSpeed { get; private set; }

    public float castingMotionLoopTime { get; private set; }

    public int skillMotionNumber { get; private set; }

    public float skillMotionTime_Origin { get; private set; }

    public float skillMotionTime { get; private set; }

    public float skillMotionSpeed { get; private set; }

    public float skillMotionLoopTime { get; private set; }

    public List<StatusEffectData> statusEffectDatas { get; private set; } = null;

    public sealed class MeleeData
    {
        public float damage { get; private set; }

        public List<StatusEffectData> statusEffectDatas { get; private set; } = null;

        public MeleeData(float damage, List<StatusEffectData> statusEffectDatas)
        {
            this.damage = damage;

            if (statusEffectDatas != null)
            {
                this.statusEffectDatas = new List<StatusEffectData>(statusEffectDatas);
            }
        }

        public MeleeData(MeleeData meleeData)
        {
            damage = meleeData.damage;

            if (meleeData.statusEffectDatas != null)
            {
                statusEffectDatas = new List<StatusEffectData>(meleeData.statusEffectDatas);
            }
        }
    }

    public MeleeData meleeData { get; private set; } = null;

    public sealed class RangedData
    {
        public ProjectileCode projectileCode { get; private set; }

        public float division { get; private set; }

        public float diffusion { get; private set; }

        public ProjectileData projectileData { get; private set; }
        
        public RangedData(ProjectileCode projectileCode, float division, float diffusion, ProjectileData projectileData)
        {
            this.projectileCode = projectileCode;

            this.division = division;

            this.diffusion = diffusion;

            this.projectileData = new ProjectileData(projectileData);
        }

        public RangedData(RangedData rangedData)
        {
            projectileCode = rangedData.projectileCode;

            division = rangedData.division;

            diffusion = rangedData.diffusion;

            projectileData = new ProjectileData(rangedData.projectileData);
        }
    }

    public RangedData rangedData { get; private set; } = null;

    public SkillData(float range, float cooldownTime, int castingMotionNumber, float castingMotionTime_Origin, float castingMotionTime, float castingMotionLoopTime, int skillMotionNumber, float skillMotionTime_Origin, float skillMotionTime, float skillMotionLoopTime, List<StatusEffectData> statusEffectDatas, MeleeData meleeData, RangedData rangedData)
    {
        this.range = range;

        this.cooldownTime = cooldownTime;

        this.castingMotionNumber = castingMotionNumber;

        this.castingMotionTime_Origin = castingMotionTime_Origin;

        if(castingMotionTime > 0f)
        {
            this.castingMotionTime = castingMotionTime;

            castingMotionSpeed = castingMotionTime_Origin / castingMotionTime;
        }

        else
        {
            this.castingMotionTime = castingMotionTime_Origin;

            castingMotionSpeed = 1f;
        }

        this.castingMotionLoopTime = castingMotionLoopTime;

        this.skillMotionNumber = skillMotionNumber;

        this.skillMotionTime_Origin = skillMotionTime_Origin;

        if(skillMotionTime > 0f)
        {
            this.skillMotionTime = skillMotionTime;

            skillMotionSpeed = skillMotionTime_Origin / skillMotionTime;
        }

        else
        {
            this.skillMotionTime = skillMotionTime_Origin;

            skillMotionSpeed = 0f;
        }

        this.skillMotionLoopTime = skillMotionLoopTime;

        if (statusEffectDatas != null)
        {
            this.statusEffectDatas = new List<StatusEffectData>(statusEffectDatas);
        }

        if (meleeData != null)
        {
            this.meleeData = new MeleeData(meleeData);
        }

        if (rangedData != null)
        {
            this.rangedData = new RangedData(rangedData);
        }
    }

    public SkillData(SkillData skillData)
    {
        range = skillData.range;

        cooldownTime = skillData.cooldownTime;

        castingMotionNumber = skillData.castingMotionNumber;

        castingMotionTime = skillData.castingMotionTime;

        castingMotionSpeed = skillData.skillMotionSpeed;

        castingMotionLoopTime = skillData.castingMotionLoopTime;

        skillMotionNumber = skillData.skillMotionNumber;

        skillMotionTime = skillData.skillMotionTime;

        skillMotionSpeed = skillData.skillMotionSpeed;

        skillMotionLoopTime = skillData.skillMotionLoopTime;

        if (skillData.meleeData != null)
        {
            meleeData = new MeleeData(skillData.meleeData);
        }

        if (skillData.rangedData != null)
        {
            rangedData = new RangedData(skillData.rangedData);
        }

        if (skillData.statusEffectDatas != null)
        {
            statusEffectDatas = new List<StatusEffectData>(skillData.statusEffectDatas);
        }
    }
}