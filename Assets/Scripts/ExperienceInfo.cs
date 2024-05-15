public sealed class ExperienceInfo
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
        private int _level;

        public int level
        {
            get => _level;

            set
            {
                _level = value;

                experiencePoint_Max = _experiencePoint_Max * _level;

                experiencePoint_Drop = _experiencePoint_Drop * _level;
            }
        }

        private float _experiencePoint_Max;

        public float experiencePoint_Max { get; private set; }

        private float _experiencePoint_Drop;

        public float experiencePoint_Drop { get; private set; }

        public LevelUpData(float experiencePoint_Max, float experiencePoint_Drop)
        {
            _level = 1;

            _experiencePoint_Max = experiencePoint_Max;

            this.experiencePoint_Max = experiencePoint_Max;

            _experiencePoint_Drop = experiencePoint_Drop;

            this.experiencePoint_Drop = experiencePoint_Drop;
        }

        public LevelUpData(LevelUpData levelUpData)
        {
            _level = levelUpData._level;

            _experiencePoint_Max = levelUpData._experiencePoint_Max;

            experiencePoint_Max = levelUpData.experiencePoint_Max;

            _experiencePoint_Drop = levelUpData._experiencePoint_Drop;

            experiencePoint_Drop = levelUpData.experiencePoint_Drop;
        }
    }

    public void LevelUp(LevelUpData levelUpData)
    {
        experiencePoint_Max += levelUpData.experiencePoint_Max;

        experiencePoint_Drop += levelUpData.experiencePoint_Drop;
    }
}