
using UnityEngine;

[System.Serializable]

public class DamageableData
{
    [Space] public int healthPoint;
    
    public int healthPointMax;

    [Space] public float invincibleTime;

    public DamageableData(int healthPoint, int healthPointMax, float invincibleTime)
    {
        this.healthPoint = healthPoint;

        this.healthPointMax = healthPointMax;

        this.invincibleTime = invincibleTime;
    }

    public DamageableData(DamageableData damageableDate)
    {
        healthPoint = damageableDate.healthPoint;

        healthPointMax = damageableDate.healthPointMax;

        invincibleTime = damageableDate.invincibleTime;
    }
}