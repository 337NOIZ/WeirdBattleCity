
using UnityEngine;

public abstract class Enemy : Damageable
{
    [Space]

    [SerializeField] private Animator animator = null;

    [Space]

    [SerializeField] private EnemyData enemyData = null;

    private new Rigidbody rigidbody;

    public EnemyCode enemyCode { get; protected set; }

    public virtual void Initialize()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Spawn(EnemyData enemyData)
    {
        this.enemyData = new EnemyData(enemyData);

        damageableData = enemyData.damageableData;

        transform.position = enemyData.transformPosition;

        gameObject.SetActive(true);
    }

    protected override void Dead()
    {
        --EnemySpawner.instance.enemyCount;

        gameObject.SetActive(false);
    }
}