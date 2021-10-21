
using System.Collections;

public abstract class Consumable : Item
{
    protected virtual void Awake()
    {
        itemType = ItemType.consumable;
    }

    public override void Consum(bool state)
    {
        if (state == true)
        {
            if (_consum == null)
            {
                if (itemInfo.stackCount > 0)
                {
                    if (itemInfo.cooldownTime <= 0)
                    {
                        _consum = _Consum();

                        StartCoroutine(_consum);
                    }
                }
            }
        }

        else if(_consum != null)
        {
            StopCoroutine(_consum);

            _consum = null;
        }
    }

    protected IEnumerator _consum = null;

    protected abstract IEnumerator _Consum();
}