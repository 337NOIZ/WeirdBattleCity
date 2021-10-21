
using System.Collections.Generic;

using UnityEngine;

[System.Serializable]

public class PlayerData
{
    [Space]
    
    public Vector3 transformPosition;

    [Space]
    
    public Vector3 cameraPivotLocalEulerAngles;

    [Space]
    
    public Vector3 animatorlocalEulerAngles;

    [Space]

    public DamageableData damageableData;

    [Space]
    
    public float movingSpeed;

    public float movingSpeedMultiply;

    [Space]

    public float runningSpeedMultiply;

    [Space]
    
    public float jumpForce;

    public int jumpCountMax;

    public Dictionary<ItemType, List<ItemInfo>> inventory;

    public Dictionary<ItemType, int> currentItemNumber;

    public PlayerData()
    {
        transformPosition = Vector3.zero;

        cameraPivotLocalEulerAngles = Vector3.zero;

        animatorlocalEulerAngles = Vector3.zero;

        damageableData = new DamageableData(100, 100, 1f, 0);

        movingSpeed = 3f;

        movingSpeedMultiply = 1f;

        runningSpeedMultiply = 2f;

        jumpForce = 5f;

        jumpCountMax = 1;

        inventory = new Dictionary<ItemType, List<ItemInfo>>();

        inventory.Add(ItemType.ammo, new List<ItemInfo>());

        inventory[ItemType.ammo].Add(new ItemInfo(ItemType.ammo, ItemCode.pistolAmmo, 135));

        inventory[ItemType.ammo].Add(new ItemInfo(ItemType.ammo, ItemCode.shotgunAmmo, 27));

        inventory[ItemType.ammo].Add(new ItemInfo(ItemType.ammo, ItemCode.submachineGunAmmo, 270));

        inventory.Add(ItemType.consumable, new List<ItemInfo>());

        inventory[ItemType.consumable].Add(new ItemInfo(ItemType.consumable, ItemCode.medikit, 0));

        inventory.Add(ItemType.weapon, new List<ItemInfo>());

        inventory[ItemType.weapon].Add(new ItemInfo(ItemType.weapon, ItemCode.pistol, 1, 15));

        inventory[ItemType.weapon].Add(new ItemInfo(ItemType.weapon, ItemCode.shotgun, 1, 3));

        inventory[ItemType.weapon].Add(new ItemInfo(ItemType.weapon, ItemCode.submachineGun, 1, 30));

        currentItemNumber = new Dictionary<ItemType, int>();

        currentItemNumber.Add(ItemType.consumable, 0);

        currentItemNumber.Add(ItemType.weapon, 0);
    }

    public PlayerData(PlayerData playerData)
    {
        transformPosition = playerData.transformPosition;

        cameraPivotLocalEulerAngles = playerData.cameraPivotLocalEulerAngles;

        damageableData = playerData.damageableData;

        movingSpeed = playerData.movingSpeed;

        movingSpeedMultiply = playerData.movingSpeedMultiply;

        runningSpeedMultiply = playerData.runningSpeedMultiply;

        jumpForce = playerData.jumpForce;

        jumpCountMax = playerData.jumpCountMax;

        inventory = new Dictionary<ItemType, List<ItemInfo>>(playerData.inventory);

        currentItemNumber = new Dictionary<ItemType, int>(playerData.currentItemNumber);
    }
}