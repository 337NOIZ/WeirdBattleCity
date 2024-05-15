using Newtonsoft.Json;

using System.Collections;

using System.Collections.Generic;

using System.IO;

using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; } = null;

    [SerializeField] private int _targetFrameRate = 60;

    private string _gameInfoPath;

    public Dictionary<string, Sprite> sprites { get; private set; } = new Dictionary<string, Sprite>();

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
        #if UNITY_EDITOR == true

            Application.runInBackground = true;

        #endif

        Application.targetFrameRate = _targetFrameRate;

        _gameInfoPath = Application.dataPath + "/GameInfo.cfg";
    }

    private void LoadResources()
    {
        var sprites = Resources.LoadAll<Sprite>("Sprites");
        
        var index_Max = sprites.Length;

        for (int index = 0; index < index_Max; ++index)
        {
            this.sprites.Add(sprites[index].name, sprites[index]);
        }
    }

    public void CheckGameInfo()
    {
        if (new FileInfo(_gameInfoPath).Exists == true)
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
        gameInfo = JsonConvert.DeserializeObject<GameInfo>(System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(File.ReadAllText(_gameInfoPath))));
    }

    public void NewGameInfo()
    {
        gameInfo = new GameInfo();
    }

    public void SaveGameInfo()
    {
        File.WriteAllText(_gameInfoPath, System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(gameInfo))));
    }

    public void NewLevelInfo()
    {
        gameInfo.NewLevelInfo(gameData);
    }

    public void StartRecordPlayTime()
    {
        if (_recordPlayTime == null)
        {
            _recordPlayTime = _RecordPlayTime();

            StartCoroutine(_recordPlayTime);
        }
    }

    private IEnumerator _recordPlayTime = null;

    private IEnumerator _RecordPlayTime()
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

    public void QuitGame()
    {
        #if UNITY_EDITOR == true

            UnityEditor.EditorApplication.isPlaying = false;

        #else

            Application.Quit();

        #endif
    }
}