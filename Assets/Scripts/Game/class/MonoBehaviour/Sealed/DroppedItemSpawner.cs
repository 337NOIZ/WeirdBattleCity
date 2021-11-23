
using UnityEngine;

public sealed class DroppedItemSpawner : Spawner
{
    public static DroppedItemSpawner instance { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        instance = this;
    }

    public void Spawn(ItemCode itemCode, int spotNumber, int itemLevel)
    {
        var droppedItem = ObjectPool.instance.Pop(itemCode);

        droppedItem.transform.parent = spots[spotNumber];

        droppedItem.transform.localPosition = Vector3.zero;

        droppedItem.transform.localEulerAngles = Vector3.zero;

        droppedItem.Initialize(itemLevel);

        ++spawnCount;
    }
}