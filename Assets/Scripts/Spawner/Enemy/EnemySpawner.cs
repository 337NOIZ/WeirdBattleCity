
using System.Collections.Generic;

using UnityEngine;

public class EnemySpawner : Spawner
{
    public static EnemySpawner instance { get; private set; }

    [Space]

    [SerializeField] private GameObject _enemies = null;

    private Dictionary<EnemyCode, List<Enemy>> enemies = new Dictionary<EnemyCode, List<Enemy>>();

    public int enemyCount { get; set; } = 0;

    private void Awake()
    {
        instance = this;
    }

    public void Initialize()
    {
        _enemies.SetActive(true);

        var enemies = _enemies.GetComponentsInChildren<Enemy>();

        int length = enemies.Length;

        for(int index = 0; index < length; ++index)
        {
            EnemyCode enemyCode = enemies[index].enemyCode;

            if (this.enemies.ContainsKey(enemyCode) == false)
            {
                this.enemies.Add(enemyCode, new List<Enemy>());
            }

            this.enemies[enemyCode].Add(enemies[index]);

            enemies[index].gameObject.SetActive(false);
        }
    }

    public void Spawn(EnemyData enemyData)
    {
        EnemyCode enemyCode = enemyData.enemyCode;

        int count = enemies[enemyCode].Count;

        for (int index = 0; index < count; ++index)
        {
            if(enemies[enemyCode][index].gameObject.activeSelf == false)
            {
                ++enemyCount;

                enemies[enemyCode][index].Spawn(enemyData);
            }
        }
    }
}