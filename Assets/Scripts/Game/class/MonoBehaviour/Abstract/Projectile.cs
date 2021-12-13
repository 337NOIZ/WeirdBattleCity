
using System.Collections;

using UnityEngine;

using UnityEngine.Events;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected AttackBox _attackBox = null;

    public abstract ProjectileCode projectileCode { get; }

    protected Rigidbody _rigidbody;

    protected TrailRenderer _trailRenderer;

    protected UnityAction<HitBox> _actionOnHit;

    protected Vector3 _previousPosition;

    protected RaycastHit _raycastHit;

    public void Awaken()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _trailRenderer = GetComponent<TrailRenderer>();
    }

    public void Launch(Character attacker, UnityAction<HitBox> actionOnHit, ProjectileInfo projectileInfo)
    {
        _attackBox.Initialize(attacker);

        _actionOnHit = actionOnHit;

        _launch = Launch_(projectileInfo);

        StartCoroutine(_launch);
    }

    protected IEnumerator _launch = null;

    protected virtual IEnumerator Launch_(ProjectileInfo projectileInfo)
    {
        _rigidbody.velocity = transform.forward * projectileInfo.force;

        _attackBox.StartTrailCasting(ActionOnHit, true);

        yield return CoroutineWizard.WaitForSeconds(projectileInfo.lifeTime);

        Disable();
    }

    protected virtual void ActionOnHit(HitBox hitBox)
    {
        if(hitBox != null)
        {
            _actionOnHit.Invoke(hitBox);
        }

        Disable();
    }

    protected void PlayParticleEffect(Vector3 position, ParticleEffectCode particleEffectCode)
    {
        var particleEffect = ObjectPool.instance.Pop(particleEffectCode);

        particleEffect.transform.position = position;

        particleEffect.gameObject.SetActive(true);

        particleEffect.Play();
    }

    protected void Disable()
    {
        if (_launch != null)
        {
            StopCoroutine(_launch);

            _launch = null;
        }

        _attackBox.StopTrailCasting();

        transform.rotation = Quaternion.identity;

        _rigidbody.Sleep();

        if (_trailRenderer != null)
        {
            _trailRenderer.Clear();
        }

        gameObject.SetActive(false);

        ObjectPool.instance.Push(this);
    }
}