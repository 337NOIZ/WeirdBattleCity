
using System.Collections.Generic;

using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [Space]

    [SerializeField] private LayerMask layerMask = default;

    private new Rigidbody rigidbody;

    private TrailRenderer trailRenderer;

    Stack<Projectile> projectilePool;

    private Vector3 rigidbody_Position_Old;

    private RaycastHit raycastHit;

    private int damage;

    private void OnDisable()
    {
        transform.position = Vector3.zero;

        transform.rotation = Quaternion.identity;

        rigidbody.Sleep();

        trailRenderer.Clear();

        projectilePool.Push(this);
    }

    private void FixedUpdate()
    {
        if (Physics.Linecast(rigidbody.position, rigidbody_Position_Old, out raycastHit, layerMask) == true)
        {
            Character character = raycastHit.collider.GetComponent<Character>();

            if(character != null)
            {
                character.GainHealthPoint(-damage);
            }

            RecoveryToPool();
        }

        rigidbody_Position_Old = rigidbody.position;
    }

    public void Initialize(Stack<Projectile> projectilePool)
    {
        rigidbody = GetComponent<Rigidbody>();

        trailRenderer = GetComponent<TrailRenderer>();

        this.projectilePool = projectilePool;
    }

    public void Launch(int damage, float force, float lifeTime)
    {
        this.damage = damage;

        rigidbody_Position_Old = rigidbody.position;

        rigidbody.AddForce((transform.forward) * force, ForceMode.Impulse);

        Invoke("RecoveryToPool", lifeTime);
    }

    private void RecoveryToPool()
    {
        gameObject.SetActive(false);
    }
}