
using System.Collections;

using UnityEngine;

public sealed class Grenade : Consumable
{
    [SerializeField] private Muzzle _muzzle = null;

    public override ItemCode itemCode { get => ItemCode.Crenade; }

    public override void Awaken(Character character)
    {
        base.Awaken(character);

        _muzzle.Awaken(character.aim);
    }

    protected override IEnumerator Skill_(int skillNumber)
    {
        var skillInfo = _itemInfo.skillInfos[skillNumber];

        if (skillInfo.cooldownTimer == 0f)
        {
            var skillInfo_RangedInfo = skillInfo.rangedInfo;

            var explosionInfo = skillInfo_RangedInfo.projectileInfo.explosionInfo;

            switch (skillNumber)
            {
                case 0:

                    if (_itemInfo.stackCount > 0)
                    {
                        --_itemInfo.stackCount;

                        _animator.SetBool("isAiming", true);

                        _muzzle.LaunchProjectile
                        (
                            _character,

                            (hitBox) =>
                            {
                                hitBox.character.TakeAttack(_character, explosionInfo.damage, null);
                            },

                            skillInfo_RangedInfo
                        );

                        _animator.SetBool("isAiming", false);
                    }

                    skillInfo.SetCoolTimer();

                    _skillWizard.StartSkillCooldown(skillInfo);

                    break;

                default:

                    break;
            }
        }

        _skill = null;

        yield return null;
    }
}