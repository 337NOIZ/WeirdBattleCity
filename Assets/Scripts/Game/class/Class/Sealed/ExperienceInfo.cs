
public class ExperienceInfo
{
    public float experiencePoint_Gain { get; private set; }

    public float experiencePoint_Max { get; private set; }

    private float _experiencePoint_Gain_Extra = 0f;

    public float experiencePoint_Gain_Extra
    {
        get { return _experiencePoint_Gain_Extra; }

        set
        {
            if (value > 0)
            {
                experiencePoint_Gain -= _experiencePoint_Gain_Extra;

                _experiencePoint_Gain_Extra = value;

                experiencePoint_Gain += _experiencePoint_Gain_Extra;
            }
        }
    }

    private float _experiencePoint_Max_Extra = 0f;

    public float experiencePoint_Max_Extra
    {
        get { return _experiencePoint_Max_Extra; }

        set
        {
            if (value > 0)
            {
                experiencePoint_Max -= _experiencePoint_Max_Extra;

                _experiencePoint_Max_Extra = value;

                experiencePoint_Max += _experiencePoint_Max_Extra;
            }
        }
    }

    public float experiencePoint { get; set; }

    public class LevelUpData
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
                    experiencePoint_Gain_Extra -= _level;

                    experiencePoint_Max_Extra -= _level;

                    _level = value;

                    experiencePoint_Gain_Extra += _level;

                    experiencePoint_Max_Extra += _level;
                }
            }
        }

        public float experiencePoint_Gain_Extra { get; private set; } 

        public float experiencePoint_Max_Extra { get; private set; }

        public LevelUpData(float experiencePoint_Gain_Extra, float experiencePoint_Max_Extra)
        {
            this.experiencePoint_Gain_Extra = experiencePoint_Gain_Extra;

            this.experiencePoint_Max_Extra = experiencePoint_Max_Extra;
        }

        public LevelUpData(LevelUpData levelUpData)
        {
            experiencePoint_Gain_Extra = levelUpData.experiencePoint_Gain_Extra;

            experiencePoint_Max_Extra = levelUpData.experiencePoint_Max_Extra;
        }
    }

    public ExperienceInfo(ExperienceData experienceData)
    {
        experiencePoint_Gain = experienceData.experiencePoint_Gain;

        experiencePoint_Max = experienceData.experiencePoint_Max;

        Initialize();
    }

    public ExperienceInfo(ExperienceInfo experienceInfo)
    {
        experiencePoint_Gain = experienceInfo.experiencePoint_Gain;

        experiencePoint_Max = experienceInfo.experiencePoint_Max;

        _experiencePoint_Gain_Extra = experienceInfo._experiencePoint_Gain_Extra;

        _experiencePoint_Max_Extra = experienceInfo._experiencePoint_Max_Extra;

        experiencePoint = experienceInfo.experiencePoint;
    }

    public void Initialize()
    {
        experiencePoint = 0;
    }

    public void LevelUp(LevelUpData levelUpData)
    {
        experiencePoint_Gain_Extra += levelUpData.experiencePoint_Gain_Extra;

        experiencePoint_Max_Extra += levelUpData.experiencePoint_Max_Extra;
    }
}