
using System.Collections.Generic;

public sealed class SkillData
{
    public float range { get; private set; }

    public float cooldownTime { get; private set; }

    public float castingMotionTime { get; private set; }

    public float skillMotionTime { get; private set; }

    public sealed class MeleeData
    {
        public float range { get; private set; }

        public int damage { get; private set; }

        public ExplosionData explosionData { get; private set; }

        public List<StatusEffectData> statusEffectDatas { get; private set; } = null;

        public MeleeData(float range, int damage, ExplosionData explosionData, List<StatusEffectData> statusEffectDatas)
        {
            this.range = range;

            this.damage = damage;

            if (explosionData != null)
            {
                this.explosionData = new ExplosionData(explosionData);
            }

            if (statusEffectDatas != null)
            {
                this.statusEffectDatas = new List<StatusEffectData>(statusEffectDatas);
            }
        }

        public MeleeData(MeleeData meleeData)
        {
            range = meleeData.range;

            damage = meleeData.damage;

            if (meleeData.explosionData != null)
            {
                explosionData = new ExplosionData(meleeData.explosionData);
            }

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

        public float force { get; private set; }

        public float lifeTime { get; private set; }

        public int damage { get; private set; }

        public ExplosionData explosionData { get; private set; } = null;

        public List<StatusEffectData> statusEffectDatas { get; private set; } = null;

        public RangedData(ProjectileCode projectileCode, float division, float diffusion, float force, float lifeTime, int damage, ExplosionData explosionData, List<StatusEffectData> statusEffectDatas)
        {
            this.projectileCode = projectileCode;

            this.division = division;

            this.diffusion = diffusion;

            this.force = force;

            this.lifeTime = lifeTime;

            this.damage = damage;

            if (explosionData != null)
            {
                this.explosionData = new ExplosionData(explosionData);
            }

            if (statusEffectDatas != null)
            {
                this.statusEffectDatas = new List<StatusEffectData>(statusEffectDatas);
            }
        }

        public RangedData(RangedData rangedData)
        {
            projectileCode = rangedData.projectileCode;

            division = rangedData.division;

            diffusion = rangedData.diffusion;

            force = rangedData.force;

            lifeTime = rangedData.lifeTime;

            damage = rangedData.damage;

            if (rangedData.explosionData != null)
            {
                explosionData = new ExplosionData(rangedData.explosionData);
            }

            if (rangedData.statusEffectDatas != null)
            {
                statusEffectDatas = new List<StatusEffectData>(rangedData.statusEffectDatas);
            }
        }
    }

    public RangedData rangedData { get; private set; } = null;

    public List<StatusEffectData> statusEffectDatas { get; private set; } = null;

    public SkillData(float range, float cooldownTime, float castingMotionTime, float skillMotionTime, MeleeData meleeData, RangedData rangedData, List<StatusEffectData> statusEffectDatas)
    {
        this.range = range;

        this.cooldownTime = cooldownTime;

        this.castingMotionTime = castingMotionTime;

        this.skillMotionTime = skillMotionTime;

        if (meleeData != null)
        {
            this.meleeData = new MeleeData(meleeData);
        }

        if (rangedData != null)
        {
            this.rangedData = new RangedData(rangedData);
        }

        if (statusEffectDatas != null)
        {
            this.statusEffectDatas = new List<StatusEffectData>(statusEffectDatas);
        }
    }

    public SkillData(SkillData skillData)
    {
        range = skillData.range;

        cooldownTime = skillData.cooldownTime;

        castingMotionTime = skillData.castingMotionTime;

        skillMotionTime = skillData.skillMotionTime;

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