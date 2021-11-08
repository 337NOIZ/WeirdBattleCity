
public sealed class StatusEffectData
{
    public StatusEffectCode statusEffectCode { get; private set; }

    public float statusEffect_Power { get; private set; }

    public float statusEffect_Duration { get; private set; }

    public StatusEffectData(StatusEffectCode statusEffectCode, float statusEffect_Power, float statusEffect_Duration)
    {
        this.statusEffectCode = statusEffectCode;

        this.statusEffect_Power = statusEffect_Power;

        this.statusEffect_Duration = statusEffect_Duration;
    }
}