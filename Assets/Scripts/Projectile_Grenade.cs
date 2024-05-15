using System.Collections;

public sealed class Projectile_Grenade : Projectile
{
    public override ProjectileCode projectileCode => ProjectileCode.Grenade;

    protected override IEnumerator _Launch(ProjectileInfo projectileInfo)
    {
        _rigidbody.velocity = transform.forward * projectileInfo.force;

        yield return CoroutineManager.WaitForSeconds(projectileInfo.lifeTime);

        _attackBox.Check((hitBox) => { if (hitBox != null) _actionOnHit.Invoke(hitBox); });

        var audioSourceMaster = AudioManager.instance.Pop(AudioClipCode.Explosion_0);

        audioSourceMaster.transform.position = transform.position;

        audioSourceMaster.gameObject.SetActive(true);

        audioSourceMaster.Play();

        PlayParticleEffect(transform.position, ParticleEffectCode.Explosion);

        Disable();
    }
}