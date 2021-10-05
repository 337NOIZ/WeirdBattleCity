
using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    protected DamageableData damageableData;

    public void GetDamage(int damage)
    {
        damageableData.healthPoint -= damage;

        if(damageableData.healthPoint <= 0)
        {
            Dead();
        }
    }

    protected abstract void Dead();
}