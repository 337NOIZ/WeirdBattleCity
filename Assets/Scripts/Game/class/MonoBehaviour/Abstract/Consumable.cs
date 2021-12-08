
using System.Collections;

public abstract class Consumable : InventoryItem
{
    public override ItemType itemType { get => ItemType.consumable; }

    public override IEnumerator StopSkill()
    {
        if (skill != null)
        {
            StopCoroutine(skill);

            skill = null;
        }

        yield return null;
    }
}