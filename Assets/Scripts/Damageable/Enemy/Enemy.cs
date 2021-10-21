
using System.Collections;

using UnityEngine;

public abstract class Enemy : Damageable
{
    [Space]

    [SerializeField] private Animator animator = null;

    //

    public EnemyCode enemyCode { get; protected set; }

    [Space]

    [SerializeField] private EnemyData enemyData = null;

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            doDamage = DoDamage(player);

            StartCoroutine(doDamage);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            StopCoroutine(doDamage);

            doDamage = null;
        }
    }

    private IEnumerator doDamage = null;

    private IEnumerator DoDamage(Player player)
    {
        while(true)
        {
            player.TakeDamage(damageableData.contactDamage);

            yield return null;
        }
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