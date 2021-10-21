
using System.Collections;

public class Shotgun : Weapon
{
    protected override void Awake()
    {
        base.Awake();

        itemCode = ItemCode.shotgun;

        stance = "shotgunStance";

        cooldownTime_Seconds = AnimationTools.FrameCountToSeconds(45);

        drawingTime_Seconds = AnimationTools.FrameCountToSeconds(40);

        reloadingTime_Seconds = AnimationTools.FrameCountToSeconds(114);
    }

    protected override IEnumerator _Reload()
    {
        animator.SetFloat("reloadingSpeed", itemInfo.reloadingSpeed);

        animator.SetTrigger("reload");

        animator.SetBool("isReloading", true);

        do
        {
            yield return null;

            if (animator.GetBool("isBulletInTubular") == true)
            {
                animator.SetBool("isBulletInTubular", false);

                ++itemInfo.magazine_AmmoCount;

                if (itemInfo.magazine_AmmoCount == itemData.magazine_AmmoCount_Max)
                {
                    animator.SetTrigger("isMagazineFull");
                }
            }
        }
        while (animator.GetBool("isReloading") == true);

        _reload = null;
    }
}