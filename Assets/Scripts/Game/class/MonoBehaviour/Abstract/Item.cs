
using System.Collections.Generic;


using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public abstract ItemType itemType { get; }

    public abstract ItemCode itemCode { get; }

    protected ItemInfo _itemInfo;

    public virtual void Awaken(Character character) { }

    public virtual void Initialize(ItemInfo itemInfo) { }

    public virtual void Initialize(float stackCount) { }
}