
using System.Collections;

using System.Collections.Generic;

public sealed class Shotgun : Weapon
{
    public override ItemCode itemCode => ItemCode.shotgun;

    protected override ItemCode ammo_ItemCode => ItemCode.shotgunAmmo;

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

        Caching();
    }

    protected override IEnumerator ReloadRoutine()
    {
        if (ammo != null)
        {
            if (itemInfo.ammoCount < itemInfo.ammoCount_Max)
            {
                if (ammo.stackCount > 0)
                {
                    player.animator.SetFloat("reloadingMotionSpeed", reloadingMotionSpeed);

                    player.animator.SetTrigger("reloadingMotion");

                    player.animator.SetBool("isReloading", true);

                    player.animationTools.SetEventAction(BulletInTubular);

                    while (player.animator.GetBool("isReloading") == true) yield return null;
                }
            }
        }

        reloadRoutine = null;
    }

    private void BulletInTubular()
    {
        --ammo.stackCount;

        ++itemInfo.ammoCount;

        if (ammo.stackCount == 0 || itemInfo.ammoCount == itemInfo.ammoCount_Max)
        {
            player.animator.SetTrigger("finishReloading");
        }
    }
}