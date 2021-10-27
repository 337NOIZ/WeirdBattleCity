
public class SubmachineGun : Weapon
{
    protected override void Awake()
    {
        base.Awake();

        itemCode = ItemCode.submachineGun;

        stance = "submachineGunStance";

        drawingTime = AnimationTools.FrameCountToSeconds(40);

        attackingTime = AnimationTools.FrameCountToSeconds(10);

        reloadingTime = AnimationTools.FrameCountToSeconds(116);
    }
    public override void Initialize(Player player)
    {
        base.Initialize(player);

        ammoInfo = player.playerInfo.inventoryInfo.itemInfos[ItemType.ammo][player.inventory.Search(ItemType.ammo, ItemCode.submachineGunAmmo)];
    }
}