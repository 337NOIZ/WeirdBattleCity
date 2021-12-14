
using System.Collections;

using UnityEngine;

public sealed class Grenade : Consumable
{
    [SerializeField] private Muzzle _muzzle = null;

    public override ItemCode itemCode { get => ItemCode.Grenade; }

    public override void Awaken(Character character)
    {
        base.Awaken(character);

        _muzzle.Awaken(character.aim);
    }

    protected override IEnumerator Skill_(int skillNumber)
    {
        if (skillNumber > -1 && skillNumber < _skillInfos.Count)
        {
            var skillInfo = _skillInfos[skillNumber];

            if (skillInfo.cooldownTimer == 0f)
            {
                switch (skillNumber)
                {
                    case 0:

                        if (_itemInfo.stackCount > 0)
                        {
                            --_itemInfo.stackCount;

                            var skillInfo_RangedInfo = skillInfo.rangedInfo;

                            var explosionInfo = skillInfo_RangedInfo.projectileInfo.explosionInfo;

                            _muzzle.LaunchProjectile
                            (
                                _character,

                                (hitBox) =>
                                {
                                    hitBox.character.TakeAttack(_character, explosionInfo.damage, null);
                                },

                                skillInfo_RangedInfo
                            );

                            skillInfo.SetCoolTimer();

                            _skillWizard.StartSkillCooldown(skillInfo);
                        }

                        break;

                    default:

                        break;
                }
            }
        }

        _skill = null;

        yield break;
    }
}