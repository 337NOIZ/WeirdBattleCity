
using System.Collections;

using UnityEngine;

public abstract class Weapon : Item
{
    [Space]

    [SerializeField] protected Transform muzzle = null;

    [Space]

    [SerializeField] protected Projectile projectile = null;

    protected virtual void Awake()
    {
        itemType = ItemType.weapon;
    }

    public override IEnumerator Store()
    {
        yield return StopAttack(0f);

        Reload(false);

        model.SetActive(false);

        animator.SetBool(stance, false);
    }

    public override void Attack(bool state)
    {
        if (animator.GetBool("isDrawing") == false && animator.GetBool("isReloading") == false)
        {
            if (state == true)
            {
                Reload(false);

                if (keepAiming != null)
                {
                    StopCoroutine(keepAiming);

                    keepAiming = null;
                }

                if (_attack == null)
                {
                    _attack = _Attack();

                    StartCoroutine(_attack);
                }
            }

            else
            {
                StartCoroutine(StopAttack(1.5f));
            }
        }
    }

    protected IEnumerator _attack = null;

    protected virtual IEnumerator _Attack()
    {
        animator.SetBool("isAiming", true);

        bool autoAttack = itemData.autoAttack;

        do
        {
            yield return null;

            if (itemInfo.magazine_AmmoCount > 0)
            {
                if (_cooldown == null)
                {
                    --itemInfo.magazine_AmmoCount;

                    int multishot = (int)itemData.projectileMultishot + ((itemData.projectileMultishot % 1f) > Random.Range(0f, 1f) ? 1 : 0);

                    for (int index = 0; index < multishot; ++index)
                    {
                        Projectile projectile = Instantiate(this.projectile, muzzle.position, muzzle.rotation * Quaternion.Euler(Random.insideUnitSphere * itemData.projectileDiffusion));

                        projectile.Launch(itemData.projectileDamage, itemData.projectileForce, itemData.projectileLifeTime_Seconds);
                    }

                    animator.SetFloat("cooldownSpeed", cooldownTime_Seconds / itemData.cooldownTime_Seconds * itemInfo.cooldownSpeed);

                    animator.SetTrigger("attack");

                    Cooldown();
                }
            }
        }
        while (autoAttack);
    }

    private IEnumerator StopAttack(float keepAimingTime_Seconds)
    {
        if (_attack != null)
        {
            StopCoroutine(_attack);

            _attack = null;

            while (_cooldown != null) yield return null;

            if (keepAiming != null)
            {
                StopCoroutine(keepAiming);
            }

            if (keepAimingTime_Seconds > 0f)
            {
                keepAiming = KeepAiming(keepAimingTime_Seconds);

                StartCoroutine(keepAiming);
            }

            else
            {
                animator.SetBool("isAiming", false);
            }
        }
    }

    protected IEnumerator keepAiming = null;

    protected IEnumerator KeepAiming(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        animator.SetBool("isAiming", false);

        keepAiming = null;
    }

    public override void Reload(bool state)
    {
        if (state == true)
        {
            StopAttack(0f);

            if (_reload == null)
            {
                //if (itemData.save.magazine_AmmoCount < itemData.magazine_AmmoCount_Max)
                //{
                    _reload = _Reload();

                    StartCoroutine(_reload);
                //}
            }
        }

        else if (_reload != null)
        {
            StopCoroutine(_reload);

            _reload = null;

            animator.SetBool("isReloading", false);
        }
    }

    protected IEnumerator _reload = null;

    protected virtual IEnumerator _Reload()
    {
        animator.SetFloat("reloadingSpeed", reloadingTime_Seconds / itemData.reloadingTime_Seconds * itemInfo.reloadingSpeed);

        animator.SetTrigger("reload");

        animator.SetBool("isReloading", true);

        while (animator.GetBool("isReloading") == true) yield return null;

        itemInfo.magazine_AmmoCount = itemData.magazine_AmmoCount_Max;

        _reload = null;
    }
}