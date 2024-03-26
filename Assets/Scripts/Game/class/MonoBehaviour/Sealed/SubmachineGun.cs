
using System.Collections;

using UnityEngine;

public sealed class SubmachineGun : Weapon
{
    public override ItemCode itemCode => ItemCode.SubmachineGun;

    protected override string _motionTriggerName { get; } = "submachineGun";

    protected override ItemCode _ammo_itemCode => ItemCode.SubmachineGunAmmo;

    private bool isUsingSkill = false;

    public override void Awaken(Character character)
    {
        base.Awaken(character);

        _muzzle.Awaken(_character.aim);
    }

    protected override IEnumerator _Skill(int skillNumber)
    {
        _animator.SetBool("isAiming", true);

        yield return new WaitForSeconds(0.05f);

        if (_skillWizard.TrySetSkill(_skillInfos[skillNumber]) == true)
        {
            var skillInfo_RangedInfo = _skillInfos[skillNumber].rangedInfo;

            var projectileInfo = skillInfo_RangedInfo.projectileInfo;

            switch (skillNumber)
            {
                case 0:

                    isUsingSkill = true;

                    do
                    {
                        if (_itemInfo.ammoCount > 0)
                        {
                            --_itemInfo.ammoCount;

                            _muzzle.LaunchProjectile
                            (
                                _character,

                                (hitBox) =>
                                {
                                    hitBox.character.TakeAttack(_character, projectileInfo.damage, null);
                                },

                                skillInfo_RangedInfo
                            );

                            _skillWizard.StartSkill(_motionTriggerName);

                            yield return _skillWizard.WaitForSkillEnd();
                        }

                        else
                        {
                            break;
                        }
                    }
                    while (isUsingSkill == true);

                    break;

                default:

                    break;
            }
        }

        _skill = null;
    }

    protected override IEnumerator _StopSkill(bool keepAiming)
    {
        isUsingSkill = false;

        yield return base._StopSkill(keepAiming);
    }
}