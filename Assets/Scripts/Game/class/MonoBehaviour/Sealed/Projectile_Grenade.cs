
using System.Collections;

using UnityEngine;

public class Projectile_Grenade : Projectile
{
    public override ProjectileCode projectileCode => ProjectileCode.grenade;

    protected override IEnumerator LauchRoutine(Character attacker, ProjectileInfo projectileInfo)
    {
        var hostileLayer = attacker.attackable;

        yield return null;

        rigidbody.velocity = transform.forward * projectileInfo.force;

        var lifeTimer = projectileInfo.lifeTime;

        var damageableInfo = projectileInfo.damageableInfo;

        while (lifeTimer >= 0f && damageableInfo.healthPoint >= 0f)
        {
            yield return null;

            lifeTimer -= Time.deltaTime;
        }

        var explosionInfo = projectileInfo.explosionInfo;

        RaycastHit[] raycastHits = Physics.SphereCastAll(transform.position, explosionInfo.range, Vector3.up, 0f, hostileLayer);

        foreach(RaycastHit raycastHit in raycastHits)
        {
            Character victim = raycastHit.collider.GetComponentInParent<Character>();

            if(victim != null)
            {
                victim.TakeAttack(attacker, explosionInfo.damage, projectileInfo.statusEffectInfos);

                victim.TakeForce(transform.position, explosionInfo.force);
            }
        }

        var particleEffec = ObjectPool.instance.Pop(explosionInfo.particleEffectCode);

        particleEffec.transform.position = transform.position;

        particleEffec.Play();

        Disable();
    }
}