
using UnityEngine;

public class ItemInfo
{
    public ItemType itemType { get; private set; }

    public ItemCode itemCode { get; private set; }

    public int stackCount { get; set; } = 1;

    public float stackCount_Max_Multiple { get; set; } = 1f;

    public float cooldownTimer { get; set; } = 0f;

    public float cooldownSpeed { get; set; } = 1f;

    public float drawingSpeed { get; set; } = 1f;

    public float consumSpeed { get; set; } = 1f;

    public float attackingSpeed { get; set; } = 1f;

    public float reloadingSpeed { get; set; } = 1f;

    public float physicalDamage_Multiple { get; set; } = 1f;

    public float physicalReach_Multiple { get; set; } = 1f;

    public float projectileMultishot_Multiple { get; set; } = 1f;

    public float projectileDiffusion_Multiple { get; set; } = 1f;

    public float projectileDamage_Multiple { get; set; } = 1f;

    public float projectileExposionDamage_Multiple { get; set; } = 1f;

    public float projectileForce_Multiple { get; set; } = 1f;

    public float projectileLifeTime_Seconds_Multiple { get; set; } = 1f;

    public int ammoCount { get; set; } = 0;

    public float ammoCount_Max_Multiple { get; set; } = 1f;

    public float resilience_Multiple { get; set; } = 1f;

    public float splashDamage_Multiple { get; set; } = 1f;

    public Vector3 transformPosition { get; set; } = default;

    public ItemInfo(ItemType itemType, ItemCode itemCode)
    {
        this.itemType = itemType;

        this.itemCode = itemCode;
    }

    public ItemInfo(ItemType itemType, ItemCode itemCode, int count)
    {
        this.itemType = itemType;

        this.itemCode = itemCode;

        if (itemType == ItemType.weapon)
        {
            ammoCount = count;
        }

        else
        {
            stackCount = count;
        }
    }

    public ItemInfo(ItemInfo itemInfo)
    {
        itemType = itemInfo.itemType;

        itemCode = itemInfo.itemCode;

        stackCount = itemInfo.stackCount;

        stackCount_Max_Multiple = itemInfo.stackCount_Max_Multiple;

        cooldownTimer = itemInfo.cooldownTimer;

        cooldownSpeed = itemInfo.cooldownSpeed;

        drawingSpeed = itemInfo.drawingSpeed;

        consumSpeed = itemInfo.consumSpeed;

        attackingSpeed = itemInfo.attackingSpeed;

        reloadingSpeed = itemInfo.reloadingSpeed;

        physicalDamage_Multiple = itemInfo.physicalDamage_Multiple;

        physicalReach_Multiple = itemInfo.physicalReach_Multiple;

        projectileDamage_Multiple = itemInfo.projectileDamage_Multiple;

        projectileExposionDamage_Multiple = itemInfo.projectileExposionDamage_Multiple;

        projectileForce_Multiple = itemInfo.projectileForce_Multiple;

        projectileLifeTime_Seconds_Multiple = itemInfo.projectileLifeTime_Seconds_Multiple;

        ammoCount = itemInfo.ammoCount;

        ammoCount_Max_Multiple = itemInfo.ammoCount_Max_Multiple;

        resilience_Multiple = itemInfo.resilience_Multiple;

        splashDamage_Multiple = itemInfo.splashDamage_Multiple;

        transformPosition = itemInfo.transformPosition;
    }
}