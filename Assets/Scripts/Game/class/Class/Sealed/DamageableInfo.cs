
public sealed class DamageableInfo
{
    public float healthPoint_Max
    {
        get => healthPoint_Max_Calculated;

        private set
        {
            healthPoint_Max_Origin = value;

            healthPoint_Max_Calculated = healthPoint_Max_Origin * healthPoint_Max_Multiple_Origin;
        }
    }

    private float healthPoint_Max_Origin;

    private float healthPoint_Max_Calculated;

    public float healthPoint_Max_Multiple
    {
        get => healthPoint_Max_Multiple_Origin;

        set
        {
            healthPoint_Max_Multiple_Origin = value;

            healthPoint_Max_Calculated = healthPoint_Max_Origin * healthPoint_Max_Multiple_Origin;
        }
    }

    private float healthPoint_Max_Multiple_Origin;

    public float invincibleTime
    {
        get => invincibleTime_Calculated;

        private set
        {
            invincibleTime_Origin = value;

            invincibleTime_Calculated = invincibleTime_Origin * invincibleTime_Multiple_Origin;
        }
    }

    private float invincibleTime_Origin;

    private float invincibleTime_Calculated;

    public float invincibleTime_Multiple
    {
        get => invincibleTime_Multiple_Origin;

        set
        {
            invincibleTime_Multiple_Origin = value;

            invincibleTime_Calculated = invincibleTime_Origin * invincibleTime_Multiple_Origin;
        }
    }

    private float invincibleTime_Multiple_Origin;

    public float healthPoint
    {
        get => healthPoint_Origin;

        set
        {
            if(value > healthPoint_Max_Calculated)
            {
                healthPoint_Origin = healthPoint_Max_Calculated;
                
            }

            if (value < 0f)
            {
                healthPoint_Origin = 0f;

            }

            else
            {
                healthPoint_Origin = value;
            }
        }
    }

    private float healthPoint_Origin;

    public float invincibleTimer { get; set; }

    public DamageableInfo(DamageableData damageableData)
    {
        healthPoint_Max_Origin = damageableData.healthPoint_Max;

        healthPoint_Max_Calculated = damageableData.healthPoint_Max;

        healthPoint_Max_Multiple_Origin = 1f;

        invincibleTime_Origin = damageableData.invincibleTime;

        invincibleTime_Calculated = damageableData.invincibleTime;

        invincibleTime_Multiple_Origin = 1f;

        Initialize();
    }

    public DamageableInfo(DamageableInfo damageableInfo)
    {
        healthPoint_Max_Origin = damageableInfo.healthPoint_Max_Origin;

        healthPoint_Max_Calculated = damageableInfo.healthPoint_Max_Calculated;

        healthPoint_Max_Multiple_Origin = damageableInfo.healthPoint_Max_Multiple_Origin;

        invincibleTime_Origin = damageableInfo.invincibleTime_Origin;

        invincibleTime_Calculated = damageableInfo.invincibleTime_Calculated;

        invincibleTime_Multiple_Origin = damageableInfo.invincibleTime_Multiple_Origin;

        healthPoint_Origin = damageableInfo.healthPoint_Origin;

        invincibleTimer = damageableInfo.invincibleTimer;
    }

    public void Initialize()
    {
        healthPoint_Origin = healthPoint_Max_Calculated;

        invincibleTimer = 0f;
    }

    public void SetInvincibleTimer()
    {
        invincibleTimer = invincibleTime_Calculated;
    }

    public class LevelUpData
    {
        public int level
        {
            get => level_Origin;

            set
            {
                level_Origin = value;

                healthPoint_Max = healthPoint_Max_Origin * level_Origin;
            }
        }

        private int level_Origin;

        public float healthPoint_Max { get; private set; }

        private float healthPoint_Max_Origin;

        public LevelUpData(float healthPoint_Max)
        {
            level_Origin = 1;

            this.healthPoint_Max = healthPoint_Max;

            healthPoint_Max_Origin = healthPoint_Max;
        }

        public LevelUpData(LevelUpData levelUpData)
        {
            level_Origin = levelUpData.level_Origin;

            healthPoint_Max = levelUpData.healthPoint_Max;

            healthPoint_Max_Origin = levelUpData.healthPoint_Max_Origin;
        }
    }

    public void LevelUp(LevelUpData levelUpData)
    {
        healthPoint_Max_Origin += levelUpData.healthPoint_Max;

        healthPoint_Origin += levelUpData.healthPoint_Max;
    }
}