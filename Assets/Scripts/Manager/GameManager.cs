
using System.Collections;

using System.Collections.Generic;

using System.IO;

using UnityEngine;

using UnityEngine.SceneManagement;

using Newtonsoft.Json;

using FadeScreen;

public enum EnemyCode
{
    crazyBird, crazySpider, crazyCow, dummy
}

public enum ItemType
{
    ammo, consumable, weapon,
}

public enum ItemCode
{
    arrow, bow, crossbow, crossbowBolt, grenade, medikit, pistol, pistolAmmo, shotgun, shotgunAmmo, submachineGun, submachineGunAmmo,
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public Dictionary<ItemCode, ItemData> itemDatas = new Dictionary<ItemCode, ItemData>();

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
        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        instance = this;

        Initialize();
    }

    private void Initialize()
    {
        itemDatas.Add(ItemCode.pistolAmmo, new ItemData(150));

        itemDatas.Add(ItemCode.shotgunAmmo, new ItemData(30));

        itemDatas.Add(ItemCode.submachineGunAmmo, new ItemData(300));

        itemDatas.Add(ItemCode.medikit, new ItemData(3, 10f, 0f, 0f));

        itemDatas.Add(ItemCode.pistol, new ItemData(1, 0.3f, 0.5f, false, 0, 0f, 1f, 1f, 5, 0, 100f, 1f, 15, 2f));

        itemDatas.Add(ItemCode.shotgun, new ItemData(1, 1f, 0.5f, false, 0, 0f, 10f, 5f, 5, 0, 100f, 1f, 3, 2.5f));

        itemDatas.Add(ItemCode.submachineGun, new ItemData(1, 0.1f, 0.5f, true, 0, 0f, 1f, 1f, 5, 0, 100f, 1f, 30, 2f));

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

    public void LoadScene(string sceneName, float routineTime)
    {
        StartCoroutine(_LoadScene(sceneName, routineTime));
    }

    private IEnumerator _LoadScene(string sceneName, float routineTime)
    {
        AudioManager.instance.FadeAudioListenerVolume(0f, routineTime);

        yield return PrimaryFadeScreen.instance.fadeScreen.Fade(null, 2, 0, routineTime);

        AudioManager.instance.backgroundMusicAudioSource.Stop();

        AudioManager.instance.StopSoundEffectAll();

        AudioManager.instance.StopFadeAudioListenerVolume();

        AudioListener.volume = 1f;

        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame(float routineTime)
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;

#else

Application.Quit();

#endif
    }
}