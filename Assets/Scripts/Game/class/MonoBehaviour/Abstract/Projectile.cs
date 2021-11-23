
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public virtual ProjectileCode projectileCode { get; }

    private new Rigidbody rigidbody;

    private TrailRenderer trailRenderer;

    private Vector3 rigidbody_Position_Previous;

    private RaycastHit raycastHit;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        trailRenderer = GetComponent<TrailRenderer>();
    }

    public void Launch(Character shooter, float force, float lifeTime, float damage, List<StatusEffectInfo> statusEffectInfos)
    {
        lauchRoutine = LauchRoutine(shooter, force, lifeTime, damage, statusEffectInfos);

        StartCoroutine(lauchRoutine);
    }

    private IEnumerator lauchRoutine = null;

    private IEnumerator LauchRoutine(Character attacker, float force, float lifeTimer, float damage, List<StatusEffectInfo> statusEffectInfos)
    {
        var hostileLayer = attacker.hostileLayer;

        yield return null;

        rigidbody.velocity = transform.forward * force;

        while (lifeTimer > 0f)
        {
            rigidbody_Position_Previous = rigidbody.position;

            yield return null;

            if (Physics.Linecast(rigidbody_Position_Previous, rigidbody.position, out raycastHit, hostileLayer) == true)
            {
                Character victim = raycastHit.collider.GetComponent<Character>();

                if (victim != null)
                {
                    victim.TakeAttack(attacker, damage, statusEffectInfos);
                }

                break;
            }

            lifeTimer -= Time.deltaTime;
        }

        Disable();
    }

    private void Disable()
    {
        if (lauchRoutine != null)
        {
            StopCoroutine(lauchRoutine);
        }

        transform.position = Vector3.zero;

        transform.rotation = Quaternion.identity;

        rigidbody.Sleep();

        trailRenderer.Clear();

        ObjectPool.instance.Push(this);
    }
}