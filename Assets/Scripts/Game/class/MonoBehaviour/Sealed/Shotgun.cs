
using System.Collections;

public sealed class Shotgun : Weapon
{
    public override ItemCode itemCode { get => ItemCode.Shotgun; }

    protected override string _motionTriggerName { get; } = "shotgun";

    protected override ItemCode _ammo_itemCode { get => ItemCode.ShotgunAmmo; }

    public override void Awaken(Character character)
    {
        base.Awaken(character);

        _muzzle.Awaken(_character.aim);
    }

    protected override IEnumerator _Reload()
    {
        StopSkill(false);

        while (_stopSkill != null) yield return null;

        if (_ammo != null)
        {
            if (_itemInfo.ammoCount < _itemInfo.ammoCount_Max)
            {
                if (_ammo.stackCount > 0)
                {
                    _animator.SetTrigger(_motionTriggerName);

                    _animator.SetFloat("reloadingMotionSpeed", _itemInfo.reloadingMotionSpeed);

                    AnimatorManager.AddEventAction(_motionTriggerName, ReloadingEventAction);

                    _animator.SetBool("isReloading", true);

                    while (_animator.GetBool("isReloading") == true) yield return null;
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

            AnimatorManager.RemoveEventAction(_motionTriggerName);
        }
    }
}