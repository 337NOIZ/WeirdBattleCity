
public class DamageableInfo
{
    public CharacterType damageableType { get; private set; }

    public CharacterCode damageableCode { get; private set; }

    public int healthPoint { get; set; } = 0;

    public float healthPoint_Max_Multiple { get; set; } = 1f;

    public float invincibleTimer { get; set; } = 0f;

    public float invincibleTime_Multiple { get; set; } = 1f;

    public DamageableInfo(CharacterType damageableType, CharacterCode damageableCode)
    {
        this.damageableType = damageableType;

        this.damageableCode = damageableCode;
    }

    public DamageableInfo(CharacterType damageableType, CharacterCode damageableCode, float healthPoint_Max_Multiple, float invincibleTime_Multiple)
    {
        this.damageableType = damageableType;

        this.damageableCode = damageableCode;

        this.healthPoint_Max_Multiple = healthPoint_Max_Multiple;

        this.invincibleTime_Multiple = invincibleTime_Multiple;
    }

    public DamageableInfo(DamageableInfo damageableInfo)
    {
        damageableType = damageableInfo.damageableType;

        damageableCode = damageableInfo.damageableCode;

        healthPoint = damageableInfo.healthPoint;

        healthPoint_Max_Multiple = damageableInfo.healthPoint_Max_Multiple;

        invincibleTime_Multiple = invincibleTime_Multiple;
    }
}