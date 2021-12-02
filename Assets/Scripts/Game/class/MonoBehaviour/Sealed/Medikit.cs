
using System.Collections;

public sealed class Medikit : Consumable
{
    public override ItemCode itemCode => ItemCode.medikit;

    protected override IEnumerator SkillRoutine(int skillNumber)
    {
        if (itemInfo.stackCount > 0)
        {
            if (player.characterInfo.damageableInfo.healthPoint < player.characterInfo.damageableInfo.healthPoint_Max)
            {
                --itemInfo.stackCount;

                player.GetHealthPoint(player.characterInfo.damageableInfo.healthPoint_Max * 0.25f);

                StartCoroutine(SkillCooldown(skillNumber));
            }
        }

        skillRoutine = null;

        yield break;
    }
}