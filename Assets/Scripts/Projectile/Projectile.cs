
using System.Reflection;

using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Space, SerializeField] string layerName = "Damageable";

    private new Rigidbody rigidbody;

    private Vector3 rigidbodyPosition_Old;

    private RaycastHit raycastHit;

    private int layerMask;

    private float damage;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        rigidbodyPosition_Old = rigidbody.position;

        layerMask = LayerMask.GetMask(layerName);
    }

    private void Update()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));

        var type = assembly.GetType("UnityEditor.LogEntries");

        var method = type.GetMethod("Clear");

        method.Invoke(new object(), null);

        if(Physics.Linecast(rigidbody.position, rigidbodyPosition_Old, out raycastHit, layerMask) == true)
        {
            Damageable damageable = raycastHit.collider.GetComponent<Damageable>();

            if(damageable != null)
            {
                damageable.Damaged(damage);

                Destroy(gameObject);
            }
        }

        Debug.DrawLine(rigidbodyPosition_Old, rigidbody.position, Color.red, 1f);

        rigidbodyPosition_Old = rigidbody.position;
    }

    public void Launch(float damage, float force, float lifeTime)
    {
        this.damage = damage;

        rigidbody.AddForce((transform.forward) * force, ForceMode.Impulse);

        Invoke("Destroy", lifeTime);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}