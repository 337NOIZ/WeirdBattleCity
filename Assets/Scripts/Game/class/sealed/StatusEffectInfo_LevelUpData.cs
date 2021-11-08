
public sealed class StatusEffectInfo_LevelUpData
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
                    power_Multiple /= _level;

                    duration_Multiple /= _level;
                }

                _level = value;

                power_Multiple *= _level;

                duration_Multiple *= _level;
            }
        }
    }

    public float power_Multiple { get; set; }

    public float duration_Multiple { get; set; }

    public StatusEffectInfo_LevelUpData(float power_Multiple, float duration_Multiple)
    {
        this.power_Multiple = power_Multiple;

        this.duration_Multiple = duration_Multiple;
    }

    public StatusEffectInfo_LevelUpData(StatusEffectInfo_LevelUpData statusEffectInfo_LevelUpData)
    {
        _level = statusEffectInfo_LevelUpData._level;

        power_Multiple = statusEffectInfo_LevelUpData.power_Multiple;

        duration_Multiple = statusEffectInfo_LevelUpData.duration_Multiple;
    }
}