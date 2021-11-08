
public sealed class ItemData
{
    public int stackCount_Max { get; private set; }

    public float cooldownTime { get; private set; } = 0f;

    public float drawingTime { get; private set; } = 0f;

    public float consumTime { get; private set; } = 0f;

    public bool autoAttack { get; private set; } = false;

    public float attackingTime { get; private set; } = 0;

    public float reloadingTime { get; private set; } = 0f;

    public int physicalDamage { get; private set; } = 0;

    public float physicalReach { get; private set; } = 0f;

    public float projectileMultishot { get; private set; } = 0f;

    public float projectileDiffusion { get; private set; } = 0f;

    public int projectileDamage { get; private set; } = 0;

    public float projectileForce { get; private set; } = 0f;

    public float projectileLifeTime { get; private set; } = 0f;

    public int ammoCount_Max { get; private set; } = 0;

    public int resilience { get; private set; } = 0;

    public int splashDamage { get; private set; } = 0;

    public ItemData(int stackCount_Max)
    {
        this.stackCount_Max = stackCount_Max;
    }

    public ItemData(int stackCount_Max, float cooldownTime, float drawingTime, float consumTime, int resilience, int splashDamage)
    {
        this.stackCount_Max = stackCount_Max;

        this.cooldownTime = cooldownTime;

        this.drawingTime = drawingTime;

        this.consumTime = consumTime;

        this.resilience = resilience;

        this.splashDamage = splashDamage;
    }

    public ItemData(int stackCount_Max, float cooldownTime, float drawingTime, bool autoAttack, float attackingTime, float reloadingTime, int physicalDamage, float physicalReach, float projectileMultishot, float projectileDiffusion, int projectileDamage, float projectileForce, float projectileLifeTime, int ammoCount_Max, int resilience, int splashDamage)
    {
        this.stackCount_Max = stackCount_Max;

        this.cooldownTime = cooldownTime;

        this.drawingTime = drawingTime;

        this.autoAttack = autoAttack;

        this.attackingTime = attackingTime;

        this.reloadingTime = reloadingTime;

        this.physicalDamage = physicalDamage;

        this.physicalReach = physicalReach;

        this.projectileMultishot = projectileMultishot;

        this.projectileDiffusion = projectileDiffusion;

        this.projectileDamage = projectileDamage;

        this.projectileForce = projectileForce;

        this.projectileLifeTime = projectileLifeTime;

        this.ammoCount_Max = ammoCount_Max;

        this.resilience = resilience;

        this.splashDamage = splashDamage;
    }

    public ItemData(ItemData itemData)
    {
        stackCount_Max = itemData.stackCount_Max;

        cooldownTime = itemData.cooldownTime;

        drawingTime = itemData.drawingTime;

        consumTime = itemData.consumTime;

        autoAttack = itemData.autoAttack;

        attackingTime = itemData.attackingTime;

        reloadingTime = itemData.reloadingTime;

        physicalDamage = itemData.physicalDamage;

        physicalReach = itemData.physicalReach;

        projectileMultishot = itemData.projectileMultishot;

        projectileDiffusion = itemData.projectileDiffusion;

        projectileDamage = itemData.projectileDamage;

        projectileForce = itemData.projectileForce;

        projectileLifeTime = itemData.projectileLifeTime;

        ammoCount_Max = itemData.ammoCount_Max;

        resilience = itemData.resilience;

        splashDamage = itemData.splashDamage;
    }
}