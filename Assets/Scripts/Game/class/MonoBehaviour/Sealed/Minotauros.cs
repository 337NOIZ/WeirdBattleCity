
using System.Collections;

using UnityEngine;

public sealed class Minotauros : Enemy
{
    [SerializeField] private AttackBox _attackBox_0 = null;

    [SerializeField] private AttackBox _minotaurossAxe_Left_AttackBox = null;

    [SerializeField] private AttackBox _minotaurossAxe_Right_AttackBox = null;

    [SerializeField] private Muzzle _minotaurossAxe_Right_Muzzle = null;

    public override CharacterCode characterCode { get => CharacterCode.Minotauros; }

    public override void Awaken()
    {
        base.Awaken();

        _attackBox_0.Initialize(this);

        _minotaurossAxe_Left_AttackBox.Initialize(this);

        _minotaurossAxe_Right_Muzzle.Awaken(_aim);

        _minotaurossAxe_Right_AttackBox.Initialize(this);
    }

    protected override bool IsInvincible() { return false; }

    protected override bool IsSkillValid(int skillNumber)
    {
        var skillInfo = _skillInfos[skillNumber];

        if (skillInfo.cooldownTimer == 0f)
        {
            if (_skillTarget != null)
            {
                float range = skillInfo.range;

                if (Vector3.Distance(transform.position, _destination.position) <= range)
                {
                    switch(skillNumber)
                    {
                        case 2:

                            return PhysicsManager.LineCast(_head.position, _skillTarget_Head.position, range, attackableLayers, _skillTarget);

                        case 3:

                            return PhysicsManager.LineCast(_minotaurossAxe_Right_Muzzle.transform.position, _skillTarget_Head.position, range, attackableLayers, _skillTarget);

                        default:

                            return true;
                    }
                }
            }
        }

        return false;
    }

    protected override void SkillEventAction()
    {
        var skillInfo = _skillInfo;

        switch (_skillNumber)
        {
            case 0:

            case 1:

            case 2:

                var skillInfo_MeleeInfo = skillInfo.meleeInfo;

                switch (_skillNumber)
                {
                    case 0:

                        _minotaurossAxe_Left_AttackBox.StartTrailCasting
                        (
                            (hitBox) =>
                            {
                                hitBox.character.TakeAttack(this, skillInfo_MeleeInfo.damage, skillInfo_MeleeInfo.statusEffectInfos);
                            },

                            false
                        );

                        animatorManager.AddEventAction(_motionTriggerName, _minotaurossAxe_Left_AttackBox.StopTrailCasting);

                        break;

                    case 1:

                        _minotaurossAxe_Right_AttackBox.StartTrailCasting
                        (
                            (hitBox) =>
                            {
                                hitBox.character.TakeAttack(this, skillInfo_MeleeInfo.damage, skillInfo_MeleeInfo.statusEffectInfos);
                            },

                            false
                        );

                        animatorManager.AddEventAction(_motionTriggerName, _minotaurossAxe_Right_AttackBox.StopTrailCasting);

                        break;

                    case 2:
                        
                        TakeStatusEffect(skillInfo.statusEffectInfos);

                        isMoving = true;

                        var audioSourceMaster = AudioMaster.instance.Pop(AudioClipCode.Minotauros_Rush_0);

                        audioSourceMaster.transform.parent = transform;

                        audioSourceMaster.transform.position = Vector3.zero;

                        audioSourceMaster.gameObject.SetActive(true);

                        audioSourceMaster.Play();

                        var particleEffect = ObjectPool.instance.Pop(ParticleEffectCode.Minotauros_Dash);

                        particleEffect.transform.parent = transform;

                        particleEffect.transform.localPosition = Vector3.zero;

                        particleEffect.transform.localEulerAngles = Vector3.zero;

                        particleEffect.gameObject.SetActive(true);

                        particleEffect.Play();

                        _attackBox_0.StartTrailCasting
                        (
                            (hitBox) =>
                            {
                                hitBox.character.TakeAttack(this, skillInfo_MeleeInfo.damage, skillInfo_MeleeInfo.statusEffectInfos);
                            },

                            false
                        );

                        animatorManager.AddEventAction
                        (
                            _motionTriggerName,

                            () =>
                            {
                                _attackBox_0.StopTrailCasting();

                                particleEffect.Disable();

                                isMoving = false;

                                animatorManager.RemoveEventAction(_motionTriggerName);
                            }
                        );

                        break;
                }

                break;

            case 3:

                var skillInfo_RangedInfo = skillInfo.rangedInfo;

                var projectileInfo = skillInfo_RangedInfo.projectileInfo;

                _minotaurossAxe_Right_Muzzle.LaunchProjectile
                (
                    this,

                    (hitBox) =>
                    {
                        hitBox.character.TakeAttack(this, projectileInfo.damage, null);
                    },

                    skillInfo_RangedInfo
                );

                break;
        }
    }
}