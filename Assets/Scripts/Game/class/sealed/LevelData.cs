
using System.Collections.Generic;

using UnityEngine;

public sealed class LevelData
{
    public Dictionary<CharacterCode, CharacterData> characterDatas { get; private set; } = new Dictionary<CharacterCode, CharacterData>();

    public Dictionary<CharacterCode, CharacterInfo> characterInfos { get; private set; } = new Dictionary<CharacterCode, CharacterInfo>();

    public PlayerData playerData { get; private set; }

    public Dictionary<ItemCode, ItemData> itemDatas { get; private set; } = new Dictionary<ItemCode, ItemData>();

    public Dictionary<ItemCode, ItemInfo> itemInfos { get; private set; } = new Dictionary<ItemCode, ItemInfo>();

    public Dictionary<SceneCode, StageData> stageDatas { get; private set; } = new Dictionary<SceneCode, StageData>();

    public LevelData()
    {
        InitializeCharacterDatas();

        InitializeCharacterInfos();

        InitializePlayerData();

        InitializeItemDatas();

        InitializeItemInfos();

        InitializeStageDatas();
    }

    private void InitializeCharacterDatas()
    {
        DamageableData damageableData;

        MovementData movementData;

        List<StatusEffectData> statusEffectDatas;

        StatusEffectData statusEffectData;

        List<SkillData> skillDatas;

        DamageableInfo_LevelUpData damageableInfo_LevelUpData;

        //TransformInfo_LevelUpData transformInfo_LevelUpData;

        MovementInfo_LevelUpData movementInfo_LevelUpData;

        List<SkillInfo_LevelUpData> skillInfo_LevelUpDatas;

        List<StatusEffectInfo_LevelUpData> statusEffectInfo_LevelUpDatas;

        StatusEffectInfo_LevelUpData statusEffectInfo_LevelUpData;

        SkillInfo_LevelUpData skillInfo_LevelUpData;

        CharacterInfo_LevelUpData characterInfo_LevelUpData;

        #region Crazy Bird

        damageableData = new DamageableData(100, 0f);

        movementData = new MovementData(1f, 1f);

        skillDatas = new List<SkillData>();

        skillDatas.Add(new SkillData(0f, 0f, 0f, 0, null));

        damageableInfo_LevelUpData = new DamageableInfo_LevelUpData(0f, 0f);

        skillInfo_LevelUpDatas = new List<SkillInfo_LevelUpData>();

        skillInfo_LevelUpData = new SkillInfo_LevelUpData(0f, 0f, 0f, 0f, null);

        skillInfo_LevelUpDatas.Add(skillInfo_LevelUpData);

        characterInfo_LevelUpData = new CharacterInfo_LevelUpData(damageableInfo_LevelUpData, null, null, skillInfo_LevelUpDatas);

        characterDatas.Add(CharacterCode.crazyBird, new CharacterData(damageableData, movementData, skillDatas, characterInfo_LevelUpData));

        #endregion

        #region Crazy Cow

        damageableData = new DamageableData(100, 0f);

        movementData = new MovementData(1f, 1f);

        skillDatas = new List<SkillData>();

        skillDatas.Add(new SkillData(0f, 0f, 0f, 0, null));

        damageableInfo_LevelUpData = new DamageableInfo_LevelUpData(0f, 0f);

        skillInfo_LevelUpDatas = new List<SkillInfo_LevelUpData>();

        skillInfo_LevelUpData = new SkillInfo_LevelUpData(0f, 0f, 0f, 0f, null);

        skillInfo_LevelUpDatas.Add(skillInfo_LevelUpData);

        characterInfo_LevelUpData = new CharacterInfo_LevelUpData(damageableInfo_LevelUpData, null, null, skillInfo_LevelUpDatas);

        characterDatas.Add(CharacterCode.crazyCow, new CharacterData(damageableData, movementData, skillDatas, characterInfo_LevelUpData ));

        #endregion

        #region Crazy Spider

        damageableData = new DamageableData(100, 0f);

        movementData = new MovementData(2f, 0f);

        skillDatas = new List<SkillData>();

        skillDatas.Add(new SkillData(5f, 1f, 0.25f, 5, null));

        statusEffectDatas = new List<StatusEffectData>();

        statusEffectData = new StatusEffectData(StatusEffectCode.slow, 0.25f, 1f);

        statusEffectDatas.Add(statusEffectData);

        skillDatas.Add(new SkillData(5f, 5f, 1f, 0, statusEffectDatas));

        damageableInfo_LevelUpData = new DamageableInfo_LevelUpData(0.1f, 0f);

        movementInfo_LevelUpData = new MovementInfo_LevelUpData(0f, 0f);

        skillInfo_LevelUpDatas = new List<SkillInfo_LevelUpData>();

        skillInfo_LevelUpData = new SkillInfo_LevelUpData(0f, 0f, 0f, 0.1f, null);

        skillInfo_LevelUpDatas.Add(skillInfo_LevelUpData);

        statusEffectInfo_LevelUpDatas = new List<StatusEffectInfo_LevelUpData>();

        statusEffectInfo_LevelUpData = new StatusEffectInfo_LevelUpData(0f, 0f);

        statusEffectInfo_LevelUpDatas.Add(statusEffectInfo_LevelUpData);

        skillInfo_LevelUpData = new SkillInfo_LevelUpData(0f, 0f, 0f, 0f, statusEffectInfo_LevelUpDatas);

        skillInfo_LevelUpDatas.Add(skillInfo_LevelUpData);

        characterInfo_LevelUpData = new CharacterInfo_LevelUpData(damageableInfo_LevelUpData, null, movementInfo_LevelUpData, skillInfo_LevelUpDatas);

        characterDatas.Add(CharacterCode.crazySpider, new CharacterData(damageableData, movementData, skillDatas, characterInfo_LevelUpData));

        #endregion

        #region Dummy

        damageableData = new DamageableData(100, 0f);

        movementData = new MovementData();

        characterDatas.Add(CharacterCode.dummy, new CharacterData(damageableData, movementData, null, null));

        #endregion

        #region Garbage Bag

        damageableData = new DamageableData(1, 0f);

        characterDatas.Add(CharacterCode.garbageBag, new CharacterData(damageableData, null, null, null));

        #endregion

        #region Player

        damageableData = new DamageableData(100, 0f);

        movementData = new MovementData(3f, 6f, 1, 5f);

        damageableInfo_LevelUpData = new DamageableInfo_LevelUpData(0f, 0f);

        movementInfo_LevelUpData = new MovementInfo_LevelUpData(0f, 0f);

        characterInfo_LevelUpData = new CharacterInfo_LevelUpData(damageableInfo_LevelUpData, null, movementInfo_LevelUpData, null);

        characterDatas.Add(CharacterCode.player, new CharacterData(damageableData, movementData, null, characterInfo_LevelUpData));

        #endregion
    }

