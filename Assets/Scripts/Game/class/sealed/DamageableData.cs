
public sealed class DamageableData
{
    public int healthPoint_Max { get; private set; }

    public float invincibleTime { get; private set; }

    public DamageableData(int healthPoint_Max, float invincibleTime)
    {
        this.healthPoint_Max = healthPoint_Max;

        this.invincibleTime = invincibleTime;
    }

    public DamageableData(DamageableData damageableDate)
    {
        healthPoint_Max = damageableDate.healthPoint_Max;

        invincibleTime = damageableDate.invincibleTime;
    }
}