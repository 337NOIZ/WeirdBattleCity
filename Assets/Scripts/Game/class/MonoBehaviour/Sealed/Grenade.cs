
using System.Collections;

using UnityEngine;

public sealed class Grenade : Consumable
{
    [SerializeField] private Muzzle _muzzle = null;

    public override ItemCode itemCode { get => ItemCode.grenade; }

    public override void Awaken(Character character)
    {
        base.Awaken(character);

        _animatorStance = "grenadeStance";
    }

    protected override IEnumerator Skill(int skillNumber)
    {
        switch (skillNumber)
        {
            case 0:

                if (_itemInfo.skillInfos[skillNumber].cooldownTimer == 0f)
                {
                    if (_itemInfo.stackCount > 0)
                    {
                        --_itemInfo.stackCount;

                        _animator.SetBool("isAiming", true);

                        _muzzle.LaunchProjectile(_character, _skillInfo.rangedInfo);

                        //_animatorWizard.AddEventAction(_animatorStance, _SkillEventAction_);

                        //_character.StartSkill(_animatorStance);

                        //while (_character.skill != null) yield return null;

                        //_animatorWizard.RemoveEventAction(_animatorStance);

                        _animator.SetBool("isAiming", false);
                    }

                    else
                    {

                    }
                }

                else
                {

                }

                break;

            default:

                break;
        }

        skill = null;

        yield return null;
    }
}