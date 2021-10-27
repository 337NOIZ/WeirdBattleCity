
using System.Collections.Generic;

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance { get; private set; }

    private Dictionary<CharacterCode, List<Character>> enemies = new Dictionary<CharacterCode, List<Character>>();

    public int enemyCount { get; set; } = 0;

    private void Awake()
    {
        instance = this;
    }
    public void Spawn(CharacterInfo characterInfo)
    {
        CharacterCode damageableCode = characterInfo.damageableInfo.damageableCode;

        int index = 0;

        if (enemies.ContainsKey(damageableCode) == false)
        {
            enemies.Add(damageableCode, new List<Character>());
        }
        int count = enemies[damageableCode].Count;

        for (; ; ++index)
        {
            if (index >= count)
            {
                enemies[damageableCode].Add(Instantiate(GameManager.instance.characterPrefabs[damageableCode], transform));

                break;
            }
            if (enemies[damageableCode][index].gameObject.activeSelf == false)
            {
                break;
            }
        }
        enemies[damageableCode][index].Initialize(characterInfo);

        ++enemyCount;
    }
}