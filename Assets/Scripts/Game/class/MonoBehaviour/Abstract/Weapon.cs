
using System.Collections;

using UnityEngine;

public abstract class Weapon : InventoryItem
{
    [Space]

    [SerializeField] protected Transform muzzle = null;

    public override ItemType itemType { get { return ItemType.weapon; } }

    protected virtual ItemCode ammoItemCode { get; }

    protected ItemInfo ammoItemInfo;

    protected float keepAimingTimer = 0f;

    protected float keepAimingTime = 1.5f;

    protected bool isUsingSkill = false;

    public override void Initialize()
    {
        base.Initialize();

        ammoItemInfo = player.playerInfo.playerInventoryInfo.itemInfos[ItemType.ammo][player.playerInventory.Search(ItemType.ammo, ammoItemCode)];
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

    protected IEnumerator keepAiming = null;

    protected IEnumerator KeepAiming()
    {
        keepAimingTimer = keepAimingTime;

        while (keepAimingTimer > 0f)
        {
            yield return null;

            keepAimingTimer -= Time.deltaTime;
        }

        keepAimingTimer = 0f;

        player.animator.SetBool("isAiming", false);

        keepAiming = null;
    }

    public override IEnumerator Skill(int skillNumber)
    {
        player.animator.SetBool("isAiming", true);

        if (keepAiming != null)
        {
            StopCoroutine(keepAiming);

            keepAiming = null;
        }

        yield return new WaitForSeconds(0.05f);

        if (skillRoutine == null)
        {
            skillRoutine = SkillRoutine(skillNumber);

            StartCoroutine(skillRoutine);

            while (skillRoutine != null) yield return null;
        }
    }

    protected override IEnumerator SkillRoutine(int skillNumber)
    {
        isUsingSkill = itemInfo.autoAttack;

        var rangedInfo = itemInfo.skillInfos[skillNumber].rangedInfo;

        do
        {
            yield return null;

            if (itemInfo.ammoCount > 0)
            {
                --itemInfo.ammoCount;

                Projectile projectile;

                int count = Mathf.FloorToInt(rangedInfo.division);

                for (int index = 0; index < count; ++index)
                {
                    projectile = ObjectPool.instance.Pop(rangedInfo.projectileCode);

                    projectile.transform.position = muzzle.position;

                    projectile.transform.rotation = muzzle.rotation;

                    if (rangedInfo.diffusion > 0f)
                    {
                        projectile.transform.rotation *= Quaternion.Euler(Random.insideUnitSphere * rangedInfo.diffusion);
                    }

                    projectile.Launch(player, rangedInfo.force, rangedInfo.lifeTime, rangedInfo.damage, rangedInfo.statusEffectInfos);
                }

                player.animator.SetFloat("skillMotionSpeed", skillMotionSpeeds[skillNumber]);

                player.animator.SetBool("isUsingSkill", true);

                player.animator.SetTrigger(skillMotionNames[skillNumber]);

                while (player.animator.GetBool("isUsingSkill") == true) yield return null;
            }
        }
        while (isUsingSkill == true);

        skillRoutine = null;
    }

    public override IEnumerator StopSkill(bool keepAiming)
    {
        isUsingSkill = false;

        while (skillRoutine != null) yield return null;

        if (keepAiming == true)
        {
            if (this.keepAiming != null)
            {
                keepAimingTimer = keepAimingTime;
            }

            else
            {
                this.keepAiming = KeepAiming();

                StartCoroutine(this.keepAiming);
            }
        }

        else
        {
            if (this.keepAiming != null)
            {
                StopCoroutine(this.keepAiming);

                keepAimingTimer = 0f;

                this.keepAiming = null;
            }

            player.animator.SetBool("isAiming", false);
        }
    }

    public override IEnumerator Reload()
    {
        if (_reload == null)
        {
            _reload = _Reload();

            StartCoroutine(_reload);

            while (_reload != null) yield return null;
        }
    }

    protected IEnumerator _reload = null;

    protected virtual IEnumerator _Reload()
    {
        if (itemInfo.ammoCount < itemInfo.ammoCount_Max)
        {
            if (ammoItemInfo.stackCount > 0)
            {
                player.animator.SetFloat("reloadingMotionSpeed", reloadingMotionSpeed);

                player.animator.SetTrigger("reloadingMotion");

                player.animator.SetBool("isReloading", true);

                while (player.animator.GetBool("isReloading") == true) yield return null;

                int magazine_ammoCount_Needs = itemInfo.ammoCount_Max - itemInfo.ammoCount;

                ammoItemInfo.stackCount -= magazine_ammoCount_Needs;

                if (ammoItemInfo.stackCount < 0)
                {
                    magazine_ammoCount_Needs += ammoItemInfo.stackCount;

                    ammoItemInfo.stackCount = 0;
                }

                itemInfo.ammoCount += magazine_ammoCount_Needs;
            }
        }

        _reload = null;
    }

    public override void StopReload()
    {
        if (_reload != null)
        {
            StopCoroutine(_reload);

            player.animator.SetBool("isReloading", false);

            _reload = null;
        }
    }
}