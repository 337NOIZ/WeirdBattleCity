using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public abstract ItemType itemType { get; }

    public abstract ItemCode itemCode { get; }

    protected ItemInfo _itemInfo;
}