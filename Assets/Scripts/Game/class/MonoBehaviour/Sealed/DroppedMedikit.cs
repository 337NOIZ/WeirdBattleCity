
public sealed class DroppedMedikit : DroppedItem
{
    public override ItemType itemType { get => ItemType.consumable; }

    public override ItemCode itemCode { get => ItemCode.medikit; }
}