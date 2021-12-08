
using System.Collections;

using UnityEngine;

public sealed class Pistol : Weapon
{
    [SerializeField] private Muzzle _muzzle = null;

    public override ItemCode itemCode { get => ItemCode.pistol; }

    protected override ItemCode _ammo_itemCode { get => ItemCode.pistolAmmo; }

    public override void Awaken(Character character)
    {
        base.Awaken(character);

        _animatorStance = "pistolStance";
    }

    protected override IEnumerator Skill(int skillNumber)
    {
        _animator.SetBool("isAiming", true);

        yield return new WaitForSeconds(0.05f);

        switch (skillNumber)
        {
            case 0:

                if (_itemInfo.ammoCount > 0)
                {
                    --_itemInfo.ammoCount;

                    _skillInfo = _skillInfos[skillNumber];

                    _muzzle.LaunchProjectile(_character, _skillInfo.rangedInfo);

                    _skillWizard.TrySetSkill(_skillInfo);

                    _skillWizard.StartSkill(_animatorStance);

                    yield return _skillWizard.WaitForSkillEnd();
                }

                break;

            default:

                break;
        }

        skill = null;
    }
}