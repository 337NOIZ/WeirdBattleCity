
using System.Collections.Generic;

using UnityEngine;

public sealed class DroppedItemSpawner : Spawner
{
    public static DroppedItemSpawner instance { get; private set; }

    private Dictionary<ItemCode, List<DroppedItem>> droppedItemPool = new Dictionary<ItemCode, List<DroppedItem>>();

    private Dictionary<int, DroppedItem> droppedItems = new Dictionary<int, DroppedItem>();

    protected override void Awake()
    {
        instance = this;

        base.Awake();
    }

    public void Spawn(ItemCode itemCode, int spotNumber, int itemLevel)
    {
        ++spawnCount;

        int index = 0;

        int count = droppedItemPool[itemCode].Count;

        DroppedItem droppedItem;

        for (; ; ++index)
        {
            if (index >= count)
            {
                droppedItem = Instantiate(GameMaster.instance.droppedItemPrefabs[itemCode], spots[spotNumber]);

                droppedItemPool[itemCode].Add(droppedItem);

                break;
            }

            if (droppedItemPool[itemCode][index].gameObject.activeSelf == false)
            {
                droppedItem = droppedItemPool[itemCode][index];

                droppedItem.transform.parent = spots[spotNumber];

                droppedItem.transform.localPosition = Vector3.zero;

                droppedItem.transform.localEulerAngles = Vector3.zero;

                droppedItem.gameObject.SetActive(true);

                break;
            }
        }

        if (droppedItems.ContainsKey(spotNumber) == true)
        {
            droppedItems[spotNumber].RecoveryToPool();
        }

        droppedItems.Add(spotNumber, droppedItem);

        droppedItem.Initialize(itemLevel);
    }
}