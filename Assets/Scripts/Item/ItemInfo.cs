
public class ItemInfo
{
    public ItemType itemType;

    public ItemCode itemCode;

    public int stackCount;

    private int stackCount_Max;

    public float stackCount_Max_Multiple = 1f;

    public float cooldownTime = 0f;

    public float cooldownSpeed = 1f;

    public float drawingSpeed = 1f;

    public float consumSpeedMultiple = 1f;

    public float physicalDamage_Multiple = 1f;

    public float physicalReach_Multiple = 1f;

    public float projectileMultishot_Multiple = 1f;

    public float projectileDiffusion_Multiple = 1f;

    public float projectileDamage_Multiple = 1f;

    public float projectileExposionDamage_Multiple = 1f;

    public float projectileForce_Multiple = 1f;

    public float projectileLifeTime_Seconds_Multiple = 1f;

    public int magazine_AmmoCount;

    public float magazine_AmmoCount_Max_Multiple = 1f;

    public float reloadingSpeed = 1f;

    public ItemInfo(ItemType itemType, ItemCode itemCode, int stackCount)
    {
        this.itemType = itemType;

        this.itemCode = itemCode;

        this.stackCount = stackCount;

        stackCount_Max = GameManager.instance.itemDatas[itemCode].stackCount_Max;
    }

    public ItemInfo(ItemType itemType, ItemCode itemCode, int stackCount, int magazine_AmmoCount)
    {
        this.itemType = itemType;

        this.itemCode = itemCode;

        this.stackCount = stackCount;

        stackCount_Max = GameManager.instance.itemDatas[itemCode].stackCount_Max;

        this.magazine_AmmoCount = magazine_AmmoCount;
    }

    public ItemInfo(ItemInfo iteminfo)
    {
        itemType = iteminfo.itemType;

        itemCode = iteminfo.itemCode;

        stackCount = iteminfo.stackCount;

        stackCount_Max = GameManager.instance.itemDatas[itemCode].stackCount_Max;

        stackCount_Max_Multiple = iteminfo.stackCount_Max_Multiple;

        cooldownTime = iteminfo.cooldownTime;

        cooldownSpeed = iteminfo.cooldownSpeed;

        drawingSpeed = iteminfo.drawingSpeed;

        consumSpeedMultiple = iteminfo.consumSpeedMultiple;

        physicalDamage_Multiple = iteminfo.physicalDamage_Multiple;

        physicalReach_Multiple = iteminfo.physicalReach_Multiple;

        projectileDamage_Multiple = iteminfo.projectileDamage_Multiple;

        projectileExposionDamage_Multiple = iteminfo.projectileExposionDamage_Multiple;

        projectileForce_Multiple = iteminfo.projectileForce_Multiple;

        projectileLifeTime_Seconds_Multiple = iteminfo.projectileLifeTime_Seconds_Multiple;

        magazine_AmmoCount = iteminfo.magazine_AmmoCount;

        magazine_AmmoCount_Max_Multiple = iteminfo.magazine_AmmoCount_Max_Multiple;

        reloadingSpeed = iteminfo.reloadingSpeed;
    }

    public int Stack(int count)
    {
        stackCount += count;

        int stackRest = stackCount - GameManager.instance.itemDatas[itemCode].stackCount_Max;

        if (stackRest > 0)
        {
            stackCount -= stackRest;
        }

        else
        {
            stackRest = 0;
        }

        return stackRest;
    }
}