    private void InitializeCharacterInfos()
    {
        List<SkillInfo> skillInfos;

        List<StatusEffectInfo> statusEffectInfos;

        #region Crazy Bird

        skillInfos = new List<SkillInfo>();

        skillInfos.Add(new SkillInfo());

        characterInfos.Add(CharacterCode.crazyBird, new CharacterInfo(new DamageableInfo(), null, new MovementInfo(), skillInfos));

        #endregion

        #region Crazy Cow

        skillInfos = new List<SkillInfo>();

        skillInfos.Add(new SkillInfo());

        characterInfos.Add(CharacterCode.crazyCow, new CharacterInfo(new DamageableInfo(), null, new MovementInfo(), skillInfos));

        #endregion

        #region Crazy Spider

        skillInfos = new List<SkillInfo>();

        skillInfos.Add(new SkillInfo());

        statusEffectInfos = new List<StatusEffectInfo>();

        statusEffectInfos.Add(new StatusEffectInfo());

        skillInfos.Add(new SkillInfo(statusEffectInfos));

        characterInfos.Add(CharacterCode.crazySpider, new CharacterInfo(new DamageableInfo(), null, new MovementInfo(), skillInfos));

        #endregion

        #region Dummy

        characterInfos.Add(CharacterCode.dummy, new CharacterInfo(new DamageableInfo(), null, null, null));

        #endregion

        #region Garbage Bag

        characterInfos.Add(CharacterCode.garbageBag, new CharacterInfo(new DamageableInfo(), null, null, null));

        #endregion
    }

