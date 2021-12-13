
using UnityEngine;

public sealed class EnemySpawner : Spawner
{
    public static EnemySpawner instance { get; private set; }

    protected override void Awake()
    {
        instance = this;

        base.Awake();
    }

    public void Spawn(CharacterCode characterCode, int characterLevel, int spotNumber)
    {
        var character = ObjectPool.instance.Pop(characterCode);

        character.transform.parent = _spots[spotNumber];

        character.transform.localPosition = Vector3.zero;

        character.transform.localEulerAngles = Vector3.zero;

        character.gameObject.SetActive(true);

        character.Initialize(characterLevel);

        character.Launch();

        ++spawnCount;
    }
}