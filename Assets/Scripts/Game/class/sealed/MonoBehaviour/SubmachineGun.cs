
public sealed class SubmachineGun : Weapon
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
}