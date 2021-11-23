
public sealed class ExperienceData
{
    public int experiencePoint_Gain { get; private set; }

    public int experiencePoint_Max { get; private set; }

    public ExperienceData(int experiencePoint_Gain, int experiencePoint_Max)
    {
        this.experiencePoint_Gain = experiencePoint_Gain;

        this.experiencePoint_Max = experiencePoint_Max;
    }

    public ExperienceData(ExperienceData experienceDate)
    {
        experiencePoint_Gain = experienceDate.experiencePoint_Gain;

        experiencePoint_Max = experienceDate.experiencePoint_Max;
    }
}