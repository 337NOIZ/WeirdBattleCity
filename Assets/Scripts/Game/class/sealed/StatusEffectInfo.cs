
public sealed class StatusEffectInfo
{
    public float power_Multiple { get; set; } = 1f;

    public float duration_Multiple { get; set; } = 1f;

    public StatusEffectInfo() { }

    public StatusEffectInfo(float power_Multiple, float duration_Multiple)
    {
        this.power_Multiple = power_Multiple;

        this.duration_Multiple = duration_Multiple;
    }

    public void LevelUp(StatusEffectInfo_LevelUpData statusEffectInfo_LevelUpData)
    {
        power_Multiple += statusEffectInfo_LevelUpData.power_Multiple;

        duration_Multiple += statusEffectInfo_LevelUpData.duration_Multiple;
    }
}