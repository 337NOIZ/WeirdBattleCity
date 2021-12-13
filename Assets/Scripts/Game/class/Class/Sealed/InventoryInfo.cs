
using System.Collections.Generic;

public sealed class InventoryInfo
{
    public Dictionary<ItemType, List<ItemInfo>> itemInfos { get; private set; } = new Dictionary<ItemType, List<ItemInfo>>();

    public Dictionary<ItemType, int> currentItemNumbers { get; private set; }

    public InventoryInfo(InventoryData inventoryData)
    {
        foreach (KeyValuePair<ItemType, List<ItemData>> itemDatas in inventoryData.itemDatas)
        {
            itemInfos.Add(itemDatas.Key, new List<ItemInfo>());

            int index_Max = itemDatas.Value.Count;

            for (int index = 0; index < index_Max; ++index)
            {
                itemInfos[itemDatas.Key].Add(new ItemInfo(itemDatas.Value[index], inventoryData.counts[itemDatas.Key][index]));
            }
        }

        currentItemNumbers = new Dictionary<ItemType, int>()
        {
            {ItemType.Consumable, 0},

            {ItemType.Weapon, 0},
        };
    }

    public InventoryInfo(InventoryInfo inventoryInfo)
    {
        foreach (KeyValuePair<ItemType, List<ItemInfo>> keyValuePair in inventoryInfo.itemInfos)
        {
            itemInfos.Add(keyValuePair.Key, keyValuePair.Value.ConvertAll(itemInfo => new ItemInfo(itemInfo)));
        }

        currentItemNumbers = new Dictionary<ItemType, int>(inventoryInfo.currentItemNumbers);
    }
}