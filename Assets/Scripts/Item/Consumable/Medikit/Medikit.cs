
using System.Collections;

using UnityEngine;

public class Medikit : Consumable
{
    protected override void Awake()
    {
        base.Awake();

        itemCode = ItemCode.medikit;
    }

    protected override IEnumerator _Consum()
    {
        yield return new WaitForSeconds(itemData.consumTime_Seconds);

        Cooldown();

        --itemInfo.stackCount;

        _consum = null;
    }
}