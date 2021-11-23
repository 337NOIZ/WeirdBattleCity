
using Newtonsoft.Json;

using System.Collections;

using System.Collections.Generic;

using System.IO;

using UnityEngine;

public sealed class GameMaster : MonoBehaviour
{
    public static GameMaster instance { get; private set; } = null;

    [Space]

    [SerializeField] private int targetFrameRate = 60;

    private string gameInfoPath;

    public Dictionary<string, Sprite> sprites { get; private set; } = new Dictionary<string, Sprite>();

    public Dictionary<CharacterCode, Character> characterPrefabs { get; private set; } = new Dictionary<CharacterCode, Character>();

    public Dictionary<ItemCode, DroppedItem> droppedItemPrefabs { get; private set; } = new Dictionary<ItemCode, DroppedItem>();

    public Dictionary<ProjectileCode, Projectile> projectilePrefabs { get; private set; } = new Dictionary<ProjectileCode, Projectile>();

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
            instance = this;

            Initialize();

            LoadResources();

            CheckGameInfo();

            DontDestroyOnLoad(gameObject);
        }
    }

    public void Initialize()
    {
        Application.targetFrameRate = targetFrameRate;

        gameInfoPath = Application.dataPath + "/GameInfo.cfg";
    }

    private void LoadResources()
    {
        var sprites = Resources.LoadAll<Sprite>("Sprites");
        
        var length = sprites.Length;

        for (int index = 0; index < length; ++index)
        {
            this.sprites.Add(sprites[index].name, sprites[index]);
        }

        var characterPrefabs = Resources.LoadAll<Character>("Prefabs/Character");

        length = characterPrefabs.Length;

        for (int index = 0; index < length; ++index)
        {
            this.characterPrefabs.Add(characterPrefabs[index].characterCode, characterPrefabs[index]);
        }

        var droppedItemPrefabs = Resources.LoadAll<DroppedItem>("Prefabs/Item/DroppedItem");

        length = droppedItemPrefabs.Length;

        for (int index = 0; index < length; ++index)
        {
            this.droppedItemPrefabs.Add(droppedItemPrefabs[index].itemCode, droppedItemPrefabs[index]);
        }

        var projectilePrefabs = Resources.LoadAll<Projectile>("Prefabs/Projectile");

        length = projectilePrefabs.Length;

        for (int index = 0; index < length; ++index)
        {
            this.projectilePrefabs.Add(projectilePrefabs[index].projectileCode, projectilePrefabs[index]);
        }
    }

    public void CheckGameInfo()
    {
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
        gameInfo.NewLevelInfo(gameData);
    }

    public void StartRecordPlayTime()
    {
        if (_recordPlayTime == null)
        {
            _recordPlayTime = RecordPlayTime();

            StartCoroutine(_recordPlayTime);
        }
    }

    private IEnumerator _recordPlayTime = null;

    private IEnumerator RecordPlayTime()
    {
        while (true)
        {
            yield return null;

            if (isGamePaused == false)
            {
                gameInfo.gamePlayTime += Time.deltaTime;

                gameInfo.levelInfo.levelPlayTime += Time.deltaTime;
            }
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

    public void Quit()
    {
        #if UNITY_EDITOR == true

            UnityEditor.EditorApplication.isPlaying = false;

        #else

            Application.Quit();

        #endif
    }
}