
using System.Collections.Generic;

public sealed class LevelInfo
{
    public PlayerInfo playerInfo { get; private set; }

    public Dictionary<SceneCode, StageInfo> stageInfos { get; private set; }

    public SceneCode stageSceneCode { get; set; }

    public float levelPlayTime { get; set; }

    public LevelInfo(LevelData levelData)
    {
        playerInfo = new PlayerInfo(levelData.playerData);

        stageInfos = new Dictionary<SceneCode, StageInfo>();

        foreach (KeyValuePair<SceneCode, StageData> stageDatas in levelData.stageDatas)
        {
            stageInfos.Add(stageDatas.Key, new StageInfo(stageDatas.Value));
        }

        /*Dictionary<SceneCode, StageInfo> stageInfos;

        List<RoundInfo> roundInfos;

        List<WaveInfo> waveInfos;

        Dictionary<int, CharacterCode> enemySpawnSpots;

        Dictionary<int, ItemCode> droppedItemSpawnSpots;

        stageInfos = new Dictionary<SceneCode, StageInfo>();

        foreach (KeyValuePair<SceneCode, StageData> stageDatas in levelData.stageDatas)
        {
            var roundDatas = stageDatas.Value.roundDatas;

            var count_0 = roundDatas.Count;

            roundInfos = new List<RoundInfo>();

            for (int index_0 = 0; index_0 < count_0; ++index_0)
            {
                var roundData = roundDatas[index_0];

                var waveDatas = roundData.waveDatas;

                var count_1 = roundData.waveDatas.Count;

                waveInfos = new List<WaveInfo>();

                for (int index_1 = 0; index_1 < count_1; ++index_1)
                {
                    var waveData = waveDatas[index_1];

                    enemySpawnSpots = null;

                    if (waveData.enemySpawnData != null)
                    {
                        enemySpawnSpots = new Dictionary<int, CharacterCode>();

                        var waveSpawnEnemiesData = waveData.enemySpawnData;

                        var spawnCount = waveSpawnEnemiesData.spawnCount;

                        if (waveSpawnEnemiesData.spawnEnemyDatas_Fixed != null)
                        {
                            var waveData_SpawnEnemyDatas_Fixed = waveSpawnEnemiesData.spawnEnemyDatas_Fixed;

                            var count_2 = waveData_SpawnEnemyDatas_Fixed.Count;

                            for (int index_2 = 0; index_2 < count_2; ++index_2)
                            {
                                var waveData_SpawnEnemyData_Fixed = waveData_SpawnEnemyDatas_Fixed[index_2];

                                var count_3 = waveData_SpawnEnemyData_Fixed.spawnCount;

                                for (int index_3 = 0; index_3 < count_3; ++index_3)
                                {
                                    while (true)
                                    {
                                        var spotNumber = Random.Range(0, 8);

                                        if (enemySpawnSpots.ContainsKey(spotNumber) == false)
                                        {
                                            enemySpawnSpots.Add(spotNumber, waveData_SpawnEnemyData_Fixed.characterCode);

                                            break;
                                        }
                                    }

                                    --spawnCount;
                                }
                            }
                        }

                        if (waveSpawnEnemiesData.spawnEnemyDatas_Random != null)
                        {
                            var waveData_SpawnEnemyDatas_Random = waveSpawnEnemiesData.spawnEnemyDatas_Random;

                            while (spawnCount > 0)
                            {
                                var spotNumber = Random.Range(0, 8);

                                if (enemySpawnSpots.ContainsKey(spotNumber) == false)
                                {
                                    var spawnChance_Max = 1f;

                                    var count_2 = waveData_SpawnEnemyDatas_Random.Count;

                                    for (int index_2 = 0; index_2 < count_2; ++index_2)
                                    {
                                        var waveData_SpawnEnemyData_Random = waveData_SpawnEnemyDatas_Random[index_2];

                                        if (Random.Range(0f, spawnChance_Max) <= waveData_SpawnEnemyData_Random.spawnChance)
                                        {
                                            enemySpawnSpots.Add(spotNumber, waveData_SpawnEnemyData_Random.characterCode);

                                            break;
                                        }

                                        spawnChance_Max -= waveData_SpawnEnemyData_Random.spawnChance;
                                    }

                                    --spawnCount;
                                }
                            }
                        }
                    }

                    droppedItemSpawnSpots = null;

                    if (waveData.droppedItemSpawnData != null)
                    {
                        droppedItemSpawnSpots = new Dictionary<int, ItemCode>();

                        var waveSpawnDroppedItemsData = waveData.droppedItemSpawnData;

                        var spawnCount = waveSpawnDroppedItemsData.spawnCount;

                        if (waveSpawnDroppedItemsData.spawnDroppedItemDatas_Fixed != null)
                        {
                            var waveData_SpawnDroppedItemDatas_Fixed = waveSpawnDroppedItemsData.spawnDroppedItemDatas_Fixed;

                            var count_2 = waveData_SpawnDroppedItemDatas_Fixed.Count;

                            for (int index_2 = 0; index_2 < count_2; ++index_2)
                            {
                                var waveData_SpawnDroppedItemData_Fixed = waveData_SpawnDroppedItemDatas_Fixed[index_2];

                                var count_3 = waveData_SpawnDroppedItemData_Fixed.spawnCount;

                                for (int index_3 = 0; index_3 < count_3; ++index_3)
                                {
                                    while (true)
                                    {
                                        var spotNumber = Random.Range(0, 8);

                                        if (droppedItemSpawnSpots.ContainsKey(spotNumber) == false)
                                        {
                                            droppedItemSpawnSpots.Add(spotNumber, waveData_SpawnDroppedItemData_Fixed.itemCode);

                                            break;
                                        }
                                    }

                                    --spawnCount;
                                }
                            }
                        }

                        if (waveSpawnDroppedItemsData.spawnDroppedItemDatas_Random != null)
                        {
                            var waveData_SpawnDroppedItemDatas_Random = waveSpawnDroppedItemsData.spawnDroppedItemDatas_Random;

                            while (spawnCount > 0)
                            {
                                var spotNumber = Random.Range(0, 8);

                                if (droppedItemSpawnSpots.ContainsKey(spotNumber) == false)
                                {
                                    var spawnChance_Max = 1f;

                                    var count_2 = waveData_SpawnDroppedItemDatas_Random.Count;

                                    for (int index_2 = 0; index_2 < count_2; ++index_2)
                                    {
                                        var waveData_SpawnDroppedItemData_Random = waveData_SpawnDroppedItemDatas_Random[index_2];

                                        if (Random.Range(0f, spawnChance_Max) <= waveData_SpawnDroppedItemData_Random.spawnChance)
                                        {
                                            droppedItemSpawnSpots.Add(spotNumber, waveData_SpawnDroppedItemData_Random.itemCode);

                                            break;
                                        }

                                        spawnChance_Max -= waveData_SpawnDroppedItemData_Random.spawnChance;
                                    }

                                    --spawnCount;
                                }
                            }
                        }
                    }

                    waveInfos.Add(new WaveInfo(waveData.waveTime, enemySpawnSpots, droppedItemSpawnSpots));
                }

                roundInfos.Add(new RoundInfo(roundData.roundTime, waveInfos));
            }

            stageInfos.Add(stageDatas.Key, new StageInfo(roundInfos));
        }

        this.stageInfos = new Dictionary<SceneCode, StageInfo>(stageInfos);*/

        stageSceneCode = SceneCode.city;

        levelPlayTime = 0f;
    }

    public LevelInfo(LevelInfo levelInfo)
    {
        playerInfo = new PlayerInfo(levelInfo.playerInfo);

        stageInfos = new Dictionary<SceneCode, StageInfo>(levelInfo.stageInfos);

        stageSceneCode = levelInfo.stageSceneCode;

        levelPlayTime = levelInfo.levelPlayTime;
    }
}