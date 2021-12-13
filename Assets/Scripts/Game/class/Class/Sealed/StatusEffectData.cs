
public sealed class StatusEffectData
{
    public readonly StatusEffectCode statusEffectCode;

    public float power { get; private set; }

    public float duration { get; private set; }

    public StatusEffectData(StatusEffectCode statusEffectCode, float power, float duration)
    {
        this.statusEffectCode = statusEffectCode;

        this.power = power;

        this.duration = duration;
    }
}