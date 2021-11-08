
using System.Collections.Generic;

using UnityEngine;

public sealed class EnemySpawner : Spawner
{
    public static EnemySpawner instance { get; private set; }

    private Dictionary<CharacterCode, List<Character>> characterPool = new Dictionary<CharacterCode, List<Character>>();

    protected override void Awake()
    {
        instance = this;

        base.Awake();
    }

    public void Spawn(CharacterCode characterCode, int spotNumber, int characterLevel)
    {
        ++spawnCount;

        if (characterPool.ContainsKey(characterCode) == false)
        {
            characterPool.Add(characterCode, new List<Character>());
        }

        Character character = null;

        var count = characterPool[characterCode].Count;

        for (int index = 0; ; ++index)
        {
            if (index >= count)
            {
                character = Instantiate(GameMaster.instance.characterPrefabs[characterCode], spots[spotNumber]);

                character.Initialize();

                characterPool[characterCode].Add(character);

                break;
            }

            if (characterPool[characterCode][index].gameObject.activeSelf == false)
            {
                character = characterPool[characterCode][index];

                character.transform.parent = spots[spotNumber];

                character.transform.localPosition = Vector3.zero;

                character.transform.localEulerAngles = Vector3.zero;

                character.gameObject.SetActive(true);

                break;
            }
        }

        character.Initialize(characterLevel);
    }
}