
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
        if (itemInfo.stackCount > 0)
        {
            int healthPoint_Max = Mathf.FloorToInt(player.characterData.damageableData.healthPoint_Max * player.characterInfo.damageableInfo.healthPoint_Max_Multiple);

            if (player.characterInfo.damageableInfo.healthPoint < healthPoint_Max)
            {
                yield return new WaitForSeconds(itemData.consumTime);

                --itemInfo.stackCount;

                player.GetHealthPoint(Mathf.FloorToInt(itemData.resilience * itemInfo.resilience_Multiple));

                Cooldown();
            }
        }

        else
        {

        }

        _consum = null;
    }
}