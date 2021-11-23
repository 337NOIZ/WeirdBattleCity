
using System.Collections;

using System.Collections.Generic;

public sealed class Shotgun : Weapon
{
    public override ItemCode itemCode { get { return ItemCode.shotgun; } }

    protected override ItemCode ammoItemCode { get { return ItemCode.shotgunAmmo; } }

    public override void Initialize()
    {
        base.Initialize();

        stance = "shotgunStance";

        drawingMotionTime = AnimationTools.FrameCountToSeconds(40);

        reloadingMotionTime = AnimationTools.FrameCountToSeconds(114);
    }

    public override void Initialize(ItemInfo itemInfo)
    {
        base.Initialize(itemInfo);

        skillMotionTimes = new List<float>()
        {
            AnimationTools.FrameCountToSeconds(45),
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

    protected override IEnumerator _Reload()
    {
        if (itemInfo.ammoCount < itemInfo.ammoCount_Max)
        {
            if (ammoItemInfo.stackCount > 0)
            {
                player.animator.SetFloat("reloadingMotionSpeed", reloadingMotionSpeed);

                player.animator.SetTrigger("reloadingMotion");

                player.animator.SetBool("isReloading", true);

                do
                {
                    yield return null;

                    if (player.animator.GetBool("isBulletInTubular") == true)
                    {
                        player.animator.SetBool("isBulletInTubular", false);

                        --ammoItemInfo.stackCount;

                        ++itemInfo.ammoCount;

                        if (ammoItemInfo.stackCount == 0 || itemInfo.ammoCount == itemInfo.ammoCount_Max)
                        {
                            player.animator.SetTrigger("stopReloadingMotion");
                        }
                    }
                }
                while (player.animator.GetBool("isReloading") == true);
            }
        }

        _reload = null;
    }
}