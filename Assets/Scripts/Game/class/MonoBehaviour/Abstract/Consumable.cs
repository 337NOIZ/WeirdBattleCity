
using System.Collections;

public abstract class Consumable : InventoryItem
{
    public override ItemType itemType => ItemType.consumable;

    public override IEnumerator Skill(int skillNumber)
    {
        if (skillRoutine == null)
        {
            if (itemInfo.skillInfos[skillNumber].cooldownTimer == 0f)
            {
                skillRoutine = SkillRoutine(skillNumber);

                StartCoroutine(skillRoutine);

                while (skillRoutine != null) yield return null;
            }
        }
    }

    public override IEnumerator StopSkill()
    {
        if (skillRoutine != null)
        {
            StopCoroutine(skillRoutine);

            skillRoutine = null;
        }

        yield return null;
    }
}