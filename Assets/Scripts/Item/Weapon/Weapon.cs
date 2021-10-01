
using System.Collections;

using UnityEngine;

public abstract class Weapon : Item
{
    private void Awake()
    {
        itemType = ItemType.WEAPON;
    }

    public override void Attack(bool state)
    {
        if (state == true)
        {
            if (itemData.autoAttack == true)
            {
                if (_attack == null)
                {
                    _attack = _Attack();

                    StartCoroutine(_attack);
                }
            }

            else
            {
                __Attack();
            }
        }

        else
        {
            if (itemData.autoAttack == true)
            {
                if (_attack != null)
                {
                    StopCoroutine(_attack);

                    _attack = null;
                }
            }
        }
    }

    private IEnumerator _attack = null;

    private IEnumerator _Attack()
    {
        while (true)
        {
            if (itemData.cooldown == 0f)
            {
                __Attack();
            }

            yield return null;
        }
    }

    protected abstract void __Attack();

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