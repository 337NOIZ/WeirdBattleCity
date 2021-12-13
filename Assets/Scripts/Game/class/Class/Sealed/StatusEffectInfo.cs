
public sealed class StatusEffectInfo
{
    public readonly StatusEffectCode statusEffectCode;

    public float power { get; set; }

    public float durationTime { get; set; }

    public float durationTimer { get; set; }

    public StatusEffectInfo(StatusEffectData statusEffectData)
    {
        statusEffectCode = statusEffectData.statusEffectCode;

        power = statusEffectData.power;

        durationTime = statusEffectData.duration;

        durationTimer = 0f;
    }

    public StatusEffectInfo(StatusEffectInfo statusEffectInfo)
    {
        statusEffectCode = statusEffectInfo.statusEffectCode;

        power = statusEffectInfo.power;

        durationTime = statusEffectInfo.durationTime;

        durationTimer = statusEffectInfo.durationTimer;
    }

    public void SetDurationTimer()
    {
        durationTimer = durationTime;
    }
}