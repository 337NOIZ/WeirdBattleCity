
using System.Collections;

using UnityEngine;

public sealed class Medikit : Consumable
{
    public override ItemCode itemCode { get { return ItemCode.medikit; } }

    protected override IEnumerator SkillRoutine(int skillNumber)
    {
        if (itemInfo.stackCount > 0)
        {
            if (player.characterInfo.damageableInfo.healthPoint < player.characterData.damageableData.healthPoint_Max)
            {
                yield return new WaitForSeconds(itemInfo.skillInfos[skillNumber].castingMotionTime);

                --itemInfo.stackCount;

                player.GainHealthPoint(player.characterData.damageableData.healthPoint_Max * 0.25f);

                Cooldown(skillNumber);
            }
        }

        else
        {

        }

        skillRoutine = null;
    }
}