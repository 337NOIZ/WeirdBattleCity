public sealed class DamageableInfo
{
    private float _healthPoint_Max_Origin;

    private float _healthPoint_Max;

    public float healthPoint_Max
    {
        get => _healthPoint_Max;

        private set
        {
            _healthPoint_Max_Origin = value;

            _healthPoint_Max = _healthPoint_Max_Origin * _healthPoint_Max_Multiple;
        }
    }

    private float _healthPoint_Max_Multiple;

    public float healthPoint_Max_Multiple
    {
        get => _healthPoint_Max_Multiple;

        set
        {
            _healthPoint_Max_Multiple = value;

            _healthPoint_Max = _healthPoint_Max_Origin * _healthPoint_Max_Multiple;
        }
    }

    private float _invincibleTime_Origin;

    private float _invincibleTime;

    public float invincibleTime
    {
        get => _invincibleTime;

        private set
        {
            _invincibleTime_Origin = value;

            _invincibleTime = _invincibleTime_Origin * _invincibleTime_Multiple;
        }
    }

    private float _invincibleTime_Multiple;

    public float invincibleTime_Multiple
    {
        get => _invincibleTime_Multiple;

        set
        {
            _invincibleTime_Multiple = value;

            _invincibleTime = _invincibleTime_Origin * _invincibleTime_Multiple;
        }
    }

    private float _healthPoint;

    public float healthPoint
    {
        get => _healthPoint;

        set
        {
            _healthPoint = value;

            if (_healthPoint > _healthPoint_Max)
            {
                _healthPoint = _healthPoint_Max;
                
            }

            if (_healthPoint < 0f)
            {
                _healthPoint = 0f;

            }
        }
    }

    public float invincibleTimer { get; set; }

    public DamageableInfo(DamageableData damageableData)
    {
        _healthPoint_Max_Origin = damageableData.healthPoint_Max;

        _healthPoint_Max = damageableData.healthPoint_Max;

        _healthPoint_Max_Multiple = 1f;

        _invincibleTime_Origin = damageableData.invincibleTime;

        _invincibleTime = damageableData.invincibleTime;

        _invincibleTime_Multiple = 1f;

        Initialize();
    }

    public DamageableInfo(DamageableInfo damageableInfo)
    {
        _healthPoint_Max_Origin = damageableInfo._healthPoint_Max_Origin;

        _healthPoint_Max = damageableInfo._healthPoint_Max;

        _healthPoint_Max_Multiple = damageableInfo._healthPoint_Max_Multiple;

        _invincibleTime_Origin = damageableInfo._invincibleTime_Origin;

        _invincibleTime = damageableInfo._invincibleTime;

        _invincibleTime_Multiple = damageableInfo._invincibleTime_Multiple;

        _healthPoint = damageableInfo._healthPoint;

        invincibleTimer = damageableInfo.invincibleTimer;
    }

    public void Initialize()
    {
        _healthPoint = _healthPoint_Max;

        invincibleTimer = 0f;
    }

    public void SetInvincibleTimer()
    {
        invincibleTimer = _invincibleTime;
    }

    public class LevelUpData
    {
        private int _level;

        public int level
        {
            get => _level;

            set
            {
                _level = value;

                healthPoint_Max = _healthPoint_Max * _level;
            }
        }

        private float _healthPoint_Max;

        public float healthPoint_Max { get; private set; }

        public LevelUpData(float healthPoint_Max)
        {
            _level = 1;

            _healthPoint_Max = healthPoint_Max;

            this.healthPoint_Max = healthPoint_Max;
        }

        public LevelUpData(LevelUpData levelUpData)
        {
            _level = levelUpData._level;

            _healthPoint_Max = levelUpData._healthPoint_Max;

            healthPoint_Max = levelUpData.healthPoint_Max;
        }
    }

    public void LevelUp(LevelUpData levelUpData)
    {
        _healthPoint_Max_Origin += levelUpData.healthPoint_Max;

        _healthPoint += levelUpData.healthPoint_Max;
    }
}