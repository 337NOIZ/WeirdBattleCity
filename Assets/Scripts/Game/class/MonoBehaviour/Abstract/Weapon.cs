
using System.Collections;

using UnityEngine;

public abstract class Weapon : InventoryItem
{
    public override ItemType itemType => ItemType.weapon;

    protected virtual ItemCode ammo_ItemCode { get; }

    protected ItemInfo ammo;

    protected float keepAimingTime = 1.5f;

    protected float keepAimingTimer = 0f;

    protected bool isUsingSkill = false;

    public override void Initialize()
    {
        base.Initialize();

        ammo = player.SearchItem(ItemType.ammo, ammo_ItemCode);
    }

    public override IEnumerator Draw()
    {
        model.SetActive(true);

        player.animator.SetBool(stance, true);

        player.animator.SetFloat("drawingMotionSpeed", drawingMotionSpeed);

        player.animator.SetTrigger("drawingMotion");

        player.animator.SetBool("isDrawing", true);

        while (player.animator.GetBool("isDrawing") == true) yield return null;
    }

    public override IEnumerator Store()
    {
        yield return StopSkill(false);

        StopReload();

        model.SetActive(false);

        player.animator.SetBool(stance, false);
    }

    public override IEnumerator Skill(int skillNumber)
    {
        if (skillRoutine == null)
        {
            StopKeepAming();

            StopReload();

            skillRoutine = SkillRoutine(skillNumber);

            StartCoroutine(skillRoutine);

            while (skillRoutine != null) yield return null;
        }
    }

    protected override IEnumerator SkillRoutine(int skillNumber)
    {
        player.animator.SetBool("isAiming", true);

        yield return new WaitForSeconds(0.05f);

        isUsingSkill = itemInfo.autoSkill;

        do
        {
            yield return null;

            if (itemInfo.ammoCount > 0)
            {
                --itemInfo.ammoCount;

                LaunchProjectile(skillNumber);

                player.animator.SetFloat("skillMotionSpeed", skillMotionSpeeds[skillNumber]);

                player.animator.SetBool("isUsingSkill", true);

                player.animator.SetInteger("skillNumber", skillNumber);

                player.animator.SetTrigger("skillMotion");

                yield return new WaitForSeconds(itemInfo.skillInfos[skillNumber].skillMotionTime);

                player.animator.SetBool("isUsingSkill", false);
            }
        }
        while (isUsingSkill == true);

        skillRoutine = null;
    }

    public override IEnumerator StopSkill(bool keepAiming)
    {
        if (skillRoutine != null)
        {
            //player.animator.SetBool("isUsingSkill", false);

            isUsingSkill = false;

            while (skillRoutine != null) yield return null;
        }

        if (keepAiming == true)
        {
            KeepAiming();
        }

        else
        {
            keepAimingTimer = 0f;
        }
    }

    protected void KeepAiming()
    {
        if (keepAimingRoutine != null)
        {
            keepAimingTimer = keepAimingTime;
        }

        else
        {
            keepAimingRoutine = KeepAimingRoutine();

            StartCoroutine(keepAimingRoutine);
        }
    }

    protected IEnumerator keepAimingRoutine = null;

    protected IEnumerator KeepAimingRoutine()
    {
        keepAimingTimer = keepAimingTime;

        while (keepAimingTimer > 0f)
        {
            yield return null;

            keepAimingTimer -= Time.deltaTime;
        }

        keepAimingTimer = 0f;

        player.animator.SetBool("isAiming", false);

        keepAimingRoutine = null;
    }

    protected void StopKeepAming()
    {
        if(keepAimingRoutine != null)
        {
            StopCoroutine(keepAimingRoutine);

            keepAimingRoutine = null;
        }
    }

    public override IEnumerator Reload()
    {
        if (reloadRoutine == null)
        {
            yield return StopSkill(false);

            reloadRoutine = ReloadRoutine();

            StartCoroutine(reloadRoutine);

            while (reloadRoutine != null) yield return null;
        }
    }

    protected IEnumerator reloadRoutine = null;

    protected virtual IEnumerator ReloadRoutine()
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

                    while (player.animator.GetBool("isReloading") == true) yield return null;

                    float magazine_ammoCount_Needs = itemInfo.ammoCount_Max - itemInfo.ammoCount;

                    ammo.stackCount -= magazine_ammoCount_Needs;

                    if (ammo.stackCount < 0)
                    {
                        magazine_ammoCount_Needs += ammo.stackCount;

                        ammo.stackCount = 0;
                    }

                    itemInfo.ammoCount += magazine_ammoCount_Needs;
                }
            }
        }

        reloadRoutine = null;
    }

    protected void StopReload()
    {
        if (reloadRoutine != null)
        {
            StopCoroutine(reloadRoutine);

            player.animator.SetBool("isReloading", false);

            reloadRoutine = null;
        }
    }
}