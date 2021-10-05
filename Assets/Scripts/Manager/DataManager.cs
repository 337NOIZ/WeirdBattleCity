
using System.Collections.Generic;

using System.IO;

using UnityEngine;

using Newtonsoft.Json;

public enum EnemyCode
{
    CRAZY_BIRD, CRAZY_SPIDER, CRAZY_COW, DUMMY
}

public enum ItemType
{
    AMMO, CONSUMABLE, WEAPON,
}

public enum ItemCode
{
    ARROW, BARE_FIST, BOW, CROSSBOW, CROSSBOW_BOLT, GRENADE, MEDIKIT, PISTOL, PISTOL_AMMO, SHOTGUN_AMMO, SMG, SMG_AMMO,
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance { get; private set; }

    public Dictionary<ItemCode, ItemData> itemDatas { get; private set; }

    private string gameDataPath;

    public class GameData
    {
        public PlayerData playerData;

        public GameData()
        {
            playerData = new PlayerData();
        }
    }

    public GameData gameData { get; private set; }

    private void Awake()
    {
        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        instance = this;

        InitializeItemDatas();

        InitializeGameData();
    }
    
    private void InitializeItemDatas()
    {
        itemDatas = new Dictionary<ItemCode, ItemData>();

        itemDatas.Add(ItemCode.BARE_FIST, new ItemData(ItemType.WEAPON, ItemCode.BARE_FIST, true, 0.5f, true, true, 1, 1f));

        itemDatas.Add(ItemCode.MEDIKIT, new ItemData(ItemType.CONSUMABLE, ItemCode.MEDIKIT, true, 1, 999, 10f, 0f));

        itemDatas.Add(ItemCode.PISTOL, new ItemData(ItemType.WEAPON, ItemCode.PISTOL, false, 0.3f, false, false, 1, 1f, 5, 100f, 1f, 15, 15, 3f));
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

            //SaveGameData();
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
        File.WriteAllText(gameDataPath, System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(gameData))));
    }
}