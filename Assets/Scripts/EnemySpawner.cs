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

        character.transform.position = _spots[spotNumber].position;

        character.gameObject.SetActive(true);

        character.Initialize(characterLevel);

        character.Launch();

        ++spawnCount;
    }
}