    private void InitializePlayerData()
    {
        playerData = new PlayerData(new Vector2(-55f, 0f), new Vector2(55f, 0f));
    }

    private void InitializeItemDatas()
    {
        itemDatas.Add(ItemCode.pistolAmmo, new ItemData(150));

        itemDatas.Add(ItemCode.shotgunAmmo, new ItemData(30));

        itemDatas.Add(ItemCode.submachineGunAmmo, new ItemData(300));

        itemDatas.Add(ItemCode.grenade, new ItemData(3, 10f, 0f, 0f, 0, 100));

        itemDatas.Add(ItemCode.medikit, new ItemData(3, 10f, 0f, 0f, 25, 0));

        itemDatas.Add(ItemCode.pistol, new ItemData(1, 0f, 0.5f, false, 0.3f, 2f, 0, 0f, 1f, 0.5f, 10, 100f, 1f, 10, 0, 0));

        itemDatas.Add(ItemCode.shotgun, new ItemData(1, 0f, 0.5f, false, 1f, 2.5f, 0, 0f, 15f, 5f, 10, 100f, 0.5f, 3, 0, 0));

        itemDatas.Add(ItemCode.submachineGun, new ItemData(1, 0f, 0.5f, true, 0.1f, 2f, 0, 0f, 1f, 0.5f, 10, 100f, 1f, 30, 0, 0));
    }

    private void InitializeItemInfos()
    {
        ItemType itemType;

        ItemCode itemCode;

        itemType = ItemType.ammo;

        itemCode = ItemCode.ammoPack;

        itemInfos.Add(itemCode, new ItemInfo(itemType, itemCode));

        itemCode = ItemCode.pistolAmmo;

        itemInfos.Add(itemCode, new ItemInfo(itemType, itemCode, 15));

        itemCode = ItemCode.shotgunAmmo;

        itemInfos.Add(itemCode, new ItemInfo(itemType, itemCode, 3));

        itemCode = ItemCode.submachineGunAmmo;

        itemInfos.Add(itemCode, new ItemInfo(itemType, itemCode, 30));

        itemType = ItemType.consumable;

        itemCode = ItemCode.grenade;

        itemInfos.Add(itemCode, new ItemInfo(itemType, itemCode, 1));

        itemCode = ItemCode.medikit;

        itemInfos.Add(itemCode, new ItemInfo(itemType, itemCode, 1));

        itemType = ItemType.weapon;

        itemCode = ItemCode.pistol;

        itemInfos.Add(itemCode, new ItemInfo(itemType, itemCode, 15));

        itemCode = ItemCode.shotgun;

        itemInfos.Add(itemCode, new ItemInfo(itemType, itemCode, 3));

        itemCode = ItemCode.submachineGun;

        itemInfos.Add(itemCode, new ItemInfo(itemType, itemCode, 30));
    }

