
using System.Collections.Generic;

public sealed class SubmachineGun : Weapon
{
    public override ItemCode itemCode { get { return ItemCode.submachineGun; } }

    protected override ItemCode ammoItemCode { get { return ItemCode.submachineGunAmmo; } }

    public override void Initialize()
    {
        base.Initialize();

        stance = "submachineGunStance";

        drawingMotionTime = AnimationTools.FrameCountToSeconds(40);

        reloadingMotionTime = AnimationTools.FrameCountToSeconds(116);
    }

    public override void Initialize(ItemInfo itemInfo)
    {
        base.Initialize(itemInfo);

        skillMotionTimes = new List<float>()
        {
            AnimationTools.FrameCountToSeconds(10),
        };

        skillMotionSpeeds = new List<float>()
        {
            0f,
        };

        skillMotionNames = new List<string>()
        {
            "skillMotion_0",
        };

        Caching();
    }
}