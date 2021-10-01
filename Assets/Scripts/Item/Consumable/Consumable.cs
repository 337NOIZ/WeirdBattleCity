
using System.Collections;

using UnityEngine;

public abstract class Consumable : Item
{
    private void Awake()
    {
        itemType = ItemType.CONSUMABLE;
    }

    public override bool Consum(bool state)
    {
        if (state == true)
        {
            if (_consum == null)
            {
                if (itemData.count > 0)
                {
                    if (itemData.cooldown <= 0)
                    {
                        _consum = _Consum();

                        StartCoroutine(_consum);

                        return true;
                    }
                }
            }
        }

        else if(_consum != null)
        {
            StopCoroutine(_consum);

            _consum = null;
        }

        return false;
    }

    private IEnumerator _consum = null;

    private IEnumerator _Consum()
    {
        yield return new WaitForSeconds(itemData.consumTime);

        __Consum();

        _consum = null;
    }

    protected abstract void __Consum();
}