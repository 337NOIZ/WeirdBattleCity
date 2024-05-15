public sealed class StatusEffectData
{
    public readonly StatusEffectCode statusEffectCode;

    public readonly float power;

    public readonly float duration;

    public StatusEffectData(StatusEffectCode statusEffectCode, float power, float duration)
    {
        this.statusEffectCode = statusEffectCode;

        this.power = power;

        this.duration = duration;
    }
}