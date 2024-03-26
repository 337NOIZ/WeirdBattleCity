
using System.Collections;

using UnityEngine;

public abstract class Weapon : InventoryItem
{
    [SerializeField] protected Muzzle _muzzle = null;

    public override ItemType itemType => ItemType.Weapon;

    protected virtual ItemCode _ammo_itemCode { get; }

    protected ItemInfo _ammo;

    protected float _keepAimingTime = 1.5f;

    protected float _keepAimingTimer = 0f;

    public override void Awaken(Character character)
    {
        base.Awaken(character);

        _ammo = character.FindItem(ItemType.Ammo, _ammo_itemCode);
    }

    public override IEnumerator Draw()
    {
        _model.SetActive(true);

        _animator.SetFloat("drawingMotionSpeed", _itemInfo.drawingMotionSpeed);

        _animator.SetBool("isDrawing", true);

        _animator.SetTrigger(_motionTriggerName);

        while (_animator.GetBool("isDrawing") == true) yield return null;
    }

    public override IEnumerator Store()
    {
        yield return _StopSkill(false);

        StopReload();

        _model.SetActive(false);
    }

    public override void StartSkill(int skillNumber)
    {
        if (_keepAiming != null)
        {
            StopCoroutine(_keepAiming);

            _keepAimingTimer = 0f;

            _keepAiming = null;
        }

        StopReload();

        base.StartSkill(skillNumber);
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

                    break;

                default:

                    break;
            }
        }

        _skill = null;
    }

    protected override IEnumerator _StopSkill(bool keepAiming)
    {
        _skillWizard.StopSkill();

        while (_skill != null) yield return null;

        if (keepAiming == true)
        {
            StartKeepAiming();
        }

        else
        {
            _keepAimingTimer = 0f;
        }

        _stopSkill = null;
    }

    public override void StartReload()
    {
        if (_reload == null)
        {
            _reload = _Reload();

            StartCoroutine(_reload);
        }
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
                    _animator.SetFloat("reloadingMotionSpeed", _itemInfo.reloadingMotionSpeed);

                    _animator.SetBool("isReloading", true);

                    _animator.SetTrigger(_motionTriggerName);

                    while (_animator.GetBool("isReloading") == true) yield return null;

                    int ammoCount = _itemInfo.ammoCount_Max - _itemInfo.ammoCount;

                    _ammo.stackCount -= ammoCount;

                    if (_ammo.stackCount < 0)
                    {
                        ammoCount += _ammo.stackCount;

                        _ammo.stackCount = 0;
                    }

                    _itemInfo.ammoCount += ammoCount;
                }
            }
        }

        _reload = null;
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

    protected void StartKeepAiming()
    {
        if (_keepAiming != null)
        {
            _keepAimingTimer = _keepAimingTime;
        }

        else
        {
            _keepAiming = _KeepAiming();

            StartCoroutine(_keepAiming);
        }
    }

    protected IEnumerator _keepAiming = null;

    protected IEnumerator _KeepAiming()
    {
        _keepAimingTimer = _keepAimingTime;

        while (_keepAimingTimer > 0f)
        {
            yield return null;

            _keepAimingTimer -= Time.deltaTime;
        }

        _keepAimingTimer = 0f;

        _animator.SetBool("isAiming", false);

        _keepAiming = null;
    }
}