    private void InitializeStageDatas()
    {
        List<RoundData> roundDatas;

        List<WaveData> waveDatas;

        List<WaveData_SpawnEnemyData_Fixed> waveData_SpawnEnemyDatas_Fixed;

        //List<WaveData_SpawnEnemyData_Random> waveData_SpawnEnemyDatas_Random;
        
        WaveSpawnEnemiesData waveData_EnemySpawnData;

        //List<WaveData_SpawnDroppedItemData_Fixed> waveData_SpawnDroppedItemDatas_Fixed;

        //List<WaveData_SpawnDroppedItemData_Random> waveData_SpawnDroppedItemDatas_Random;
        
        //WaveSpawnDroppedItemsData waveData_DroppedItemSpawnData;

        WaveData waveData;

        RoundData roundData;

        StageData stageData;

        #region City

        roundDatas = new List<RoundData>();

        #region Round 1

        waveDatas = new List<WaveData>();

        #region Wave 1

        waveData_SpawnEnemyDatas_Fixed = new List<WaveData_SpawnEnemyData_Fixed>();

        waveData_SpawnEnemyDatas_Fixed.Add(new WaveData_SpawnEnemyData_Fixed(CharacterCode.crazySpider, 1, 1));

        waveData_EnemySpawnData = new WaveSpawnEnemiesData(1, waveData_SpawnEnemyDatas_Fixed, null);

        waveData = new WaveData(180f, waveData_EnemySpawnData, null);

        waveDatas.Add(waveData);

        #endregion

        #region Wave 2

        waveData_SpawnEnemyDatas_Fixed = new List<WaveData_SpawnEnemyData_Fixed>();

        waveData_SpawnEnemyDatas_Fixed.Add(new WaveData_SpawnEnemyData_Fixed(CharacterCode.crazySpider, 1, 4));

        waveData_EnemySpawnData = new WaveSpawnEnemiesData(4, waveData_SpawnEnemyDatas_Fixed, null);

        waveData = new WaveData(180f, waveData_EnemySpawnData, null);

        waveDatas.Add(waveData);

        #endregion

        roundData = new RoundData(30f, waveDatas);

        roundDatas.Add(roundData);

        #endregion

        #region Round 2

        waveDatas = new List<WaveData>();

        #region Wave 1

        waveData_SpawnEnemyDatas_Fixed = new List<WaveData_SpawnEnemyData_Fixed>();

        waveData_SpawnEnemyDatas_Fixed.Add(new WaveData_SpawnEnemyData_Fixed(CharacterCode.crazySpider, 2, 1));

        waveData_EnemySpawnData = new WaveSpawnEnemiesData(1, waveData_SpawnEnemyDatas_Fixed, null);

        waveData = new WaveData(180f, waveData_EnemySpawnData, null);

        waveDatas.Add(waveData);

        #endregion

        #region Wave 2

        waveData_SpawnEnemyDatas_Fixed = new List<WaveData_SpawnEnemyData_Fixed>();

        waveData_SpawnEnemyDatas_Fixed.Add(new WaveData_SpawnEnemyData_Fixed(CharacterCode.crazySpider, 2, 4));

        waveData_EnemySpawnData = new WaveSpawnEnemiesData(4, waveData_SpawnEnemyDatas_Fixed, null);

        waveData = new WaveData(180f, waveData_EnemySpawnData, null);

        waveDatas.Add(waveData);

        #endregion

        roundData = new RoundData(30f, waveDatas);

        roundDatas.Add(roundData);

        #endregion

        #region Round 3

        waveDatas = new List<WaveData>();

        #region Wave 1

        waveData_SpawnEnemyDatas_Fixed = new List<WaveData_SpawnEnemyData_Fixed>();

        waveData_SpawnEnemyDatas_Fixed.Add(new WaveData_SpawnEnemyData_Fixed(CharacterCode.crazySpider, 2, 2));

        waveData_EnemySpawnData = new WaveSpawnEnemiesData(2, waveData_SpawnEnemyDatas_Fixed, null);

        waveData = new WaveData(180f, waveData_EnemySpawnData, null);

        waveDatas.Add(waveData);

        #endregion

        #region Wave 2

        waveData_SpawnEnemyDatas_Fixed = new List<WaveData_SpawnEnemyData_Fixed>();

        waveData_SpawnEnemyDatas_Fixed.Add(new WaveData_SpawnEnemyData_Fixed(CharacterCode.crazySpider, 2, 8));

        waveData_EnemySpawnData = new WaveSpawnEnemiesData(8, waveData_SpawnEnemyDatas_Fixed, null);

        waveData = new WaveData(180f, waveData_EnemySpawnData, null);

        waveDatas.Add(waveData);

        #endregion

        #region Wave 3

        waveData_SpawnEnemyDatas_Fixed = new List<WaveData_SpawnEnemyData_Fixed>();

        waveData_SpawnEnemyDatas_Fixed.Add(new WaveData_SpawnEnemyData_Fixed(CharacterCode.crazySpider, 20, 1));

        waveData_EnemySpawnData = new WaveSpawnEnemiesData(1, waveData_SpawnEnemyDatas_Fixed, null);

        waveData = new WaveData(180f, waveData_EnemySpawnData, null);

        waveDatas.Add(waveData);

        #endregion

        roundData = new RoundData(30f, waveDatas);

        roundDatas.Add(roundData);

        #endregion

        stageData = new StageData(roundDatas);

        stageDatas.Add(SceneCode.city, stageData);

        #endregion
    }
}