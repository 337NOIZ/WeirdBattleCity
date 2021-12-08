
using UnityEngine;

public sealed class CrazySpider : Enemy
{
    [SerializeField] private Muzzle _muzzle = null;

    public override CharacterCode characterCode { get => CharacterCode.spider; }

    public override void Initialize()
    {
        base.Initialize();
    }

    protected override bool IsInvincible() { return false; }

    protected override bool IsSkillValid()
    {
        return PhysicsWizard.Linecast(_muzzle.transform.position, _skillTarget_aimTarget.position, _skillInfo.range, attackable, _skillTarget.gameObject);
    }

    protected override void SkillEventAction()
    {
        _muzzle.LaunchProjectile(this, _skillInfo.rangedInfo);
    }
}