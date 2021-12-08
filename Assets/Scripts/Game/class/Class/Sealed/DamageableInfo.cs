
public sealed class DamageableInfo
{
    private float _healthPoint_Max_Origin;

    private float _healthPoint_Max_;

    public float healthPoint_Max
    {
        get => _healthPoint_Max_;

        private set
        {
            _healthPoint_Max_Origin = value;

            _healthPoint_Max_ = _healthPoint_Max_Origin * _healthPoint_Max_Multiple_;
        }
    }

    private float _healthPoint_Max_Multiple_;

    public float healthPoint_Max_Multiple
    {
        get => _healthPoint_Max_Multiple_;

        set
        {
            _healthPoint_Max_Multiple_ = value;

            _healthPoint_Max_ = _healthPoint_Max_Origin * _healthPoint_Max_Multiple_;
        }
    }

    private float _invincibleTime_Origin;

    private float _invincibleTime_;

    public float invincibleTime
    {
        get => _invincibleTime_;

        private set
        {
            _invincibleTime_Origin = value;

            _invincibleTime_ = _invincibleTime_Origin * _invincibleTime_Multiple_;
        }
    }

    private float _invincibleTime_Multiple_;

    public float invincibleTime_Multiple
    {
        get => _invincibleTime_Multiple_;

        set
        {
            _invincibleTime_Multiple_ = value;

            _invincibleTime_ = _invincibleTime_Origin * _invincibleTime_Multiple_;
        }
    }

    private float _healthPoint_;

    public float healthPoint
    {
        get => _healthPoint_;

        set
        {
            if(value > _healthPoint_Max_)
            {
                _healthPoint_ = _healthPoint_Max_;
                
            }

            if (value < 0f)
            {
                _healthPoint_ = 0f;

            }

            else
            {
                _healthPoint_ = value;
            }
        }
    }

    public float invincibleTimer { get; set; }

    public DamageableInfo(DamageableData damageableData)
    {
        _healthPoint_Max_Origin = damageableData.healthPoint_Max;

        _healthPoint_Max_ = damageableData.healthPoint_Max;

        _healthPoint_Max_Multiple_ = 1f;

        _invincibleTime_Origin = damageableData.invincibleTime;

        _invincibleTime_ = damageableData.invincibleTime;

        _invincibleTime_Multiple_ = 1f;

        Initialize();
    }

    public DamageableInfo(DamageableInfo damageableInfo)
    {
        _healthPoint_Max_Origin = damageableInfo._healthPoint_Max_Origin;

        _healthPoint_Max_ = damageableInfo._healthPoint_Max_;

        _healthPoint_Max_Multiple_ = damageableInfo._healthPoint_Max_Multiple_;

        _invincibleTime_Origin = damageableInfo._invincibleTime_Origin;

        _invincibleTime_ = damageableInfo._invincibleTime_;

        _invincibleTime_Multiple_ = damageableInfo._invincibleTime_Multiple_;

        _healthPoint_ = damageableInfo._healthPoint_;

        invincibleTimer = damageableInfo.invincibleTimer;
    }

    public void Initialize()
    {
        _healthPoint_ = _healthPoint_Max_;

        invincibleTimer = 0f;
    }

    public void SetInvincibleTimer()
    {
        invincibleTimer = _invincibleTime_;
    }

    public class LevelUpData
    {
        private int _level_;

        public int level
        {
            get => _level_;

            set
            {
                _level_ = value;

                healthPoint_Max = _healthPoint_Max_ * _level_;
            }
        }

        private float _healthPoint_Max_;

        public float healthPoint_Max { get; private set; }

        public LevelUpData(float healthPoint_Max)
        {
            _level_ = 1;

            _healthPoint_Max_ = healthPoint_Max;

            this.healthPoint_Max = healthPoint_Max;
        }

        public LevelUpData(LevelUpData levelUpData)
        {
            _level_ = levelUpData._level_;

            _healthPoint_Max_ = levelUpData._healthPoint_Max_;

            healthPoint_Max = levelUpData.healthPoint_Max;
        }
    }

    public void LevelUp(LevelUpData levelUpData)
    {
        _healthPoint_Max_Origin += levelUpData.healthPoint_Max;

        _healthPoint_ += levelUpData.healthPoint_Max;
    }
}