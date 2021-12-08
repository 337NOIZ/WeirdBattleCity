
public class ExperienceInfo
{
    public float experiencePoint_Max { get; private set; }

    public float experiencePoint_Drop { get; private set; }

    public float experiencePoint { get; set; }

    public ExperienceInfo(ExperienceData experienceData)
    {
        experiencePoint_Max = experienceData.experiencePoint_Max;

        experiencePoint_Drop = experienceData.experiencePoint_Drop;

        Initialize();
    }

    public ExperienceInfo(ExperienceInfo experienceInfo)
    {
        experiencePoint_Max = experienceInfo.experiencePoint_Max;

        experiencePoint_Drop = experienceInfo.experiencePoint_Drop;

        experiencePoint = experienceInfo.experiencePoint;
    }

    public void Initialize()
    {
        experiencePoint = 0;
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

                experiencePoint_Max = _experiencePoint_Max_ * _level_;

                experiencePoint_Drop = _experiencePoint_Drop_ * _level_;
            }
        }

        private float _experiencePoint_Max_;

        public float experiencePoint_Max { get; private set; }

        private float _experiencePoint_Drop_;

        public float experiencePoint_Drop { get; private set; }

        public LevelUpData(float experiencePoint_Max, float experiencePoint_Drop)
        {
            _level_ = 1;

            _experiencePoint_Max_ = experiencePoint_Max;

            this.experiencePoint_Max = experiencePoint_Max;

            _experiencePoint_Drop_ = experiencePoint_Drop;

            this.experiencePoint_Drop = experiencePoint_Drop;
        }

        public LevelUpData(LevelUpData levelUpData)
        {
            _level_ = levelUpData._level_;

            _experiencePoint_Max_ = levelUpData._experiencePoint_Max_;

            experiencePoint_Max = levelUpData.experiencePoint_Max;

            _experiencePoint_Drop_ = levelUpData._experiencePoint_Drop_;

            experiencePoint_Drop = levelUpData.experiencePoint_Drop;
        }
    }

    public void LevelUp(LevelUpData levelUpData)
    {
        experiencePoint_Max += levelUpData.experiencePoint_Max;

        experiencePoint_Drop += levelUpData.experiencePoint_Drop;
    }
}