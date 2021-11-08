
public class DamageableInfo_LevelUpData
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
            if (value != _level)
            {
                if (_level > 1)
                {
                    healthPoint_Max_Multiple /= _level;

                    invincibleTime_Multiple /= _level;
                }

                _level = value;

                healthPoint_Max_Multiple *= _level;

                invincibleTime_Multiple *= _level;
            }
        }
    }

    public float healthPoint_Max_Multiple { get; private set; }

    public float invincibleTime_Multiple { get; private set; }

    public DamageableInfo_LevelUpData(float healthPoint_Max_Multiple, float invincibleTime_Multiple)
    {
        this.healthPoint_Max_Multiple = healthPoint_Max_Multiple;

        this.invincibleTime_Multiple = invincibleTime_Multiple;
    }

    public DamageableInfo_LevelUpData(DamageableInfo_LevelUpData damageableInfo_LevelUpData)
    {
        _level = damageableInfo_LevelUpData._level;

        healthPoint_Max_Multiple = damageableInfo_LevelUpData.healthPoint_Max_Multiple;

        invincibleTime_Multiple = damageableInfo_LevelUpData.invincibleTime_Multiple;
    }
}