
public sealed class DamageableInfo
{
    public float healthPoint_Max { get; private set; }

    public float invincibleTime { get; private set; }

    private float _healthPoint_Max_Extra = 0f;

    public float healthPoint_Max_Extra
    {
        get { return _healthPoint_Max_Extra; }

        set
        {
            if (value > 0)
            {
                healthPoint_Max -= _healthPoint_Max_Extra;

                _healthPoint_Max_Extra = value;

                healthPoint_Max += _healthPoint_Max_Extra;
            }
        }
    }

    private float _healthPoint_Max_Multiple = 1f;

    public float healthPoint_Max_Multiple
    {
        get { return _healthPoint_Max_Multiple; }

        set
        {
            if (value > 0f)
            {
                healthPoint_Max /= _healthPoint_Max_Multiple;

                _healthPoint_Max_Multiple = value;

                healthPoint_Max *= _healthPoint_Max_Multiple;
            }
        }
    }

    private float _invincibleTime_Extra = 1f;

    public float invincibleTime_Extra
    {
        get { return _invincibleTime_Extra; }

        set
        {
            if (value > 0)
            {
                invincibleTime -= _invincibleTime_Extra;

                _invincibleTime_Extra = value;

                invincibleTime += _invincibleTime_Extra;
            }
        }
    }

    private float _invincibleTime_Multiple = 1f;

    public float invincibleTime_Multiple
    {
        get { return _invincibleTime_Multiple; }

        set
        {
            if (value > 0f)
            {
                invincibleTime /= _invincibleTime_Multiple;

                _invincibleTime_Multiple = value;

                invincibleTime *= _invincibleTime_Multiple;
            }
        }
    }

    public float healthPoint { get; set; }

    public float invincibleTimer { get; set; }

    public class LevelUpData
    {
        private int _level = 1;

        public int level
        {
            get { return _level; }

            set
            {
                if (value > 0 && value != _level)
                {
                    healthPoint_Max_Extra /= _level;

                    _level = value;

                    healthPoint_Max_Extra *= _level;
                }
            }
        }

        public float healthPoint_Max_Extra { get; private set; }

        public LevelUpData(float healthPoint_Max_Extra)
        {
            this.healthPoint_Max_Extra = healthPoint_Max_Extra;
        }

        public LevelUpData(LevelUpData levelUpData)
        {
            _level = levelUpData._level;

            healthPoint_Max_Extra = levelUpData.healthPoint_Max_Extra;
        }
    }

    public DamageableInfo(DamageableData damageableData)
    {
        healthPoint_Max = damageableData.healthPoint_Max;

        invincibleTime = damageableData.invincibleTime;

        Initialize();
    }

    public DamageableInfo(DamageableInfo damageableInfo)
    {
        healthPoint_Max = damageableInfo.healthPoint_Max;

        invincibleTime = damageableInfo.invincibleTime;

        healthPoint_Max_Extra = damageableInfo._healthPoint_Max_Extra;

        healthPoint_Max_Multiple = damageableInfo._healthPoint_Max_Multiple;

        invincibleTime_Extra = damageableInfo._invincibleTime_Extra;

        invincibleTime_Multiple = damageableInfo._invincibleTime_Multiple;

        healthPoint = damageableInfo.healthPoint;

        invincibleTimer = damageableInfo.invincibleTimer;
    }

    public void Initialize()
    {
        healthPoint = healthPoint_Max;

        invincibleTimer = 0f;
    }

    public void LevelUp(LevelUpData levelUpData)
    {
        healthPoint_Max_Multiple += levelUpData.healthPoint_Max_Extra;
    }

    public void SetInvincibleTimer()
    {
        invincibleTimer = invincibleTime;
    }
}