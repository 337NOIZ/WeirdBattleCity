
using System.Collections;

using UnityEngine;

public sealed class Medikit : Consumable
{
    public override ItemCode itemCode { get => ItemCode.Medikit; }

    public override void StartSkill(int skillNumber)
    {
        if (_skill == null)
        {
            _skill = _Skill(skillNumber);

            StartCoroutine(_skill);
        }
    }

    protected override IEnumerator _Skill(int skillNumber)
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
                            var damageableInfo = _character.damageableInfo;

                            if (damageableInfo.healthPoint < damageableInfo.healthPoint_Max)
                            {
                                --_itemInfo.stackCount;

                                var audioSourceMaster = AudioMaster.instance.Pop(AudioClipCode.Healing_0);

                                audioSourceMaster.transform.parent = _character.transform;

                                audioSourceMaster.transform.localPosition = Vector3.zero;

                                audioSourceMaster.gameObject.SetActive(true);

                                audioSourceMaster.Play();

                                _character.TakeStatusEffect(skillInfo.statusEffectInfos);

                                skillInfo.SetCoolTimer();

                                _skillManager.StartSkillCooldown(skillInfo);
                            }
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