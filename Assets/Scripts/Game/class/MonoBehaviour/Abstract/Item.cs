
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public abstract ItemType itemType { get; }

    public abstract ItemCode itemCode { get; }

    public ItemInfo itemInfo { get; set; }

    public virtual void Initialize() { }

    public virtual void Initialize(ItemInfo itemInfo) { }

    public virtual void Initialize(int itemLevel) { }
}