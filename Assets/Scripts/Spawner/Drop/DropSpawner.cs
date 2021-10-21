
using System.Collections.Generic;

using UnityEngine;

public class DropSpawner : Spawner
{
    public static DropSpawner instance { get; private set; }

    [Space]

    [SerializeField] private GameObject _drops = null;

    private Dictionary<ItemCode, List<Drop>> drops = new Dictionary<ItemCode, List<Drop>>();

    public int itemCount { get; set; } = 0;

    private void Awake()
    {
        instance = this;
    }

    public void Initialize()
    {
        _drops.SetActive(true);

        var drops = _drops.GetComponentsInChildren<Drop>();

        int length = drops.Length;

        for (int index = 0; index < length; ++index)
        {
            drops[index].Initialize();

            ItemCode enemyCode = drops[index].itemCode;

            if (this.drops.ContainsKey(enemyCode) == false)
            {
                this.drops.Add(enemyCode, new List<Drop>());
            }

            this.drops[enemyCode].Add(drops[index]);

            drops[index].gameObject.SetActive(false);
        }
    }

    public void Spawn(ItemInfo itemInfo, Vector3 transformPosition)
    {
        ItemCode itemCode = itemInfo.itemCode;

        int count = drops[itemCode].Count;

        for (int index = 0; index < count; ++index)
        {
            if (drops[itemCode][index].gameObject.activeSelf == false)
            {
                ++itemCount;

                drops[itemCode][index].Spawn(itemInfo, transformPosition);
            }
        }
    }
}