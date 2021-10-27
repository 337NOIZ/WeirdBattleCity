
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [Space]

    [SerializeField] private LayerMask layerMask = default;

    private new Rigidbody rigidbody;

    private Vector3 rigidbodyPosition_Old;

    private RaycastHit raycastHit;

    private int damage;

    private float lifeTime;

    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        rigidbodyPosition_Old = rigidbody.position;
    }

    private void FixedUpdate()
    {
        Debug.DrawLine(rigidbody.position, rigidbodyPosition_Old, Color.red, lifeTime);

        if (Physics.Linecast(rigidbody.position, rigidbodyPosition_Old, out raycastHit, layerMask) == true)
        {
            Character character = raycastHit.collider.GetComponent<Character>();

            if(character != null)
            {
                character.GetHealthPoint(-damage);
            }

            Destroy(gameObject);
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