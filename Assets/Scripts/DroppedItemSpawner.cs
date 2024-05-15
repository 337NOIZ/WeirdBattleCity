using UnityEngine;

public sealed class DroppedItemSpawner : Spawner
{
    public static DroppedItemSpawner instance { get; private set; }

    protected override void Awake()
    {
        instance = this;

        base.Awake();
    }

    public void Spawn(ItemCode itemCode, int spotNumber, int itemStackCount)
    {
        var droppedItem = ObjectPool.instance.Pop(itemCode);

        droppedItem.transform.parent = _spots[spotNumber];

        droppedItem.transform.localPosition = Vector3.zero;

        droppedItem.transform.localEulerAngles = Vector3.zero;

        droppedItem.gameObject.SetActive(true);

        droppedItem.Initialize(itemStackCount);

        ++spawnCount;
    }
}