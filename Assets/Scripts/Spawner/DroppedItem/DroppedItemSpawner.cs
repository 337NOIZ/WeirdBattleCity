
using System.Collections.Generic;

using UnityEngine;

public class DroppedItemSpawner : MonoBehaviour
{
    public static DroppedItemSpawner instance { get; private set; }

    private Dictionary<ItemCode, List<DroppedItem>> droppedItems = new Dictionary<ItemCode, List<DroppedItem>>();

    public int droppedItemCount { get; set; } = 0;

    private void Awake()
    {
        instance = this;
    }

    public void Spawn(ItemInfo itemInfo)
    {
        ItemCode itemCode = itemInfo.itemCode;

        int index = 0;

        int count = droppedItems[itemCode].Count;

        for(; ; ++index)
        {
            if(index < count)
            {
                droppedItems[itemCode].Add(Instantiate(GameManager.instance.droppedItemPrefabs[itemCode], transform));

                break;
            }

            if (droppedItems[itemCode][index].gameObject.activeSelf == false)
            {
                break;
            }
        }

        droppedItems[itemCode][index].Initialize(itemInfo);

        ++droppedItemCount;
    }
}