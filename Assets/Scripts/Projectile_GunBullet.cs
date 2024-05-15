public sealed class Projectile_GunBullet : Projectile
{
    public override ProjectileCode projectileCode => ProjectileCode.GunBullet;

    protected override void ActionOnHit(HitBox hitBox)
    {
        PlayParticleEffect(_attackBox.raycastHit.point, ParticleEffectCode.GunBullet_Hit);

        base.ActionOnHit(hitBox);
    }
}