
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public ItemType itemType { get; protected set; }

    public ItemCode itemCode { get; protected set; }

    public ItemData itemData { get; set; }

    public ItemInfo itemInfo { get; set; }
}