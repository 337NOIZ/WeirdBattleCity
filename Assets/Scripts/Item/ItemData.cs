
public class ItemData
{
    public int stackCount_Max { get; private set; }

    public float cooldownTime_Seconds { get; private set; }

    public float drawingTime_Seconds { get; private set; }

    public float consumTime_Seconds { get; private set; }

    public bool autoAttack { get; private set; }

    public int physicalDamage { get; private set; }

    public float physicalReach { get; private set; }

    public float projectileMultishot { get; private set; }

    public float projectileDiffusion { get; private set; }

    public int projectileDamage { get; private set; }

    public int projectileExposionDamage { get; private set; }

    public float projectileForce { get; private set; }

    public float projectileLifeTime_Seconds { get; private set; }

    public int magazine_AmmoCount_Max { get; private set; }

    public float reloadingTime_Seconds { get; private set; }

    public ItemData(int stackCount_Max)
    {
        this.stackCount_Max = stackCount_Max;
    }

    public ItemData(int stackCount_Max, float cooldownTime_Seconds, float drawingTime_Seconds, float consumTime_Seconds)
    {
        this.stackCount_Max = stackCount_Max;

        this.cooldownTime_Seconds = cooldownTime_Seconds;

        this.drawingTime_Seconds = drawingTime_Seconds;

        this.consumTime_Seconds = consumTime_Seconds;
    }

    public ItemData(int stackCount_Max, float cooldownTime_Seconds, float drawingTime_Seconds, bool autoAttack, int physicalDamage, float physicalReach, float projectileMultishot, float projectileDiffusion, int projectileDamage, int projectileExposionDamage, float projectileForce, float projectileLifeTime_Seconds, int magazine_AmmoCount_Max, float reloadingTime_Seconds)
    {
        this.stackCount_Max = stackCount_Max;

        this.cooldownTime_Seconds = cooldownTime_Seconds;

        this.drawingTime_Seconds = drawingTime_Seconds;

        this.autoAttack = autoAttack;

        this.physicalDamage = physicalDamage;

        this.physicalReach = physicalReach;

        this.projectileMultishot = projectileMultishot;

        this.projectileDiffusion = projectileDiffusion;

        this.projectileDamage = projectileDamage;

        this.projectileExposionDamage = projectileExposionDamage;

        this.projectileForce = projectileForce;

        this.projectileLifeTime_Seconds = projectileLifeTime_Seconds;

        this.magazine_AmmoCount_Max = magazine_AmmoCount_Max;

        this.reloadingTime_Seconds = reloadingTime_Seconds;
    }

    public ItemData(ItemData itemData)
    {
        stackCount_Max = itemData.stackCount_Max;

        cooldownTime_Seconds = itemData.cooldownTime_Seconds;

        drawingTime_Seconds = itemData.drawingTime_Seconds;

        consumTime_Seconds = itemData.consumTime_Seconds;

        autoAttack = itemData.autoAttack;

        physicalDamage = itemData.physicalDamage;

        physicalReach = itemData.physicalReach;

        projectileMultishot = itemData.projectileMultishot;

        projectileDiffusion = itemData.projectileDiffusion;

        projectileDamage = itemData.projectileDamage;

        projectileExposionDamage = itemData.projectileExposionDamage;

        projectileForce = itemData.projectileForce;

        projectileLifeTime_Seconds = itemData.projectileLifeTime_Seconds;

        magazine_AmmoCount_Max = itemData.magazine_AmmoCount_Max;

        reloadingTime_Seconds = itemData.reloadingTime_Seconds;
    }
}