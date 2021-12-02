
using System.Collections.Generic;

using UnityEngine;

public sealed class PlayerInfo
{
    public CharacterInfo characterInfo { get; private set; }

    public sealed class InventoryInfo
    {
        public Dictionary<ItemType, List<ItemInfo>> itemInfos { get; private set; } = new Dictionary<ItemType, List<ItemInfo>>();

        public Dictionary<ItemType, int> currentItemNumbers { get; private set; }

        public InventoryInfo(PlayerData.InventoryData playerInventoryData)
        {
            foreach (KeyValuePair<ItemType, List<ItemData>> itemDatas in playerInventoryData.itemDatas)
            {
                itemInfos.Add(itemDatas.Key, new List<ItemInfo>());

                int index_Max = itemDatas.Value.Count;

                for (int index = 0; index < index_Max; ++index)
                {
                    itemInfos[itemDatas.Key].Add(new ItemInfo(itemDatas.Value[index], playerInventoryData.counts[itemDatas.Key][index]));
                }
            }

            currentItemNumbers = new Dictionary<ItemType, int>()
            {
                {ItemType.consumable, 0},

                {ItemType.weapon, 0},
            };
        }

        public InventoryInfo(InventoryInfo playerInventoryInfo)
        {
            foreach (KeyValuePair<ItemType, List<ItemInfo>> keyValuePair in playerInventoryInfo.itemInfos)
            {
                itemInfos.Add(keyValuePair.Key, keyValuePair.Value.ConvertAll(itemInfo => new ItemInfo(itemInfo)));
            }

            currentItemNumbers = new Dictionary<ItemType, int>(playerInventoryInfo.currentItemNumbers);
        }
    }
    
    public InventoryInfo inventoryInfo { get; private set; }

    public Vector3 animator_LocalEulerAngles { get; set; }

    public Vector3 cameraPivot_LocalEulerAngles { get; set; }

    public float cameraPivot_Sensitivity { get; set; }

    public PlayerInfo(PlayerData playerData)
    {
        characterInfo = new CharacterInfo(playerData.characterData, new TransformInfo());

        inventoryInfo = new InventoryInfo(playerData.inventoryData);

        animator_LocalEulerAngles = Vector3.zero;

        cameraPivot_LocalEulerAngles = Vector3.zero;

        cameraPivot_Sensitivity = 1f;
    }

    public PlayerInfo(PlayerInfo playerInfo)
    {
        characterInfo = new CharacterInfo(playerInfo.characterInfo);

        inventoryInfo = new InventoryInfo(playerInfo.inventoryInfo);

        animator_LocalEulerAngles = playerInfo.animator_LocalEulerAngles;

        cameraPivot_LocalEulerAngles = playerInfo.cameraPivot_LocalEulerAngles;

        cameraPivot_Sensitivity = playerInfo.cameraPivot_Sensitivity;
    }
}