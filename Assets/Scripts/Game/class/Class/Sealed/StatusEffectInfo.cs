
[System.Serializable]

public sealed class StatusEffectInfo
{
    public float power
    {
        get => power_Calculated;

        private set
        {
            power_Origin = value;

            power_Calculated = power_Origin * power_Multiple_Origin;
        }
    }

    private float power_Origin;

    private float power_Calculated;

    public float power_Multiple
    {
        get => power_Multiple_Origin;

        set
        {
            power_Multiple_Origin = value;

            power_Calculated = power_Origin * power_Multiple_Origin;
        }
    }

    private float power_Multiple_Origin;

    public float durationTime
    {
        get => durationTime_Calculated;

        private set
        {
            durationTime_Origin = value;

            durationTime_Calculated = durationTime_Origin * durationTime_Multiple_Origin;
        }
    }

    private float durationTime_Origin;

    private float durationTime_Calculated;

    public float durationTime_Multiple
    {
        get => durationTime_Multiple_Origin;

        set
        {
            durationTime_Multiple_Origin = value;

            durationTime_Calculated = durationTime_Origin * durationTime_Multiple_Origin;
        }
    }

    private float durationTime_Multiple_Origin;

    public float durationTimer { get; set; }

    public StatusEffectInfo(StatusEffectData statusEffectData)
    {
        power_Origin = statusEffectData.power;

        power_Calculated = statusEffectData.power;

        power_Multiple_Origin = 1f;

        durationTime_Origin = statusEffectData.duration;

        durationTime_Calculated = statusEffectData.duration;

        durationTime_Multiple_Origin = 1f;

        durationTimer = 0f;
    }

    public StatusEffectInfo(StatusEffectInfo statusEffectInfo)
    {
        power_Origin = statusEffectInfo.power_Origin;

        power_Calculated = statusEffectInfo.power_Calculated;

        power_Multiple_Origin = statusEffectInfo.power_Multiple_Origin;

        durationTime_Origin = statusEffectInfo.durationTime_Origin;

        durationTime_Calculated = statusEffectInfo.durationTime_Calculated;

        durationTime_Multiple_Origin = statusEffectInfo.durationTime_Multiple_Origin;

        durationTimer = statusEffectInfo.durationTimer;
    }

    public sealed class LevelUpData
    {
        public int level
        {
            get => level_Origin;

            set
            {
                level_Origin = value;

                power = power_Origin * level_Origin;

                durationTime = durationTime_Origin * level_Origin;
            }
        }

        private int level_Origin;

        public float power { get; private set; }

        private float power_Origin;

        public float durationTime { get; private set; }

        private float durationTime_Origin;

        public LevelUpData(float power, float durationTime)
        {
            level_Origin = 1;

            this.power = power;

            power_Origin = power;

            this.durationTime = durationTime;

            durationTime_Origin = durationTime;
        }

        public LevelUpData(LevelUpData levelUpData)
        {
            level_Origin = levelUpData.level_Origin;

            power = levelUpData.power;

            power_Origin = levelUpData.power_Origin;

            durationTime = levelUpData.durationTime;

            durationTime_Origin = levelUpData.durationTime_Origin;
        }
    }

    public void LevelUp(LevelUpData levelUpData)
    {
        power += levelUpData.power;

        durationTime += levelUpData.durationTime;
    }
}