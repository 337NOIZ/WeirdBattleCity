
using System.Collections.Generic;

using UnityEngine;

public sealed class PlayerData
{
    public CharacterData characterData { get; private set; }

    public sealed class InventoryData
    {
        public Dictionary<ItemType, List<ItemData>> itemDatas { get; private set; } = new Dictionary<ItemType, List<ItemData>>();

        public Dictionary<ItemType, List<int>> counts { get; private set; }

        public InventoryData(Dictionary<ItemType, List<ItemData>> itemDatas, Dictionary<ItemType, List<int>> counts)
        {
            foreach (KeyValuePair<ItemType, List<ItemData>> keyValuePair in itemDatas)
            {
                this.itemDatas.Add(keyValuePair.Key, keyValuePair.Value.ConvertAll(itemData => new ItemData(itemData)));
            }

            this.counts = new Dictionary<ItemType, List<int>>(counts);
        }

        public InventoryData(InventoryData playerInventoryData)
        {
            foreach (KeyValuePair<ItemType, List<ItemData>> keyValuePair in playerInventoryData.itemDatas)
            {
                itemDatas.Add(keyValuePair.Key, keyValuePair.Value.ConvertAll(itemData => new ItemData(itemData)));
            }

            counts = new Dictionary<ItemType, List<int>>(playerInventoryData.counts);
        }
    }

    public InventoryData inventoryData { get; private set; }

    public PlayerData(CharacterData characterData, InventoryData inventoryData)
    {
        this.characterData = characterData;

        this.inventoryData = new InventoryData(inventoryData);
    }

    public PlayerData(PlayerData playerData)
    {
        inventoryData = new InventoryData(playerData.inventoryData);
    }
}