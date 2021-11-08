
public sealed class DroppedMedikit : DroppedItem
{
    private void Awake()
    {
        itemType = ItemType.consumable;

        itemCode = ItemCode.medikit;
    }
}