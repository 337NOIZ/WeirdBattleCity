
using UnityEngine;

[System.Serializable]

public class EnemyData
{
    [Space]

    public EnemyCode enemyCode;

    [Space]

    public Vector3 transformPosition;

    [Space]

    public Vector3 animatorlocalEulerAngles;

    [Space]
    
    public DamageableData damageableData;

    [Space]

    public int physicalDamage;

    public EnemyData(EnemyCode enemyCode,  Vector3 rigidbodyPosition, Vector3 animatorlocalEulerAngles, DamageableData damageableData, int physicalDamage)
    {
        this.enemyCode = enemyCode;

        this.transformPosition = rigidbodyPosition;

        this.animatorlocalEulerAngles = animatorlocalEulerAngles;

        this.damageableData = new DamageableData(damageableData);

        this.physicalDamage = physicalDamage;
    }

    public EnemyData(EnemyData enemyData)
    {
        enemyCode = enemyData.enemyCode;

        transformPosition = enemyData.transformPosition;

        animatorlocalEulerAngles = enemyData.animatorlocalEulerAngles;

        damageableData = enemyData.damageableData;

        physicalDamage = enemyData.physicalDamage;
    }
}