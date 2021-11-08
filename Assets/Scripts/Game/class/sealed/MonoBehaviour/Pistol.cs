
public sealed class Pistol : Weapon
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
}