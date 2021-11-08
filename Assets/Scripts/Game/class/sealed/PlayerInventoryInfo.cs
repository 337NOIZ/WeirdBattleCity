
using System.Collections.Generic;

public sealed class PlayerInventoryInfo
{
    public Dictionary<ItemType, List<ItemInfo>> itemInfos { get; private set; }

    public Dictionary<ItemType, int> lastItemNumber { get; private set; }

    public PlayerInventoryInfo(Dictionary<ItemType, List<ItemInfo>> itemInfos)
    {
        this.itemInfos = new Dictionary<ItemType, List<ItemInfo>>(itemInfos);

        lastItemNumber = new Dictionary<ItemType, int>();

        lastItemNumber.Add(ItemType.consumable, 0);

        lastItemNumber.Add(ItemType.weapon, 0);
    }

    public PlayerInventoryInfo(PlayerInventoryInfo inventoryInfo)
    {
        itemInfos = new Dictionary<ItemType, List<ItemInfo>>(inventoryInfo.itemInfos);

        lastItemNumber = new Dictionary<ItemType, int>(inventoryInfo.lastItemNumber);
    }
}