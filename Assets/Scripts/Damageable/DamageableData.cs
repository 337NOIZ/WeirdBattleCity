
public class DamageableData
{
    public int healthPoint;
    
    public int healthPoint_Max;
    
    public float invincibleTime = 0f;

    public float invincibleTime_Seconds;

    public int contactDamage;

    public DamageableData(int healthPoint, int healthPoint_Max, float invincibleTime_Seconds, int contactDamage)
    {
        this.healthPoint = healthPoint;

        this.healthPoint_Max = healthPoint_Max;

        this.invincibleTime_Seconds = invincibleTime_Seconds;

        this.contactDamage = contactDamage;
    }

    public DamageableData(DamageableData damageableDate)
    {
        healthPoint = damageableDate.healthPoint;

        healthPoint_Max = damageableDate.healthPoint_Max;

        invincibleTime_Seconds = damageableDate.invincibleTime_Seconds;

        contactDamage = damageableDate.contactDamage;
    }
}