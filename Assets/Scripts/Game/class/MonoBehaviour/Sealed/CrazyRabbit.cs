
using UnityEngine;

public sealed class CrazyRabbit : Enemy
{
    [SerializeField] private AttackBox _attackBox_0 = null;

    public override CharacterCode characterCode { get => CharacterCode.CrazyRabbit; }

    public override void Awaken()
    {
        base.Awaken();

        _attackBox_0.Initialize(this);
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

                if (Vector3.Distance(transform.position, _destination.position) <= range == true)
                {
                    return true;
                }
            }
        }

        return false;
    }

    protected override void SkillEventAction()
    {
        _attackBox_0.StartTrailCasting
        (
            (hitBox) =>
            {
                hitBox.character.TakeAttack(this, _skillInfo.meleeInfo.damage, null);
            },

            false
        );

        animatorWizard.AddEventAction(_motionTriggerName, _attackBox_0.StopTrailCasting);
    }
}