
public sealed class DroppedMedikit : DroppedItem
{
    public override ItemType itemType { get { return ItemType.consumable; } }

    public override ItemCode itemCode { get { return ItemCode.medikit; } }
}