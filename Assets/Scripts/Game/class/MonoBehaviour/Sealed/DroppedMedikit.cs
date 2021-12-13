
public sealed class DroppedMedikit : DroppedItem
{
    public override ItemType itemType { get => ItemType.Consumable; }

    public override ItemCode itemCode { get => ItemCode.Medikit; }
}