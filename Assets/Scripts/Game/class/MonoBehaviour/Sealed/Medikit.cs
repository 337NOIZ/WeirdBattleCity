
using System.Collections;

public sealed class Medikit : Consumable
{
    public override ItemCode itemCode { get => ItemCode.Medikit; }

    public override void StartSkill(int skillNumber)
    {
        if (_skill == null)
        {
            _skill = Skill_(skillNumber);

            StartCoroutine(_skill);
        }
    }

    protected override IEnumerator Skill_(int skillNumber)
    {
        var skillInfo = _skillInfos[skillNumber];

        switch (skillNumber)
        {
            case 0:

                if (_itemInfo.stackCount > 0)
                {
                    /*if (_character._damageableInfo.healthPoint < _character._damageableInfo.healthPoint_Max)
                    {
                        --_itemInfo.stackCount;

                        _character.GetHealthPoint(_character._damageableInfo.healthPoint_Max * 0.25f);


                    }*/

                    _character.TakeStatusEffect(skillInfo.statusEffectInfos);
                }

                break;

            default:

                break;
        }

        _skill = null;

        yield break;
    }
}