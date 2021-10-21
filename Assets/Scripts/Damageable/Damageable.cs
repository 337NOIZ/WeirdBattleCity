
using System.Collections;

using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    protected DamageableData damageableData;

    protected new Rigidbody rigidbody;

    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void TakeDamage(int damage)
    {
        if (invincible == null)
        {
            if(damageableData.healthPoint > 0)
            {
                damageableData.healthPoint -= damage;

                if (damageableData.healthPoint <= 0)
                {
                    Dead();
                }

                else
                {
                    invincible = Invincible();

                    StartCoroutine(invincible);
                }
            }
        }
    }

    private IEnumerator invincible = null;

    private IEnumerator Invincible()
    {
        damageableData.invincibleTime = damageableData.invincibleTime_Seconds;

        while (damageableData.invincibleTime > 0f)
        {
            yield return null;

            damageableData.invincibleTime -= Time.deltaTime;
        }

        damageableData.invincibleTime = 0f;

        invincible = null;
    }

    protected abstract void Dead();
}