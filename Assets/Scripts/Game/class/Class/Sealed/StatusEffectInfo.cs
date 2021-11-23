
[System.Serializable]

public sealed class StatusEffectInfo
{
    public float power { get; private set; }

    public float duration { get; private set; }

    private float _power_Multiple = 1f;

    public float power_Multiple
    {
        get { return _power_Multiple; }

        set
        {
            if (value > 0f)
            {
                power /= _power_Multiple;

                _power_Multiple = value;

                power *= _power_Multiple;
            }
        }
    }

    private float _duration_Multiple = 1f;

    public float duration_Multiple
    {
        get { return _duration_Multiple; }

        set
        {
            if (value > 0f)
            {
                duration /= _duration_Multiple;

                _duration_Multiple = value;

                duration *= _duration_Multiple;
            }
        }
    }

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
                    power_Multiple /= _level;

                    duration_Multiple /= _level;

                    _level = value;

                    power_Multiple *= _level;

                    duration_Multiple *= _level;
                }
            }
        }

        public float power_Multiple { get; set; }

        public float duration_Multiple { get; set; }

        public LevelUpData(float power_Multiple, float duration_Multiple)
        {
            this.power_Multiple = power_Multiple;

            this.duration_Multiple = duration_Multiple;
        }

        public LevelUpData(LevelUpData levelUpData)
        {
            _level = levelUpData._level;

            power_Multiple = levelUpData.power_Multiple;

            duration_Multiple = levelUpData.duration_Multiple;
        }
    }

    public StatusEffectInfo(StatusEffectData statusEffectData)
    {
        power = statusEffectData.power;

        duration = statusEffectData.duration;
    }

    public StatusEffectInfo(StatusEffectInfo statusEffectInfo)
    {
        power = statusEffectInfo.power;

        duration = statusEffectInfo.duration;

        power_Multiple = statusEffectInfo.power_Multiple;

        duration_Multiple = statusEffectInfo.duration_Multiple;
    }

    public void LevelUp(LevelUpData levelUpData)
    {
        power_Multiple += levelUpData.power_Multiple;

        duration_Multiple += levelUpData.duration_Multiple;
    }
}