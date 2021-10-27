
using System.Collections;

using System.Collections.Generic;

using System.IO;

using UnityEngine;

using UnityEngine.SceneManagement;

using Newtonsoft.Json;

using FadeScreen;

public enum CharacterType
{
    enemy, friendly, neutrality
}
public enum CharacterCode
{
    crazyBird, crazySpider, crazyCow, dummy, player,
}
public enum ItemType
{
    ammo, consumable, weapon,
}
public enum ItemCode
{
    arrow, bow, crossbow, crossbowBolt, grenade, medikit, pistol, pistolAmmo, shotgun, shotgunAmmo, submachineGun, submachineGunAmmo,
}
public enum ConditionEffect
{
    slow, stun
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public Dictionary<CharacterCode, Character> characterPrefabs { get; private set; } = new Dictionary<CharacterCode, Character>();

    public Dictionary<ItemCode, DroppedItem> droppedItemPrefabs { get; private set; } = new Dictionary<ItemCode, DroppedItem>();

    public Dictionary<CharacterCode, CharacterData> characterDatas { get; private set; } = new Dictionary<CharacterCode, CharacterData>();

    public PlayerData playerData { get; private set; }

    public Dictionary<ItemCode, ItemData> itemDatas { get; private set; } = new Dictionary<ItemCode, ItemData>();

    private string gameDataPath;

    public class GameData
    {
        public float survivedTime_Seconds = 0f;

        public PlayerInfo playerInfo { get; private set; }

        public GameData()
        {
            var characterInfo = new CharacterInfo(new TransformInfo(), new DamageableInfo(CharacterType.friendly, CharacterCode.player, 1f, 1f), new MovementInfo());

            var itemInfos = new Dictionary<ItemType, List<ItemInfo>>();

            itemInfos.Add(ItemType.ammo, new List<ItemInfo>());

            itemInfos[ItemType.ammo].Add(new ItemInfo(ItemType.ammo, ItemCode.pistolAmmo, 135));

            itemInfos[ItemType.ammo].Add(new ItemInfo(ItemType.ammo, ItemCode.shotgunAmmo, 27));

            itemInfos[ItemType.ammo].Add(new ItemInfo(ItemType.ammo, ItemCode.submachineGunAmmo, 270));

            itemInfos.Add(ItemType.consumable, new List<ItemInfo>());

            itemInfos[ItemType.consumable].Add(new ItemInfo(ItemType.consumable, ItemCode.medikit, 0));

            itemInfos.Add(ItemType.weapon, new List<ItemInfo>());

            itemInfos[ItemType.weapon].Add(new ItemInfo(ItemType.weapon, ItemCode.pistol, 15));

            itemInfos[ItemType.weapon].Add(new ItemInfo(ItemType.weapon, ItemCode.shotgun, 3));

            itemInfos[ItemType.weapon].Add(new ItemInfo(ItemType.weapon, ItemCode.submachineGun, 30));

            var inventoryInfo = new InventoryInfo(itemInfos);

            playerInfo = new PlayerInfo(characterInfo, inventoryInfo);
        }
        public GameData(GameData gameData)
        {
            survivedTime_Seconds = gameData.survivedTime_Seconds;

            playerInfo = new PlayerInfo(gameData.playerInfo);
        }
    }
    public GameData gameData { get; private set; }

    public bool isGamePaused = false;

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
        var characterPrefabs = Resources.LoadAll<Character>("Prefabs/Character");

        int length = characterPrefabs.Length;

        for(int index = 0; index < length; ++index)
        {
            this.characterPrefabs.Add(characterPrefabs[index].characterCode, characterPrefabs[index]);
        }
        var droppedItemPrefabs = Resources.LoadAll<DroppedItem>("Prefabs/DroppedItem");

        length = droppedItemPrefabs.Length;

        for (int index = 0; index < length; ++index)
        {
            this.droppedItemPrefabs.Add(droppedItemPrefabs[index].itemCode, droppedItemPrefabs[index]);
        }
        DamageableData damageableData = new DamageableData(50, 0f);

        MovementData movementData = new MovementData(2f, 2f);

        List<SkillData> skillDatas = new List<SkillData>();

        skillDatas.Add(new SkillData(1f, 1f, 1f, 5));

        characterDatas.Add(CharacterCode.crazyBird, new CharacterData(damageableData, movementData, skillDatas));

        damageableData = new DamageableData(2000, 0f);

        movementData = new MovementData(1f, 1f);

        skillDatas = new List<SkillData>();

        skillDatas.Add(new SkillData(5f, 2f, 2f, 10));

        characterDatas.Add(CharacterCode.crazyCow, new CharacterData(damageableData, movementData, skillDatas));

        damageableData = new DamageableData(100, 0f);

        movementData = new MovementData(1.5f, 1.5f);

        skillDatas = new List<SkillData>();

        skillDatas.Add(new SkillData(5f, 0.3f, 1.5f, 5));

        characterDatas.Add(CharacterCode.crazySpider, new CharacterData(damageableData, movementData, skillDatas));

        damageableData = new DamageableData(100, 0f);

        movementData = new MovementData();

        characterDatas.Add(CharacterCode.dummy, new CharacterData(damageableData, movementData));

        damageableData = new DamageableData(100, 0f);

        movementData = new MovementData(3f, 6f, 1, 5f);

        characterDatas.Add(CharacterCode.player, new CharacterData(damageableData, movementData));

        playerData = new PlayerData(new Vector2(-55f, 0f), new Vector2(55f, 0f));

        itemDatas.Add(ItemCode.pistolAmmo, new ItemData(150));

        itemDatas.Add(ItemCode.shotgunAmmo, new ItemData(30));

        itemDatas.Add(ItemCode.submachineGunAmmo, new ItemData(300));

        itemDatas.Add(ItemCode.grenade, new ItemData(3, 10f, 0f, 0f, 0, 100));

        itemDatas.Add(ItemCode.medikit, new ItemData(3, 10f, 0f, 0f, 25, 0));

        itemDatas.Add(ItemCode.pistol, new ItemData(1, 0f, 0.5f, false, 0.3f, 2f, 0, 0f, 1f, 1f, 5, 100f, 1f, 15, 0, 0));

        itemDatas.Add(ItemCode.shotgun, new ItemData(1, 0f, 0.5f, false, 1f, 2.5f, 0, 0f, 10f, 5f, 5, 100f, 1f, 3, 0, 0));

        itemDatas.Add(ItemCode.submachineGun, new ItemData(1, 0f, 0.5f, true, 0.1f, 2f, 0, 0f, 1f, 1f, 5, 100f, 1f, 30, 0, 0));

        gameDataPath = Application.dataPath + "/GameData.cfg";

        if (new FileInfo(gameDataPath).Exists == true)
        {
            LoadGameData();
        }
        else
        {
            NewGameData();

            //SaveGameData();
        }
    }
    public void LoadGameData()
    {
        gameData = JsonConvert.DeserializeObject<GameData>(System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(File.ReadAllText(gameDataPath))));
    }
    public void NewGameData()
    {
        gameData = new GameData();
    }
    public void SaveGameData()
    {
        File.WriteAllText(gameDataPath, System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(gameData))));
    }
    private IEnumerator RecordSurvivedTime()
    {
        while(true)
        {
            if(isGamePaused == false)
            {
                gameData.survivedTime_Seconds += Time.deltaTime;
            }
        }
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
        #if UNITY_EDITOR == true

            UnityEditor.EditorApplication.isPlaying = false;

        #else

            Application.Quit();

        #endif
    }
}