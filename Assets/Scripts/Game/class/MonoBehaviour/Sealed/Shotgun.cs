
using System.Collections;

using UnityEngine;

public sealed class Shotgun : Weapon
{
    [SerializeField] private Muzzle _muzzle = null;

    public override ItemCode itemCode => ItemCode.shotgun;

    protected override ItemCode _ammo_itemCode => ItemCode.shotgunAmmo;

    public override void Awaken(Character character)
    {
        base.Awaken(character);

        _animatorStance = "shotgunStance";
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

                    _muzzle.LaunchProjectile(_character, _skillInfo.rangedInfo);

                    _skillWizard.StartSkill(_animatorStance);

                    yield return _skillWizard.WaitForSkillEnd();
                }

                break;

            default:

                break;
        }

        skill = null;
    }

    protected override IEnumerator Reload_()
    {
        if (_ammo != null)
        {
            if (_itemInfo.ammoCount < _itemInfo.ammoCount_Max)
            {
                if (_ammo.stackCount > 0)
                {
                    _animatorWizard.AddEventAction(_animatorStance, ReloadingEventAction);

                    _animator.SetBool("isReloading", true);

                    _animator.SetFloat("reloadingMotionSpeed", _itemInfo.reloadingMotionSpeed);

                    _animator.SetTrigger("reloadingMotion");

                    yield return CoroutineWizard.WaitForSeconds(_itemInfo.reloadingMotionTime);

                    _animator.SetBool("isReloading", false);
                }
            }
        }

        _reload = null;
    }

    private void ReloadingEventAction()
    {
        --_ammo.stackCount;

        ++_itemInfo.ammoCount;

        if (_ammo.stackCount == 0 || _itemInfo.ammoCount == _itemInfo.ammoCount_Max)
        {
            _animator.SetTrigger("finishReloading");
        }
    }
}