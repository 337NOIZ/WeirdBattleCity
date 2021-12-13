
using System.Collections;

public class Projectile_Grenade : Projectile
{
    public override ProjectileCode projectileCode => ProjectileCode.Grenade;

    protected override IEnumerator Launch_(ProjectileInfo projectileInfo)
    {
        _rigidbody.velocity = transform.forward * projectileInfo.force;

        yield return CoroutineWizard.WaitForSeconds(projectileInfo.lifeTime);

        _attackBox.Check((hitBox) => { if (hitBox != null) _actionOnHit.Invoke(hitBox); });

        PlayParticleEffect(transform.position, ParticleEffectCode.Explosion);

        Disable();
    }
}