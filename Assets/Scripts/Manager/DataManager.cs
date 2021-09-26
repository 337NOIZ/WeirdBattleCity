
using System.IO;

using UnityEngine;

using Newtonsoft.Json;

public class DataManager : MonoBehaviour
{
    public static DataManager instance { get; private set; }

    private string playerDataPath = null;

    public class PlayerData
    {
        public PlayerData()
        {

        }
    }

    public PlayerData playerData { get; private set; } = null;

    private string gameDataPath = null;

    public class GameData
    {
        public GameData()
        {

        }
    }

    public GameData gameData { get; private set; } = null;

    private void Awake()
    {
        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(gameObject);
        }

        else
        {
            DontDestroyOnLoad(gameObject);

            instance = this;

            InitializePlayerData();

            InitializeGameData();
        }
    }

    private void InitializePlayerData()
    {
        playerDataPath = Application.dataPath + "/PlayerData.cfg";

        if (new FileInfo(playerDataPath).Exists == true)
        {
            LoadPlayerData();
        }

        else
        {
            ResetPlayerData();

            SavePlayerData();
        }
    }

    public void LoadPlayerData()
    {
        playerData = JsonConvert.DeserializeObject<PlayerData>(System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(File.ReadAllText(playerDataPath))));
    }

    public void ResetPlayerData()
    {
        playerData = new PlayerData();
    }

    public void SavePlayerData()
    {
        File.WriteAllText(playerDataPath, System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(playerData))));
    }

    private void InitializeGameData()
    {
        gameDataPath = Application.dataPath + "/GameData.cfg";

        if (new FileInfo(gameDataPath).Exists == true)
        {
            LoadGameData();
        }

        else
        {
            ResetGameData();

            SaveGameData();
        }
    }

    public void LoadGameData()
    {
        gameData = JsonConvert.DeserializeObject<GameData>(System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(File.ReadAllText(gameDataPath))));
    }

    public void ResetGameData()
    {
        gameData = new GameData();
    }

    public void SaveGameData()
    {
        File.WriteAllText(playerDataPath, System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(gameData))));
    }
}