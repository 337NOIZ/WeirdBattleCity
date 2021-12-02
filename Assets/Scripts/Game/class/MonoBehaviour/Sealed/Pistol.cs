
using System.Collections.Generic;

public sealed class Pistol : Weapon
{
    public override ItemCode itemCode => ItemCode.pistol;

    protected override ItemCode ammo_ItemCode => ItemCode.pistolAmmo;

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

        Caching();
    }
}