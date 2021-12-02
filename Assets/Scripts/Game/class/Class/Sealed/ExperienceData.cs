
public sealed class ExperienceData
{
    public float experiencePoint_Max { get; private set; }

    public float experiencePoint_Drop { get; private set; }

    public ExperienceData(float experiencePoint_Max, float experiencePoint_Drop)
    {
        this.experiencePoint_Max = experiencePoint_Max;

        this.experiencePoint_Drop = experiencePoint_Drop;
    }

    public ExperienceData(ExperienceData experienceDate)
    {
        experiencePoint_Max = experienceDate.experiencePoint_Max;

        experiencePoint_Drop = experienceDate.experiencePoint_Drop;
    }
}