
using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    [Space, SerializeField] protected float healthPoint = 1f;

    public void Damaged(float damage)
    {
        healthPoint -= damage;

        if(healthPoint <= 0)
        {
            Dead();
        }
    }

    protected abstract void Dead();
}