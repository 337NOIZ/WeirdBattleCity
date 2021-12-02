
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
        public int level
        {
            get => level_Origin;

            set
            {
                level_Origin = value;

                experiencePoint_Max = experiencePoint_Max_Origin * level_Origin;

                experiencePoint_Drop = experiencePoint_Drop_Origin * level_Origin;
            }
        }

        private int level_Origin;

        public float experiencePoint_Max { get; private set; }

        private float experiencePoint_Max_Origin;

        public float experiencePoint_Drop { get; private set; }

        private float experiencePoint_Drop_Origin;

        public LevelUpData(float experiencePoint_Max, float experiencePoint_Drop)
        {
            level_Origin = 1;

            this.experiencePoint_Max = experiencePoint_Max;

            experiencePoint_Max_Origin = experiencePoint_Max;

            this.experiencePoint_Drop = experiencePoint_Drop;

            experiencePoint_Drop_Origin = experiencePoint_Drop;
        }

        public LevelUpData(LevelUpData levelUpData)
        {
            level_Origin = levelUpData.level_Origin;

            experiencePoint_Max = levelUpData.experiencePoint_Max;

            experiencePoint_Max_Origin = levelUpData.experiencePoint_Max_Origin;

            experiencePoint_Drop = levelUpData.experiencePoint_Drop;

            experiencePoint_Drop_Origin = levelUpData.experiencePoint_Drop_Origin;
        }
    }

    public void LevelUp(LevelUpData levelUpData)
    {
        experiencePoint_Max += levelUpData.experiencePoint_Max;

        experiencePoint_Drop += levelUpData.experiencePoint_Drop;
    }
}