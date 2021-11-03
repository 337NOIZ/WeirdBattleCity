
public class DamageableData
{
    public int healthPoint_Max { get; private set; }

    public float invincibleTime_Seconds { get; private set; }

    public DamageableData(int healthPoint_Max, float invincibleTime_Seconds)
    {
        this.healthPoint_Max = healthPoint_Max;

        this.invincibleTime_Seconds = invincibleTime_Seconds;
    }

    public DamageableData(DamageableData damageableDate)
    {
        healthPoint_Max = damageableDate.healthPoint_Max;

        invincibleTime_Seconds = damageableDate.invincibleTime_Seconds;
    }
}