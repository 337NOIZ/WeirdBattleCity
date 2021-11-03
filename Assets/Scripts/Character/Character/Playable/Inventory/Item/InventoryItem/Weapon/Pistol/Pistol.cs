
public class Pistol : Weapon
{
    protected override void Awake()
    {
        base.Awake();

        itemCode = ItemCode.pistol;

        stance = "pistolStance";

        drawingTime = AnimationTools.FrameCountToSeconds(40);

        attackingTime = AnimationTools.FrameCountToSeconds(10);

        reloadingTime = AnimationTools.FrameCountToSeconds(125);
    }
    public override void Initialize(Player player)
    {
        base.Initialize(player);

        ammoInfo = player.playerInfo.inventoryInfo.itemInfos[ItemType.ammo][player.inventory.Search(ItemType.ammo, ItemCode.pistolAmmo)];
    }
}