
[System.Serializable]

public sealed class StatusEffectInfo
{
    private float _power_Origin;

    private float _power_;

    public float power
    {
        get => _power_;

        private set
        {
            _power_Origin = value;

            _power_ = _power_Origin * _power_Multiple_;
        }
    }

    private float _power_Multiple_;

    public float power_Multiple
    {
        get => _power_Multiple_;

        set
        {
            _power_Multiple_ = value;

            _power_ = _power_Origin * _power_Multiple_;
        }
    }

    private float _durationTime_Origin;

    private float _durationTime_;

    public float durationTime
    {
        get => _durationTime_;

        private set
        {
            _durationTime_Origin = value;

            _durationTime_ = _durationTime_Origin * _durationTime_Multiple_;
        }
    }

    private float _durationTime_Multiple_;

    public float durationTime_Multiple
    {
        get => _durationTime_Multiple_;

        set
        {
            _durationTime_Multiple_ = value;

            _durationTime_ = _durationTime_Origin * _durationTime_Multiple_;
        }
    }

    public float durationTimer { get; set; }

    public StatusEffectInfo(StatusEffectData statusEffectData)
    {
        _power_Origin = statusEffectData.power;

        _power_ = statusEffectData.power;

        _power_Multiple_ = 1f;

        _durationTime_Origin = statusEffectData.duration;

        _durationTime_ = statusEffectData.duration;

        _durationTime_Multiple_ = 1f;

        durationTimer = 0f;
    }

    public StatusEffectInfo(StatusEffectInfo statusEffectInfo)
    {
        _power_Origin = statusEffectInfo._power_Origin;

        _power_ = statusEffectInfo._power_;

        _power_Multiple_ = statusEffectInfo._power_Multiple_;

        _durationTime_Origin = statusEffectInfo._durationTime_Origin;

        _durationTime_ = statusEffectInfo._durationTime_;

        _durationTime_Multiple_ = statusEffectInfo._durationTime_Multiple_;

        durationTimer = statusEffectInfo.durationTimer;
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

                power = _power_ * _level_;

                durationTime = _durationTime_ * _level_;
            }
        }

        private float _power_;

        public float power { get; private set; }

        private float _durationTime_;

        public float durationTime { get; private set; }

        public LevelUpData(float power, float durationTime)
        {
            _level_ = 1;

            _power_ = power;

            this.power = power;

            _durationTime_ = durationTime;

            this.durationTime = durationTime;
        }

        public LevelUpData(LevelUpData levelUpData)
        {
            _level_ = levelUpData._level_;

            _power_ = levelUpData._power_;

            power = levelUpData.power;

            _durationTime_ = levelUpData._durationTime_;

            durationTime = levelUpData.durationTime;
        }
    }

    public void LevelUp(LevelUpData levelUpData)
    {
        power += levelUpData.power;

        durationTime += levelUpData.durationTime;
    }
}