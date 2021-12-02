
using System.Collections.Generic;


using UnityEngine;

public sealed class WaveInfo
{
    public sealed class EnemySpawnInfo
    {
        public sealed class SpawnEnemyInfo
        {
            public CharacterCode characterCode { get; private set; }

            public int characterLevel { get; private set; }

            public List<int> spotNumbers { get; private set; }

            public SpawnEnemyInfo(WaveData.EnemySpawnData.SpawnEnemyData spawnEnemyData)
            {
                characterCode = spawnEnemyData.characterCode;

                characterLevel = spawnEnemyData.characterLevel;

                spotNumbers = new List<int>();

                int index_Max = spawnEnemyData.fixedSpawnCount;

                for (int index = 0; index < index_Max; ++index)
                {
                    spotNumbers.Add(Random.Range(0, 8));
                }
            }

            public SpawnEnemyInfo(SpawnEnemyInfo spawnEnemyInfo)
            {
                characterCode = spawnEnemyInfo.characterCode;

                characterLevel = spawnEnemyInfo.characterLevel;

                spotNumbers = new List<int>(spawnEnemyInfo.spotNumbers);
            }
        }

        public List<SpawnEnemyInfo> spawnEnemyInfos { get; private set; } = null;

        public EnemySpawnInfo(WaveData.EnemySpawnData enemySpawnData)
        {
            var spawnCount_Max = enemySpawnData.spawnCount_Max;

            var spawnEnemyDatas = enemySpawnData.spawnEnemyDatas;

            spawnEnemyInfos = new List<SpawnEnemyInfo>();

            int index_Max = spawnEnemyDatas.Count;

            for(int index = 0; index < index_Max; ++index)
            {
                spawnEnemyInfos.Add(new SpawnEnemyInfo(spawnEnemyDatas[index]));

                spawnCount_Max -= spawnEnemyDatas[index].fixedSpawnCount;
            }

            while(spawnCount_Max > 0)
            {
                var spawnChance_Max = 1f;

                for (int index = 0; index < index_Max; ++index)
                {
                    if (Random.Range(0f, spawnChance_Max) <= spawnEnemyDatas[index].randomSpawnChance)
                    {
                        spawnEnemyInfos[index].spotNumbers.Add(Random.Range(0, 8));

                        break;
                    }

                    spawnChance_Max -= spawnEnemyDatas[index].randomSpawnChance;
                }

                --spawnCount_Max;
            }
        }

        public EnemySpawnInfo(EnemySpawnInfo enemySpawnInfo)
        {
            spawnEnemyInfos = new List<SpawnEnemyInfo>(enemySpawnInfo.spawnEnemyInfos);
        }
    }

    public sealed class DroppedItemSpawnInfo
    {
        public sealed class SpawnDroppedItemInfo
        {
            public ItemCode itemCode { get; private set; }

            public int itemLevel { get; private set; }

            public List<int> spotNumbers { get; private set; }

            public SpawnDroppedItemInfo(WaveData.DroppedItemSpawnData.SpawnDroppedItemData spawnDroppedItemData)
            {
                itemCode = spawnDroppedItemData.itemCode;
                
                itemLevel = spawnDroppedItemData.itemLevel;

                spotNumbers = new List<int>();

                int index_Max = spawnDroppedItemData.fixedSpawnCount;

                for (int index = 0; index < index_Max; ++index)
                {
                    spotNumbers.Add(Random.Range(0, 8));
                }
            }

            public SpawnDroppedItemInfo(SpawnDroppedItemInfo spawnDroppedItemInfo)
            {
                itemCode = spawnDroppedItemInfo.itemCode;

                itemLevel = spawnDroppedItemInfo.itemLevel;

                spotNumbers = new List<int>(spawnDroppedItemInfo.spotNumbers);
            }
        }

        public List<SpawnDroppedItemInfo> spawnDroppedItemInfos { get; private set; } = null;

        public DroppedItemSpawnInfo(WaveData.DroppedItemSpawnData droppedItemSpawnData)
        {
            var spawnCount_Max = droppedItemSpawnData.spawnCount_Max;

            var spawnEnemyDatas = droppedItemSpawnData.spawnDroppedItemDatas;

            spawnDroppedItemInfos = new List<SpawnDroppedItemInfo>();

            int index_Max = spawnEnemyDatas.Count;

            for (int index = 0; index < index_Max; ++index)
            {
                spawnDroppedItemInfos.Add(new SpawnDroppedItemInfo(spawnEnemyDatas[index]));

                spawnCount_Max -= spawnEnemyDatas[index].fixedSpawnCount;
            }

            while (spawnCount_Max > 0)
            {
                var spawnChance_Max = 1f;

                for (int index = 0; index < index_Max; ++index)
                {
                    if (Random.Range(0f, spawnChance_Max) <= spawnEnemyDatas[index].randomSpawnChance)
                    {
                        spawnDroppedItemInfos[index].spotNumbers.Add(Random.Range(0, 8));

                        break;
                    }

                    spawnChance_Max -= spawnEnemyDatas[index].randomSpawnChance;
                }

                --spawnCount_Max;
            }
        }

        public DroppedItemSpawnInfo(DroppedItemSpawnInfo droppedItemSpawnInfo)
        {
            spawnDroppedItemInfos = new List<SpawnDroppedItemInfo>(droppedItemSpawnInfo.spawnDroppedItemInfos);
        }
    }

    public EnemySpawnInfo enemySpawnData { get; private set; } = null;

    public DroppedItemSpawnInfo droppedItemSpawnData { get; private set; } = null;

    public float waveTimer { get; set; }

    public WaveInfo(WaveData waveData)
    {
        var enemySpawnData = waveData.enemySpawnData;

        if (enemySpawnData != null)
        {
            this.enemySpawnData = new EnemySpawnInfo(enemySpawnData);
        }

        var droppedItemSpawnData = waveData.droppedItemSpawnData;

        if (droppedItemSpawnData != null)
        {
            this.droppedItemSpawnData = new DroppedItemSpawnInfo(droppedItemSpawnData);
        }

        waveTimer = waveData.waveTime;
    }

    public WaveInfo(WaveInfo waveInfo)
    {
        if(waveInfo.enemySpawnData != null)
        {
            enemySpawnData = new EnemySpawnInfo(waveInfo.enemySpawnData);
        }

        if (waveInfo.droppedItemSpawnData != null)
        {
            droppedItemSpawnData = new DroppedItemSpawnInfo(waveInfo.droppedItemSpawnData);
        }

        waveTimer = waveInfo.waveTimer;
    }
}