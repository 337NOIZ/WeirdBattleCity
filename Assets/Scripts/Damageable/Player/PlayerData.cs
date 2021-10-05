
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

    public Dictionary<ItemType, List<ItemData>> inventory;

    public Dictionary<ItemType, int> currentItemNumber;

    public PlayerData()
    {
        transformPosition = Vector3.zero;

        cameraPivotLocalEulerAngles = Vector3.zero;

        animatorlocalEulerAngles = Vector3.zero;

        damageableData = new DamageableData(100, 100, 1f);

        movingSpeed = 3f;

        movingSpeedMultiply = 1f;

        runningSpeedMultiply = 2f;

        jumpForce = 5f;

        jumpCountMax = 1;

        inventory = new Dictionary<ItemType, List<ItemData>>();

        inventory.Add(ItemType.AMMO, new List<ItemData>());

        inventory[ItemType.AMMO].Add(new ItemData(ItemType.AMMO, ItemCode.PISTOL_AMMO, true, 15, 150));

        inventory.Add(ItemType.CONSUMABLE, new List<ItemData>());

        inventory[ItemType.CONSUMABLE].Add(new ItemData(ItemType.CONSUMABLE, ItemCode.MEDIKIT, true, 0, 3, 10f, 0f));

        inventory.Add(ItemType.WEAPON, new List<ItemData>());

        inventory[ItemType.WEAPON].Add(new ItemData(ItemType.WEAPON, ItemCode.BARE_FIST, true, 0.5f, true, true, 1, 1f));

        inventory[ItemType.WEAPON].Add(new ItemData(ItemType.WEAPON, ItemCode.PISTOL, false, 0.3f, false, false, 1, 1f, 5, 100f, 1f, 15, 15, 3f));

        currentItemNumber = new Dictionary<ItemType, int>();

        currentItemNumber.Add(ItemType.CONSUMABLE, 0);

        currentItemNumber.Add(ItemType.WEAPON, 0);
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

        inventory = new Dictionary<ItemType, List<ItemData>>(playerData.inventory);

        currentItemNumber = new Dictionary<ItemType, int>(playerData.currentItemNumber);
    }
}