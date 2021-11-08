
public sealed class DamageableInfo
{
    public int healthPoint { get; set; } = 0;

    public float healthPoint_Max_Multiple { get; set; } = 1f;

    public float invincibleTimer { get; set; } = 0f;

    public float invincibleTime_Multiple { get; set; } = 1f;

    public DamageableInfo() { }

    public DamageableInfo(DamageableInfo damageableInfo)
    {
        healthPoint = damageableInfo.healthPoint;

        healthPoint_Max_Multiple = damageableInfo.healthPoint_Max_Multiple;

        invincibleTimer = damageableInfo.invincibleTimer;

        invincibleTime_Multiple = damageableInfo.invincibleTime_Multiple;
    }

    public void Recycling()
    {
        healthPoint = 0;

        invincibleTimer = 0f;
    }

    public void LevelUp(DamageableInfo_LevelUpData damageableInfo_LevelUpData)
    {
        healthPoint_Max_Multiple += damageableInfo_LevelUpData.healthPoint_Max_Multiple;

        invincibleTime_Multiple += damageableInfo_LevelUpData.invincibleTime_Multiple;
    }
}