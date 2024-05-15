using System.Collections.Generic;

public sealed class WaveData
{
    public sealed class EnemySpawnData
    {
        public int spawnCount_Max { get; private set; }

        public sealed class SpawnEnemyData
        {
            public CharacterCode characterCode { get; private set; }

            public int characterLevel { get; private set; }

            public int fixedSpawnCount { get; private set; }

            public float randomSpawnChance { get; private set; }

            public SpawnEnemyData(CharacterCode characterCode, int characterLevel, int fixedSpawnCount, float randomSpawnChance)
            {
                this.characterCode = characterCode;

                this.characterLevel = characterLevel;

                this.fixedSpawnCount = fixedSpawnCount;

                this.randomSpawnChance = randomSpawnChance;
            }
        }

        public List<SpawnEnemyData> spawnEnemyDatas { get; private set; } = null;

        public EnemySpawnData(int spawnCount_Max, List<SpawnEnemyData> spawnEnemyDatas)
        {
            this.spawnCount_Max = spawnCount_Max;
            
            this.spawnEnemyDatas = new List<SpawnEnemyData>(spawnEnemyDatas);
        }

        public EnemySpawnData(EnemySpawnData enemySpawnData)
        {
            spawnCount_Max = enemySpawnData.spawnCount_Max;
            
            spawnEnemyDatas = new List<SpawnEnemyData>(enemySpawnData.spawnEnemyDatas);
        }
    }

    public sealed class DroppedItemSpawnData
    {
        public int spawnCount_Max { get; private set; }

        public sealed class SpawnDroppedItemData
        {
            public ItemCode itemCode { get; private set; }

            public int itemLevel { get; private set; }

            public int fixedSpawnCount { get; private set; }

            public float randomSpawnChance { get; private set; }

            public SpawnDroppedItemData(ItemCode itemCode, int itemLevel, int fixedSpawnCount, float randomSpawnChance)
            {
                this.itemCode = itemCode;

                this.itemLevel = itemLevel;

                this.fixedSpawnCount = fixedSpawnCount;

                this.randomSpawnChance = randomSpawnChance;
            }
        }

        public List<SpawnDroppedItemData> spawnDroppedItemDatas { get; private set; } = null;

        public DroppedItemSpawnData(int spawnCount_Max, List<SpawnDroppedItemData> spawnDroppedItemDatas)
        {
            this.spawnCount_Max = spawnCount_Max;
            
            this.spawnDroppedItemDatas = new List<SpawnDroppedItemData>(spawnDroppedItemDatas);
        }

        public DroppedItemSpawnData(DroppedItemSpawnData droppedItemSpawnData)
        {
            spawnCount_Max = droppedItemSpawnData.spawnCount_Max;
            
            spawnDroppedItemDatas = new List<SpawnDroppedItemData>(droppedItemSpawnData.spawnDroppedItemDatas);
        }
    }

    public float waveTime { get; private set; }

    public EnemySpawnData enemySpawnData { get; private set; } = null;

    public DroppedItemSpawnData droppedItemSpawnData { get; private set; } = null;

    public WaveData(float waveTime, EnemySpawnData enemySpawnData, DroppedItemSpawnData droppedItemSpawnData)
    {
        this.waveTime = waveTime;

        if(enemySpawnData != null)
        {
            this.enemySpawnData = new EnemySpawnData(enemySpawnData);
        }
        
        if(droppedItemSpawnData != null)
        {
            this.droppedItemSpawnData = new DroppedItemSpawnData(droppedItemSpawnData);
        }
    }
}