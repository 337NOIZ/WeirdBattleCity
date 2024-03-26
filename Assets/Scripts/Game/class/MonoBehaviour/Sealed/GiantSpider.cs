
using System.Collections;

using UnityEngine;

public sealed class GiantSpider : Enemy
{
    [SerializeField] private Muzzle _muzzle_0 = null;

    public override CharacterCode characterCode { get => CharacterCode.GiantSpider; }

    public override void Awaken()
    {
        base.Awaken();

        _muzzle_0.Awaken(_aim);
    }

    protected override bool IsInvincible() { return false; }

    protected override IEnumerator _Dead()
    {
        var audioSourceMaster = AudioMaster.instance.Pop(AudioClipCode.Spider_Chirping_1);

        audioSourceMaster.transform.position = transform.position;

        audioSourceMaster.gameObject.SetActive(true);

        audioSourceMaster.Play();

        yield return base._Dead();
    }

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
                    return PhysicsWizard.LineCast(_muzzle_0.transform.position, _skillTarget_Head.position, range, attackableLayers, _skillTarget);
                }
            }
        }

        return false;
    }

    protected override void SkillEventAction()
    {
        var skillInfo_RangedInfo = _skillInfo.rangedInfo;

        var projectileInfo = skillInfo_RangedInfo.projectileInfo;

        _muzzle_0.LaunchProjectile
        (
            this,
            
            (hitBox) =>
            {
                hitBox.character.TakeAttack(this, projectileInfo.damage, projectileInfo.statusEffectInfos);
            },

            skillInfo_RangedInfo
        );
    }
}