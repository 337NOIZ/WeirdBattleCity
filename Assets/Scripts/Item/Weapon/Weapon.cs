
using System.Collections;

using UnityEngine;

public abstract class Weapon : Item
{
    protected bool isAttacking = false;

    private void Awake()
    {
        itemType = ItemType.WEAPON;
    }

    public override void Attack(bool state)
    {
        if (state == true)
        {
            if (_attack == null)
            {
                animator.SetBool("isAttacking", true);

                isAttacking = itemData.autoAttack;

                _attack = _Attack();

                StartCoroutine(_attack);
            }
        }

        else
        {
            _attack = null;

            isAttacking = false;

            animator.SetBool("isAttacking", false);
        }
    }

    private IEnumerator _attack = null;

    protected abstract IEnumerator _Attack();

    public override bool Reload(bool state)
    {
        if (state == true)
        {
            if (itemData.magazinRest < itemData.magazineCapacity)
            {
                if (_reload == null)
                {
                    _reload = _Reload();

                    StartCoroutine(_reload);

                    return true;
                }
            }
        }

        else if (_reload != null)
        {
            StopCoroutine(_reload);

            _reload = null;
        }

        return false;
    }

    private IEnumerator _reload = null;

    private IEnumerator _Reload()
    {
        yield return new WaitForSeconds(itemData.reloadTime);

        __Reload();

        _reload = null;
    }

    protected virtual void __Reload()
    {
        itemData.magazinRest = itemData.magazineCapacity;
    }
}