
using System.Collections;

using UnityEngine;

public sealed class SubmachineGun : Weapon
{
    [SerializeField] private Muzzle _muzzle = null;

    public override ItemCode itemCode => ItemCode.submachineGun;

    protected override ItemCode _ammo_itemCode => ItemCode.submachineGunAmmo;

    private bool isSkillStopped = false;

    public override void Awaken(Character character)
    {
        base.Awaken(character);

        _animatorStance = "submachineGunStance";
    }

    protected override IEnumerator Skill(int skillNumber)
    {
        _animator.SetBool("isAiming", true);

        yield return new WaitForSeconds(0.05f);

        if (_skillWizard.TrySetSkill(_skillInfo) == true)
        {
            _skillInfo = _skillInfos[skillNumber];

            switch (skillNumber)
            {
                case 0:

                    isSkillStopped = false;

                    while (isSkillStopped == false)
                    {
                        if (_itemInfo.ammoCount > 0)
                        {
                            --_itemInfo.ammoCount;

                            _muzzle.LaunchProjectile(_character, _skillInfo.rangedInfo);

                            _skillWizard.StartSkill(_animatorStance);

                            yield return _skillWizard.WaitForSkillEnd();
                        }
                    }

                    break;

                default:

                    break;
            }
        }

        skill = null;
    }

    public override IEnumerator StopSkill(bool keepAiming)
    {
        isSkillStopped = true;

        yield return base.StopSkill(keepAiming);
    }
}