
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [Space]
    
    [SerializeField] private string layerName = null;

    private new Rigidbody rigidbody;

    private Vector3 rigidbodyPosition_Old;

    private RaycastHit raycastHit;

    private int layerMask;

    private int damage;

    private float lifeTime;

    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        rigidbodyPosition_Old = rigidbody.position;

        layerMask = LayerMask.GetMask(layerName);
    }

    private void FixedUpdate()
    {
        Debug.DrawLine(rigidbody.position, rigidbodyPosition_Old, Color.red, lifeTime);

        if (Physics.Linecast(rigidbody.position, rigidbodyPosition_Old, out raycastHit, layerMask) == true)
        {
            Damageable damageable = raycastHit.collider.GetComponent<Damageable>();

            if(damageable != null)
            {
                damageable.TakeDamage(damage);

                Destroy(gameObject);
            }
        }

        rigidbodyPosition_Old = rigidbody.position;
    }

    public void Launch(int damage, float force, float lifeTime)
    {
        this.damage = damage;

        rigidbody.AddForce((transform.forward) * force, ForceMode.Impulse);

        this.lifeTime = lifeTime;

        Invoke("Destroy", lifeTime);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}