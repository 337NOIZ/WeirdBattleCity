
using UnityEngine;

[System.Serializable]

public class ItemData
{
    [Space] public ItemType itemType;

    public ItemCode itemCode;

    [Space] public bool onlyHaveOne;

    [Space] public int count = 1;

    public int countMax = 1;

    [Space] public float cooldown = 0;

    public float cooldownTime;

    [Space] public float consumTime = 0;

    [Space] public bool autoAttack = false;

    [Space] public bool dualWield = false;

    [Space] public int physicalDamage = 0;

    public float physicalReach = 0;

    [Space] public int projectileDamage = 0;

    public float projectileForce = 0f;

    public float projectileLifeTime = 0f;

    [Space] public int magazineCapacity = 0;

    public int magazinRest = 0;

    [Space] public float reloadTime = 0f;

    public ItemData(ItemType itemType, ItemCode itemCode, bool onlyHaveOne, int count, int countMax)
    {
        this.itemType = itemType;

        this.itemCode = itemCode;

        this.onlyHaveOne = onlyHaveOne;

        this.count = count;

        this.countMax = countMax;
    }

    public ItemData(ItemType itemType, ItemCode itemCode, bool onlyHaveOne, int count, int countMax, float cooldownTime, float consumTime)
    {
        this.itemType = itemType;

        this.itemCode = itemCode;

        this.onlyHaveOne = onlyHaveOne;

        this.count = count;

        this.countMax = countMax;

        this.cooldownTime = cooldownTime;

        this.consumTime = consumTime;
    }

    public ItemData(ItemType itemType, ItemCode itemCode, bool onlyHaveOne, float cooldownTime, bool autoAttack, bool dualWield, int physicalDamage, float physicalReach)
    {
        this.itemType = itemType;

        this.itemCode = itemCode;

        this.onlyHaveOne = onlyHaveOne;

        this.cooldownTime = cooldownTime;

        this.autoAttack = autoAttack;

        this.dualWield = dualWield;

        this.physicalDamage = physicalDamage;

        this.physicalReach = physicalReach;
    }

    public ItemData(ItemType itemType, ItemCode itemCode, bool onlyHaveOne, float cooldownTime, bool autoAttack, bool dualWield, int physicalDamage, float physicalReach, int projectileDamage, float projectileForce, float projectileLifeTime, int magazineCapacity, int magazinRest, float reloadTime)
    {
        this.itemType = itemType;

        this.itemCode = itemCode;

        this.onlyHaveOne = onlyHaveOne;

        this.cooldownTime = cooldownTime;

        this.autoAttack = autoAttack;

        this.dualWield = dualWield;

        this.physicalDamage = physicalDamage;

        this.physicalReach = physicalReach;

        this.projectileDamage = projectileDamage;

        this.projectileForce = projectileForce;

        this.projectileLifeTime = projectileLifeTime;

        this.magazineCapacity = magazineCapacity;

        this.magazinRest = magazinRest;

        this.reloadTime = reloadTime;
    }

    public ItemData(ItemData itemData)
    {
        itemType = itemData.itemType;

        itemCode = itemData.itemCode;

        onlyHaveOne = itemData.onlyHaveOne;

        count = itemData.count;

        countMax = itemData.countMax;

        cooldown = itemData.cooldown;

        cooldownTime = itemData.cooldownTime;

        consumTime = itemData.consumTime;

        autoAttack = itemData.autoAttack;

        dualWield = itemData.dualWield;

        physicalDamage = itemData.physicalDamage;

        physicalReach = itemData.physicalReach;

        projectileDamage = itemData.projectileDamage;

        projectileForce = itemData.projectileForce;

        projectileLifeTime = itemData.projectileLifeTime;

        magazineCapacity = itemData.magazineCapacity;

        magazinRest = itemData.magazinRest;

        reloadTime = itemData.reloadTime;
}

    public int Stack(int count)
    {
        this.count += count;

        int stackRest = this.count - countMax;

        if(stackRest > 0)
        {
            this.count -= stackRest;
        }

        else
        {
            stackRest = 0;
        }

        return stackRest;
    }
}