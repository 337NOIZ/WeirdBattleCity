
using System.Collections.Generic;

public sealed class PlayerInventoryInfo
{
    public Dictionary<ItemType, List<ItemInfo>> itemInfos { get; private set; } = new Dictionary<ItemType, List<ItemInfo>>();

    public Dictionary<ItemType, int> lastItemNumber { get; private set; }

    public PlayerInventoryInfo(PlayerInventoryData playerInventoryData)
    {
        foreach (KeyValuePair<ItemType, List<ItemData>> keyValuePair in playerInventoryData.itemDatas)
        {
            itemInfos.Add(keyValuePair.Key, new List<ItemInfo>());

            int count = keyValuePair.Value.Count;

            for (int index = 0; index < count; ++index)
            {
                var itemInfo = new ItemInfo(keyValuePair.Value[index]);

                itemInfo.Initialize(playerInventoryData.counts[keyValuePair.Key][index]);

                itemInfos[keyValuePair.Key].Add(itemInfo);
            }
        }

        lastItemNumber = new Dictionary<ItemType, int>();

        lastItemNumber.Add(ItemType.consumable, 0);

        lastItemNumber.Add(ItemType.weapon, 0);
    }

    public PlayerInventoryInfo(PlayerInventoryInfo playerInventoryInfo)
    {
        foreach (KeyValuePair<ItemType, List<ItemInfo>> keyValuePair in playerInventoryInfo.itemInfos)
        {
            itemInfos.Add(keyValuePair.Key, keyValuePair.Value.ConvertAll(itemInfo => new ItemInfo(itemInfo)));
        }

        lastItemNumber = new Dictionary<ItemType, int>(playerInventoryInfo.lastItemNumber);
    }
}