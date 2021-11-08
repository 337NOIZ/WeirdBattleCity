
using System.Collections;

using UnityEngine;

public sealed class Shotgun : Weapon
{
    protected override void Awake()
    {
        base.Awake();

        itemCode = ItemCode.shotgun;

        stance = "shotgunStance";

        drawingTime = AnimationTools.FrameCountToSeconds(40);

        attackingTime = AnimationTools.FrameCountToSeconds(45);

        reloadingTime = AnimationTools.FrameCountToSeconds(114);
    }

    protected override IEnumerator _Reload()
    {
        int magazine_AmmoCount_Max = Mathf.FloorToInt(itemData.ammoCount_Max * itemInfo.ammoCount_Max_Multiple);

        if (itemInfo.ammoCount < magazine_AmmoCount_Max)
        {
            if (ammoItemInfo.stackCount > 0)
            {
                player.animator.SetFloat("reloadingSpeed", reloadingTime / itemData.reloadingTime * itemInfo.reloadingSpeed);

                player.animator.SetTrigger("reload");

                player.animator.SetBool("isReloading", true);

                do
                {
                    yield return null;

                    if (player.animator.GetBool("isBulletInTubular") == true)
                    {
                        player.animator.SetBool("isBulletInTubular", false);

                        --ammoItemInfo.stackCount;

                        ++itemInfo.ammoCount;

                        if (ammoItemInfo.stackCount == 0 || itemInfo.ammoCount == magazine_AmmoCount_Max)
                        {
                            player.animator.SetTrigger("stopReload");
                        }
                    }
                }
                while (player.animator.GetBool("isReloading") == true);
            }
        }

        _reload = null;
    }
}