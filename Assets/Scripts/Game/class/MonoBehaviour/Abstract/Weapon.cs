
using System.Collections;

using UnityEngine;

public abstract class Weapon : InventoryItem
{
    public override ItemType itemType => ItemType.weapon;

    protected virtual ItemCode _ammo_itemCode { get; }

    protected ItemInfo _ammo;

    protected float _keepAimingTime = 1.5f;

    protected float _keepAimingTimer = 0f;

    public override void Awaken(Character character)
    {
        base.Awaken(character);

        _ammo = character.FindItem(ItemType.ammo, _ammo_itemCode);
    }

    public override IEnumerator Draw()
    {
        _model.SetActive(true);

        _animator.SetBool(_animatorStance, true);

        _animator.SetBool("isDrawing", true);

        _animator.SetFloat("drawingMotionSpeed", _itemInfo.drawingMotionSpeed);

        _animator.SetTrigger("drawingMotion");

        yield return CoroutineWizard.WaitForSeconds(_itemInfo.drawingMotionTime);

        _animator.SetBool("isDrawing", false);
    }

    public override IEnumerator Store()
    {
        yield return StopSkill(false);

        StopReload();

        _model.SetActive(false);

        _animator.SetBool(_animatorStance, false);
    }

    public override void StartSkill(int skillNumber)
    {
        StopKeepAming();

        StopReload();

        base.StartSkill(skillNumber);
    }

    public override IEnumerator StopSkill(bool keepAiming)
    {
        _skillWizard.TryStopSkill();

        while (skill != null) yield return null;

        if (keepAiming == true)
        {
            KeepAiming();
        }

        else
        {
            _keepAimingTimer = 0f;
        }
    }

    public override IEnumerator Reload()
    {
        if (_reload == null)
        {
            yield return StopSkill(false);

            _reload = Reload_();

            StartCoroutine(_reload);

            while (_reload != null) yield return null;
        }
    }

    protected IEnumerator _reload = null;

    protected virtual IEnumerator Reload_()
    {
        if (_ammo != null)
        {
            if (_itemInfo.ammoCount < _itemInfo.ammoCount_Max)
            {
                if (_ammo.stackCount > 0)
                {
                    _animator.SetBool("isReloading", true);

                    _animator.SetFloat("reloadingMotionSpeed", _itemInfo.reloadingMotionSpeed);

                    _animator.SetTrigger("reloadingMotion");

                    yield return CoroutineWizard.WaitForSeconds(_itemInfo.reloadingMotionTime);

                    _animator.SetBool("isReloading", false);

                    float magazine_ammoCount_Needs = _itemInfo.ammoCount_Max - _itemInfo.ammoCount;

                    _ammo.stackCount -= magazine_ammoCount_Needs;

                    if (_ammo.stackCount < 0)
                    {
                        magazine_ammoCount_Needs += _ammo.stackCount;

                        _ammo.stackCount = 0;
                    }

                    _itemInfo.ammoCount += magazine_ammoCount_Needs;
                }
            }
        }

        _reload = null;
    }

    protected void KeepAiming()
    {
        if (keepAimingRoutine != null)
        {
            _keepAimingTimer = _keepAimingTime;
        }

        else
        {
            keepAimingRoutine = KeepAimingRoutine();

            StartCoroutine(keepAimingRoutine);
        }
    }

    protected IEnumerator keepAimingRoutine = null;

    protected IEnumerator KeepAimingRoutine()
    {
        _keepAimingTimer = _keepAimingTime;

        while (_keepAimingTimer > 0f)
        {
            yield return null;

            _keepAimingTimer -= Time.deltaTime;
        }

        _keepAimingTimer = 0f;

        _animator.SetBool("isAiming", false);

        keepAimingRoutine = null;
    }

    protected void StopKeepAming()
    {
        if (keepAimingRoutine != null)
        {
            StopCoroutine(keepAimingRoutine);

            keepAimingRoutine = null;
        }
    }
    protected void StopReload()
    {
        if (_reload != null)
        {
            StopCoroutine(_reload);

            _animator.SetBool("isReloading", false);

            _reload = null;
        }
    }
}