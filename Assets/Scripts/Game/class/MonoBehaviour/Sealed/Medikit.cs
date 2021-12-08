
using System.Collections;

public sealed class Medikit : Consumable
{
    public override ItemCode itemCode { get => ItemCode.medikit; }

    protected override IEnumerator Skill(int skillNumber)
    {
        switch (skillNumber)
        {
            case 0:

                if (_itemInfo.stackCount > 0)
                {
                    if (_character.damageableInfo.healthPoint < _character.damageableInfo.healthPoint_Max)
                    {
                        --_itemInfo.stackCount;

                        _character.GetHealthPoint(_character.damageableInfo.healthPoint_Max * 0.25f);


                    }
                }

                break;

            default:

                break;
        }

        skill = null;

        yield break;
    }
}