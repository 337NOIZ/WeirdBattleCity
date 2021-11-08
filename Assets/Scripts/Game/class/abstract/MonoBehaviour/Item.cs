
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public ItemType itemType { get; protected set; }

    public ItemCode itemCode { get; protected set; }

    public ItemData itemData { get; set; } = null;

    public ItemInfo itemInfo { get; set; }

    public virtual void Initialize() { }

    public virtual void Initialize(ItemInfo itemInfo) { }

    public virtual void Initialize(int itemLevel) { }
}