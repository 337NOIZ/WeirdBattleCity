
public class DroppedMedikit : DroppedItem
{
    protected void Awake()
    {
        itemType = ItemType.consumable;

        itemCode = ItemCode.medikit;
    }
}