
using System.Collections;

using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public virtual ProjectileCode projectileCode { get; }

    protected new Rigidbody rigidbody;

    private TrailRenderer trailRenderer;

    private Vector3 rigidbody_Position_Previous;

    private RaycastHit raycastHit;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        trailRenderer = GetComponent<TrailRenderer>();
    }

    public void Launch(Character attacker, ProjectileInfo projectileInfo)
    {
        lauchRoutine = LauchRoutine(attacker, projectileInfo);

        StartCoroutine(lauchRoutine);
    }

    private IEnumerator lauchRoutine = null;

    protected virtual IEnumerator LauchRoutine(Character attacker, ProjectileInfo projectileInfo)
    {
        var hostileLayer = attacker.attackable;

        yield return null;

        rigidbody.velocity = transform.forward * projectileInfo.force;

        var lifeTimer = projectileInfo.lifeTime;

        while (lifeTimer > 0f)
        {
            rigidbody_Position_Previous = rigidbody.position;

            yield return null;

            if (Physics.Linecast(rigidbody_Position_Previous, rigidbody.position, out raycastHit, hostileLayer) == true)
            {
                Character victim = raycastHit.collider.GetComponentInParent<Character>();

                if (victim != null)
                {
                    victim.TakeAttack(attacker, projectileInfo.damage, projectileInfo.statusEffectInfos);
                }

                break;
            }

            lifeTimer -= Time.deltaTime;
        }

        Disable();
    }

    protected void Disable()
    {
        if (lauchRoutine != null)
        {
            StopCoroutine(lauchRoutine);
        }

        transform.rotation = Quaternion.identity;

        rigidbody.Sleep();

        trailRenderer.Clear();

        gameObject.SetActive(false);

        ObjectPool.instance.Push(this);
    }
}