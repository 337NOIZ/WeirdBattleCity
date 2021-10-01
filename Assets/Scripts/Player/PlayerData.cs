
using System.Collections.Generic;

using UnityEngine;

[System.Serializable]

public class PlayerData
{
    [Space] public Vector3 transformPosition;

    [Space] public int healthPoint;

    public float movingSpeed;

    public float movingSpeedMultiply;

    public float runningSpeedMultiply;

    [Space] public float jumpForce;

    public int jumpCountMax;

    public Dictionary<ItemType, List<ItemData>> inventory;

    public Dictionary<ItemType, int> currentItemNumber;

    public PlayerData()
    {
        transformPosition = Vector3.zero;

        healthPoint = 100;

        movingSpeed = 3f;

        movingSpeedMultiply = 1f;

        runningSpeedMultiply = 2f;

        jumpForce = 5f;

        jumpCountMax = 1;

        inventory = new Dictionary<ItemType, List<ItemData>>();

        inventory.Add(ItemType.AMMO, new List<ItemData>());

        inventory[ItemType.AMMO].Add(new ItemData(ItemType.AMMO, ItemCode.PISTOL_AMMO, true, 0, 3));

        inventory.Add(ItemType.CONSUMABLE, new List<ItemData>());

        inventory[ItemType.CONSUMABLE].Add(new ItemData(ItemType.CONSUMABLE, ItemCode.MEDIKIT, true, 0, 3, 0.5f, 0f));

        inventory.Add(ItemType.WEAPON, new List<ItemData>());

        inventory[ItemType.WEAPON].Add(new ItemData(ItemType.WEAPON, ItemCode.BARE_FIST, true, 1, 1, 0.5f, true, true, 1, 1f));

        inventory[ItemType.WEAPON].Add(new ItemData(ItemType.WEAPON, ItemCode.PISTOL, false, 1, 1, 0.3f, true, false, 1, 1f, 5, 100f, 1f, 15, 15, 3f));

        currentItemNumber = new Dictionary<ItemType, int>();

        currentItemNumber.Add(ItemType.CONSUMABLE, 0);

        currentItemNumber.Add(ItemType.WEAPON, 0);
    }

    public PlayerData(PlayerData playerData)
    {
        transformPosition = playerData.transformPosition;

        healthPoint = playerData.healthPoint;

        movingSpeed = playerData.movingSpeed;

        jumpForce = playerData.jumpForce;

        jumpCountMax = playerData.jumpCountMax;

        inventory = new Dictionary<ItemType, List<ItemData>>(playerData.inventory);

        currentItemNumber = new Dictionary<ItemType, int>(playerData.currentItemNumber);
    }
}