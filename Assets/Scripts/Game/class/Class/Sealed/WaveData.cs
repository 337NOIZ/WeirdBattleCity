
using System.Collections.Generic;

public abstract class WaveData_SpawnEnemyData
{
    public CharacterCode characterCode { get; protected set; }

    public int characterLevel { get; protected set; }
}

public sealed class WaveData_SpawnEnemyData_Fixed : WaveData_SpawnEnemyData
{
    public int spawnCount { get; private set; }

    public WaveData_SpawnEnemyData_Fixed(CharacterCode characterCode, int characterLevel, int spawnCount)
    {
        this.characterCode = characterCode;

        this.characterLevel = characterLevel;

        this.spawnCount = spawnCount;
    }
}

public sealed class WaveData_SpawnEnemyData_Random : WaveData_SpawnEnemyData
{
    public float spawnChance { get; private set; }

    public WaveData_SpawnEnemyData_Random(CharacterCode characterCode, int characterLevel, float spawnChance)
    {
        this.characterCode = characterCode;

        this.characterLevel = characterLevel;

        this.spawnChance = spawnChance;
    }
}

public sealed class WaveSpawnEnemiesData // WaveData_EnemySpawnData
{
    public int spawnCount { get; private set; }

    public List<WaveData_SpawnEnemyData_Fixed> waveData_SpawnEnemyDatas_Fixed { get; private set; } = null;

    public List<WaveData_SpawnEnemyData_Random> waveData_SpawnEnemyDatas_Random { get; private set; } = null;

    public WaveSpawnEnemiesData(int spawnCount, List<WaveData_SpawnEnemyData_Fixed> waveData_SpawnEnemyDatas_Fixed, List<WaveData_SpawnEnemyData_Random> waveData_SpawnEnemyDatas_Random)
    {
        this.spawnCount = spawnCount;

        if (waveData_SpawnEnemyDatas_Fixed != null)
        {
            this.waveData_SpawnEnemyDatas_Fixed = new List<WaveData_SpawnEnemyData_Fixed>(waveData_SpawnEnemyDatas_Fixed);
        }

        if (waveData_SpawnEnemyDatas_Random != null)
        {
            this.waveData_SpawnEnemyDatas_Random = new List<WaveData_SpawnEnemyData_Random>(waveData_SpawnEnemyDatas_Random);
        }
    }

    public WaveSpawnEnemiesData(WaveSpawnEnemiesData waveSpawnEnemiesData)
    {
        spawnCount = waveSpawnEnemiesData.spawnCount;

        if (waveSpawnEnemiesData.waveData_SpawnEnemyDatas_Fixed != null)
        {
            waveData_SpawnEnemyDatas_Fixed = new List<WaveData_SpawnEnemyData_Fixed>(waveSpawnEnemiesData.waveData_SpawnEnemyDatas_Fixed);
        }

        if (waveSpawnEnemiesData.waveData_SpawnEnemyDatas_Random != null)
        {
            waveData_SpawnEnemyDatas_Random = new List<WaveData_SpawnEnemyData_Random>(waveSpawnEnemiesData.waveData_SpawnEnemyDatas_Random);
        }
    }
}

public abstract class WaveData_SpawnDroppedItemData
{
    public ItemCode itemCode { get; protected set; }

    public int itemLevel { get; protected set; }
}

public sealed class WaveData_SpawnDroppedItemData_Fixed : WaveData_SpawnDroppedItemData
{
    public int spawnCount { get; private set; }

    public WaveData_SpawnDroppedItemData_Fixed(ItemCode itemCode, int itemLevel, int spawnCount)
    {
        this.itemCode = itemCode;

        this.itemLevel = itemLevel;

        this.spawnCount = spawnCount;
    }
}

public sealed class WaveData_SpawnDroppedItemData_Random : WaveData_SpawnDroppedItemData
{
    public float spawnChance { get; private set; }

    public WaveData_SpawnDroppedItemData_Random(ItemCode itemCode, int itemLevel, float spawnChance)
    {
        this.itemCode = itemCode;

        this.itemLevel = itemLevel;

        this.spawnChance = spawnChance;
    }
}

public sealed class WaveSpawnDroppedItemsData // WaveData_DroppedItemSpawnData
{
    public int spawnCount { get; private set; }

    public List<WaveData_SpawnDroppedItemData_Fixed> waveData_SpawnDroppedItemDatas_Fixed { get; private set; } = null;

    public List<WaveData_SpawnDroppedItemData_Random> waveData_SpawnDroppedItemDatas_Random { get; private set; } = null;

    public WaveSpawnDroppedItemsData(int spawnCount, List<WaveData_SpawnDroppedItemData_Fixed> waveData_SpawnDroppedItemDatas_Fixed, List<WaveData_SpawnDroppedItemData_Random> waveData_SpawnDroppedItemDatas_Random)
    {
        this.spawnCount = spawnCount;

        if (waveData_SpawnDroppedItemDatas_Fixed != null)
        {
            this.waveData_SpawnDroppedItemDatas_Fixed = new List<WaveData_SpawnDroppedItemData_Fixed>(waveData_SpawnDroppedItemDatas_Fixed);
        }

        if (waveData_SpawnDroppedItemDatas_Random != null)
        {
            this.waveData_SpawnDroppedItemDatas_Random = new List<WaveData_SpawnDroppedItemData_Random>(waveData_SpawnDroppedItemDatas_Random);
        }
    }

    public WaveSpawnDroppedItemsData(WaveSpawnDroppedItemsData waveSpawnDroppedItemsData)
    {
        spawnCount = spawnCount;

        if (waveSpawnDroppedItemsData.waveData_SpawnDroppedItemDatas_Fixed != null)
        {
            waveData_SpawnDroppedItemDatas_Fixed = new List<WaveData_SpawnDroppedItemData_Fixed>(waveSpawnDroppedItemsData.waveData_SpawnDroppedItemDatas_Fixed);
        }

        if (waveSpawnDroppedItemsData.waveData_SpawnDroppedItemDatas_Random != null)
        {
            waveData_SpawnDroppedItemDatas_Random = new List<WaveData_SpawnDroppedItemData_Random>(waveSpawnDroppedItemsData.waveData_SpawnDroppedItemDatas_Random);
        }
    }
}

public sealed class WaveData
{
    public float waveTime { get; private set; }

    public WaveSpawnEnemiesData enemySpawnData { get; private set; } = null;

    public WaveSpawnDroppedItemsData droppedItemSpawnData { get; private set; } = null;

    public WaveData(float waveTime, WaveSpawnEnemiesData enemySpawnData, WaveSpawnDroppedItemsData droppedItemSpawnData)
    {
        this.waveTime = waveTime;

        if(enemySpawnData != null)
        {
            this.enemySpawnData = new WaveSpawnEnemiesData(enemySpawnData);
        }
        
        if(droppedItemSpawnData != null)
        {
            this.droppedItemSpawnData = new WaveSpawnDroppedItemsData(droppedItemSpawnData);
        }
    }
}