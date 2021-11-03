
using System.Collections;

public abstract class Consumable : InventoryItem
{
    protected virtual void Awake()
    {
        itemType = ItemType.consumable;
    }

    public override IEnumerator Consum()
    {
        if (_consum == null)
        {
            if (itemInfo.cooldownTimer <= 0)
            {
                _consum = _Consum();

                StartCoroutine(_consum);

                while (_consum != null) yield return null;
            }
        }
    }

    public override void StopConsum()
    {
        if (_consum != null)
        {
            StopCoroutine(_consum);

            _consum = null;
        }
    }

    protected IEnumerator _consum = null;

    protected abstract IEnumerator _Consum();
}