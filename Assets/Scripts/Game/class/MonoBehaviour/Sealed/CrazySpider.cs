
using UnityEngine;

public sealed class CrazySpider : Enemy
{
    public override CharacterCode characterCode => CharacterCode.spider;

    [SerializeField] private Transform muzzle = null;

    protected override bool Invincible() { return false; }

    protected override void SkillEffect()
    {
        var rangedInfo = skillInfo.rangedInfo;

        var projectile = ObjectPool.instance.Pop(rangedInfo.projectileCode);

        projectile.transform.position = muzzle.position;

        projectile.transform.rotation = muzzle.rotation;

        projectile.Launch(this, rangedInfo.projectileInfo);
    }
}