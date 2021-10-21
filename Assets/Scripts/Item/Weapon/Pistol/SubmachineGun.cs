
public class SubmachineGun : Weapon
{
    protected override void Awake()
    {
        base.Awake();

        itemCode = ItemCode.submachineGun;

        stance = "submachineGunStance";

        cooldownTime_Seconds = AnimationTools.FrameCountToSeconds(10);

        drawingTime_Seconds = AnimationTools.FrameCountToSeconds(40);

        reloadingTime_Seconds = AnimationTools.FrameCountToSeconds(126);
    }
}