
public sealed class TransformInfo_LevelUpData
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
                    scale_Multiple /= _level;
                }

                _level = value;

                scale_Multiple *= _level;
            }
        }
    }

    public float scale_Multiple { get; set; }

    public TransformInfo_LevelUpData(float scale_Multiple)
    {
        this.scale_Multiple = scale_Multiple;
    }

    public TransformInfo_LevelUpData(TransformInfo_LevelUpData transformInfo_LevelUpData)
    {
        _level = transformInfo_LevelUpData._level;

        scale_Multiple = transformInfo_LevelUpData.scale_Multiple;
    }
}