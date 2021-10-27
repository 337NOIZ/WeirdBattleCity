
using System.Collections;

using UnityEngine;

public abstract class Weapon : InventoryItem
{
    [Space]

    [SerializeField] protected Transform muzzle = null;

    [Space]

    [SerializeField] protected Projectile projectile = null;

    protected float attackingTime;

    protected float reloadingTime;

    protected ItemInfo ammoInfo;

    protected float keepAimingTimer = 0f;

    protected float keepAimingTime = 1.5f;

    protected bool isAttacking = false;

    protected virtual void Awake()
    {
        itemType = ItemType.weapon;
    }
    public override IEnumerator Draw()
    {
        model.SetActive(true);

        player.animator.SetBool(stance, true);

        player.animator.SetFloat("drawingSpeed", drawingTime / itemData.drawingTime * itemInfo.drawingSpeed);

        player.animator.SetTrigger("draw");

        player.animator.SetBool("isDrawing", true);

        while (player.animator.GetBool("isDrawing") == true) yield return null;
    }
    public override IEnumerator Store()
    {
        yield return StopAttack(false);

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
    public override IEnumerator Attack()
    {
        player.animator.SetBool("isAiming", true);

        if (keepAiming != null)
        {
            StopCoroutine(keepAiming);

            keepAiming = null;
        }

        yield return new WaitForSeconds(0.05f);

        if (_attack == null)
        {
            _attack = _Attack();

            StartCoroutine(_attack);

            while (_attack != null) yield return null;
        }
    }
    protected IEnumerator _attack = null;

    protected virtual IEnumerator _Attack()
    {
        isAttacking = itemData.autoAttack;

        do
        {
            yield return null;

            if (itemInfo.ammoCount > 0)
            {
                --itemInfo.ammoCount;

                int multishot = Mathf.FloorToInt(itemData.projectileMultishot) + (itemData.projectileMultishot % 1f > Random.Range(0f, 1f) ? 1 : 0);

                for (int index = 0; index < multishot; ++index)
                {
                    Projectile projectile = Instantiate(this.projectile, muzzle.position, muzzle.rotation * Quaternion.Euler(Random.insideUnitSphere * itemData.projectileDiffusion));

                    projectile.Launch(itemData.projectileDamage, itemData.projectileForce, itemData.projectileLifeTime);
                }
                player.animator.SetFloat("attackingSpeed", attackingTime / itemData.attackingTime * itemInfo.attackingSpeed);

                player.animator.SetBool("isAttacking", true);

                while (player.animator.GetBool("isAttacking") == true) yield return null;
            }
        }
        while (isAttacking == true);

        _attack = null;
    }
    public override IEnumerator StopAttack(bool keepAiming)
    {
        isAttacking = false;

        while (_attack != null) yield return null;

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
        int magazine_AmmoCount_Max = Mathf.FloorToInt(itemData.ammoCount_Max * itemInfo.ammoCount_Max_Multiple);

        if (itemInfo.ammoCount < magazine_AmmoCount_Max)
        {
            if (ammoInfo.stackCount > 0)
            {
                player.animator.SetFloat("reloadingSpeed", reloadingTime / itemData.reloadingTime * itemInfo.reloadingSpeed);

                player.animator.SetTrigger("reload");

                player.animator.SetBool("isReloading", true);

                while (player.animator.GetBool("isReloading") == true) yield return null;

                int magazine_ammoCount_Needs = magazine_AmmoCount_Max - itemInfo.ammoCount;

                ammoInfo.stackCount -= magazine_ammoCount_Needs;

                if (ammoInfo.stackCount < 0)
                {
                    magazine_ammoCount_Needs += ammoInfo.stackCount;

                    ammoInfo.stackCount = 0;
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