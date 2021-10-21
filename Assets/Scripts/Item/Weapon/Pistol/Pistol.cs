
public class Pistol : Weapon
{
    protected override void Awake()
    {
        base.Awake();

        itemCode = ItemCode.pistol;

        stance = "pistolStance";

        cooldownTime_Seconds = AnimationTools.FrameCountToSeconds(10);

        drawingTime_Seconds = AnimationTools.FrameCountToSeconds(40);

        reloadingTime_Seconds = AnimationTools.FrameCountToSeconds(104);
    }
}