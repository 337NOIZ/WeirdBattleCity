
using System.Collections.Generic;

using UnityEngine;

public sealed class PlayerInfo
{
    public Vector3 animator_LocalEulerAngles { get; set; }

    public Vector3 cameraPivot_LocalEulerAngles { get; set; }

    public float cameraPivot_Sensitivity { get; set; }

    public CharacterInfo characterInfo { get; private set; }

    public PlayerInventoryInfo playerInventoryInfo { get; private set; }

    public PlayerInfo()
    {
        animator_LocalEulerAngles = Vector3.zero;

        cameraPivot_LocalEulerAngles = Vector3.zero;

        cameraPivot_Sensitivity = 1f;

        characterInfo = new CharacterInfo(new DamageableInfo(), new TransformInfo(), new MovementInfo(), null);

        Dictionary<ItemType, List<ItemInfo>> itemInfos;

        itemInfos = new Dictionary<ItemType, List<ItemInfo>>();

        itemInfos.Add(ItemType.ammo, new List<ItemInfo>());

        itemInfos[ItemType.ammo].Add(new ItemInfo(ItemType.ammo, ItemCode.pistolAmmo, 135));

        itemInfos[ItemType.ammo].Add(new ItemInfo(ItemType.ammo, ItemCode.shotgunAmmo, 27));

        itemInfos[ItemType.ammo].Add(new ItemInfo(ItemType.ammo, ItemCode.submachineGunAmmo, 270));

        itemInfos.Add(ItemType.consumable, new List<ItemInfo>());

        itemInfos[ItemType.consumable].Add(new ItemInfo(ItemType.consumable, ItemCode.medikit, 1));

        itemInfos.Add(ItemType.weapon, new List<ItemInfo>());

        itemInfos[ItemType.weapon].Add(new ItemInfo(ItemType.weapon, ItemCode.pistol, 15));

        itemInfos[ItemType.weapon].Add(new ItemInfo(ItemType.weapon, ItemCode.shotgun, 3));

        itemInfos[ItemType.weapon].Add(new ItemInfo(ItemType.weapon, ItemCode.submachineGun, 30));

        playerInventoryInfo = new PlayerInventoryInfo(itemInfos);
    }

    public PlayerInfo(PlayerInfo playerInfo)
    {
        animator_LocalEulerAngles = playerInfo.animator_LocalEulerAngles;

        cameraPivot_LocalEulerAngles = playerInfo.cameraPivot_LocalEulerAngles;

        cameraPivot_Sensitivity = playerInfo.cameraPivot_Sensitivity;

        characterInfo = new CharacterInfo(playerInfo.characterInfo);

        playerInventoryInfo = new PlayerInventoryInfo(playerInfo.playerInventoryInfo);
    }
}