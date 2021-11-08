
using Newtonsoft.Json;

using System.Collections;

using System.Collections.Generic;

using System.IO;

using UnityEngine;

public sealed class GameMaster : MonoBehaviour
{
    public static GameMaster instance { get; private set; } = null;

    public Dictionary<CharacterCode, Character> characterPrefabs { get; private set; } = new Dictionary<CharacterCode, Character>();

    public Dictionary<ItemCode, DroppedItem> droppedItemPrefabs { get; private set; } = new Dictionary<ItemCode, DroppedItem>();

    public Dictionary<string, Sprite> sprites { get; private set; } = new Dictionary<string, Sprite>();

    private string gameInfoPath;

    public GameData gameData { get; private set; } = new GameData();

    public GameInfo gameInfo { get; private set; }

    public bool isGamePaused { get; set; } = false;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            DontDestroyOnLoad(gameObject);

            instance = this;

            LoadResources();

            CheckGameInfo();
        }
    }

    private void LoadResources()
    {
        var characterPrefabs = Resources.LoadAll<Character>("Prefab/Character");

        int length = characterPrefabs.Length;

        for (int index = 0; index < length; ++index)
        {
            this.characterPrefabs.Add(characterPrefabs[index].characterCode, characterPrefabs[index]);
        }

        var droppedItemPrefabs = Resources.LoadAll<DroppedItem>("Prefab/Item/DroppedItem");

        length = droppedItemPrefabs.Length;

        for (int index = 0; index < length; ++index)
        {
            this.droppedItemPrefabs.Add(droppedItemPrefabs[index].itemCode, droppedItemPrefabs[index]);
        }

        var sprites = Resources.LoadAll<Sprite>("Sprite");

        length = sprites.Length;

        for (int index = 0; index < length; ++index)
        {
            this.sprites.Add(sprites[index].name, sprites[index]);
        }
    }

    public void CheckGameInfo()
    {
        gameInfoPath = Application.dataPath + "/GameInfo.cfg";

        if (new FileInfo(gameInfoPath).Exists == true)
        {
            LoadGameInfo();
        }

        else
        {
            NewGameInfo();

            //SaveUserInfo();
        }
    }

    public void LoadGameInfo()
    {
        gameInfo = JsonConvert.DeserializeObject<GameInfo>(System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(File.ReadAllText(gameInfoPath))));
    }

    public void NewGameInfo()
    {
        gameInfo = new GameInfo();
    }

    public void SaveGameInfo()
    {
        File.WriteAllText(gameInfoPath, System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(gameInfo))));
    }

    public void NewLevelInfo()
    {
        Dictionary<SceneCode, StageInfo> stageInfos;

        List<RoundInfo> roundInfos;

        List<WaveInfo> waveInfos;

        Dictionary<int, CharacterCode> enemySpawnSpots;

        Dictionary<int, ItemCode> droppedItemSpawnSpots;

        stageInfos = new Dictionary<SceneCode, StageInfo>();

        foreach (KeyValuePair<SceneCode, StageData> stageDatas in gameData.levelData.stageDatas)
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

                        if (waveSpawnEnemiesData.waveData_SpawnEnemyDatas_Fixed != null)
                        {
                            var waveData_SpawnEnemyDatas_Fixed = waveSpawnEnemiesData.waveData_SpawnEnemyDatas_Fixed;

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

                        if (waveSpawnEnemiesData.waveData_SpawnEnemyDatas_Random != null)
                        {
                            var waveData_SpawnEnemyDatas_Random = waveSpawnEnemiesData.waveData_SpawnEnemyDatas_Random;

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

                        if (waveSpawnDroppedItemsData.waveData_SpawnDroppedItemDatas_Fixed != null)
                        {
                            var waveData_SpawnDroppedItemDatas_Fixed = waveSpawnDroppedItemsData.waveData_SpawnDroppedItemDatas_Fixed;

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

                        if (waveSpawnDroppedItemsData.waveData_SpawnDroppedItemDatas_Random != null)
                        {
                            var waveData_SpawnDroppedItemDatas_Random = waveSpawnDroppedItemsData.waveData_SpawnDroppedItemDatas_Random;

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

        gameInfo.NewLevelInfo(stageInfos);
    }

    public void StartRecordPlayTime()
    {
        if (_recordPlayTime == null)
        {
            _recordPlayTime = RecordPlayTime();

            StartCoroutine(_recordPlayTime);
        }
    }

    public void StopRecordPlayTime()
    {
        if (_recordPlayTime != null)
        {
            StopCoroutine(_recordPlayTime);

            _recordPlayTime = null;
        }
    }

    private IEnumerator _recordPlayTime = null;

    private IEnumerator RecordPlayTime()
    {
        while (true)
        {
            if (isGamePaused == false)
            {
                gameInfo.gamePlayTime += Time.deltaTime;

                gameInfo.levelInfo.levelPlayTime += Time.deltaTime;
            }

            yield return null;
        }
    }

    public void Quit()
    {
        #if UNITY_EDITOR == true

            UnityEditor.EditorApplication.isPlaying = false;

        #else

            Application.Quit();

        #endif
    }
}