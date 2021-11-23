
using System.Collections.Generic;

public sealed class Pistol : Weapon
{
    public override ItemCode itemCode { get { return ItemCode.pistol; } }

    protected override ItemCode ammoItemCode { get { return ItemCode.pistolAmmo; } }

    public override void Initialize()
    {
        base.Initialize();

        stance = "pistolStance";

        drawingMotionTime = AnimationTools.FrameCountToSeconds(40);

        reloadingMotionTime = AnimationTools.FrameCountToSeconds(125);
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