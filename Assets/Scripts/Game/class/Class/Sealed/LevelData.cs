
using System.Collections.Generic;

using UnityEngine;

public sealed class LevelData
{
    public Dictionary<CharacterCode, CharacterData> characterDatas { get; private set; } = new Dictionary<CharacterCode, CharacterData>();

    public Dictionary<ItemCode, ItemData> itemDatas { get; private set; } = new Dictionary<ItemCode, ItemData>();

    public PlayerData playerData { get; private set; }

    public Dictionary<SceneCode, StageData> stageDatas { get; private set; } = new Dictionary<SceneCode, StageData>();

    public LevelData()
    {
        InitializeCharacterDatas();

        InitializeItemDatas();

        InitializePlayerData();

        InitializeStageDatas();
    }

    private void InitializeCharacterDatas()
    {
        List<StatusEffectData> statusEffectDatas;

        List<SkillData> skillDatas;

        List<SkillInfo.LevelUpData> skillInfo_LevelUpDatas;

        List<StatusEffectInfo.LevelUpData> statusEffectInfo_LevelUpDatas;

        CharacterInfo.LevelUpData characterInfo_LevelUpData;

        #region Crazy Bird

        skillDatas = new List<SkillData>()
        {
            new SkillData(0f, 0f, 0f, 0f, null, null, null)
        };

        skillInfo_LevelUpDatas = new List<SkillInfo.LevelUpData>();

        skillInfo_LevelUpDatas.Add(new SkillInfo.LevelUpData(null, null, null));

        characterInfo_LevelUpData = new CharacterInfo.LevelUpData(new DamageableInfo.LevelUpData(0f), new ExperienceInfo.LevelUpData(0f, 0f), null, skillInfo_LevelUpDatas);

        characterDatas.Add(CharacterCode.crazyBird, new CharacterData(new DamageableData(100, 0f), new ExperienceData(0, 0), new MovementData(1f, 1f), skillDatas, characterInfo_LevelUpData));

        #endregion

        #region Crazy Cow

        skillDatas = new List<SkillData>()
        {
            new SkillData(0f, 0f, 0f, 0f, null, null, null)
        };

        skillInfo_LevelUpDatas = new List<SkillInfo.LevelUpData>();

        skillInfo_LevelUpDatas.Add(new SkillInfo.LevelUpData(null, null, null));

        characterInfo_LevelUpData = new CharacterInfo.LevelUpData(new DamageableInfo.LevelUpData(0f), new ExperienceInfo.LevelUpData(0f, 0f), null, skillInfo_LevelUpDatas);

        characterDatas.Add(CharacterCode.crazyCow, new CharacterData(new DamageableData(100, 0f), new ExperienceData(0, 0), new MovementData(1f, 1f), skillDatas, characterInfo_LevelUpData ));

        #endregion

        #region Crazy Spider

        skillDatas = new List<SkillData>();

        statusEffectDatas = new List<StatusEffectData>()
        {
            new StatusEffectData(StatusEffectCode.slow, 0.25f, 1f),
        };

        skillDatas.Add(new SkillData(10f, 5f, 0f, 0.25f, null, new SkillData.RangedData(ProjectileCode.spiderWebBullet, 0f, 0f, 25f, 1f, 0, null, statusEffectDatas), null));

        skillDatas.Add(new SkillData(10f, 1f, 0f, 0.25f, null, new SkillData.RangedData(ProjectileCode.poisonBullet, 0f, 0f, 25f, 1f, 5, null, null), null));

        skillInfo_LevelUpDatas = new List<SkillInfo.LevelUpData>();

        statusEffectInfo_LevelUpDatas = new List<StatusEffectInfo.LevelUpData>()
        {
            new StatusEffectInfo.LevelUpData(0f, 0f),
        };

        skillInfo_LevelUpDatas.Add(new SkillInfo.LevelUpData(null, new SkillInfo.RangedInfo.LevelUpData(0f, null, statusEffectInfo_LevelUpDatas), null));

        skillInfo_LevelUpDatas.Add(new SkillInfo.LevelUpData(null, new SkillInfo.RangedInfo.LevelUpData(0.1f, null, null), null));

        characterInfo_LevelUpData = new CharacterInfo.LevelUpData(new DamageableInfo.LevelUpData(0.1f), new ExperienceInfo.LevelUpData(20f, 0f), new MovementInfo.LevelUpData(0f), skillInfo_LevelUpDatas);

        characterDatas.Add(CharacterCode.crazySpider, new CharacterData(new DamageableData(100, 0f), new ExperienceData(20, 0), new MovementData(2f, 0f), skillDatas, characterInfo_LevelUpData));

        #endregion

        #region Dummy

        characterDatas.Add(CharacterCode.dummy, new CharacterData(new DamageableData(100, 0f), null, new MovementData(), null, null));

        #endregion

        #region Garbage Bag

        characterDatas.Add(CharacterCode.garbageBag, new CharacterData(new DamageableData(1, 0f), null, null, null, null));

        #endregion

        #region Player

        characterInfo_LevelUpData = new CharacterInfo.LevelUpData(new DamageableInfo.LevelUpData(0f), new ExperienceInfo.LevelUpData(0, 100), null, null);

        characterDatas.Add(CharacterCode.player, new CharacterData(new DamageableData(100, 0f), new ExperienceData(0, 100), new MovementData(3f, 6f, 1, 5f), null, characterInfo_LevelUpData));

        #endregion
    }

    private void InitializeItemDatas()
    {
        ItemType itemType;

        ItemCode itemCode;

        List<SkillData> skillDatas;

        itemType = ItemType.ammo;

        itemCode = ItemCode.pistolAmmo;

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 1, 150, 0f, 0f, false, null));

        itemCode = ItemCode.shotgunAmmo;

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 1, 30, 0f, 0f, false, null));

        itemCode = ItemCode.submachineGunAmmo;

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 1, 300, 0f, 0f, false, null));

        itemType = ItemType.consumable;

        itemCode = ItemCode.grenade;

        skillDatas = new List<SkillData>()
        {
            new SkillData(0f, 0f, 0f, 0f, null, new SkillData.RangedData(ProjectileCode.grenade, 0f, 0f, 5f, 5f, 0, new ExplosionData(5f, 100, 1f, null), null), null),
        };

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 3, 0, 1f, 0f, false, skillDatas));

        itemCode = ItemCode.medikit;

        skillDatas = new List<SkillData>()
        {
            new SkillData(0f, 0f, 0f, 0f, null, null, null),
        };

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 3, 0, 0f, 0f, false, skillDatas));

        itemType = ItemType.weapon;

        itemCode = ItemCode.pistol;

        skillDatas = new List<SkillData>()
        {
            new SkillData(0f, 0f, 0f, 0.3f, null, new SkillData.RangedData(ProjectileCode.gunBullet, 1f, 0.5f, 100f,  1f, 10, null, null), null),
        };

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 1, 10, 0.5f, 2f, false, skillDatas));

        itemCode = ItemCode.shotgun;

        skillDatas = new List<SkillData>()
        {
            new SkillData(0f, 0f, 0f, 1f, null, new SkillData.RangedData(ProjectileCode.gunBullet, 15f, 5f, 100f, 0.5f, 10, null, null), null),
        };

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 1, 3, 0.5f, 2.5f, false, skillDatas));

        itemCode = ItemCode.submachineGun;

        skillDatas = new List<SkillData>()
        {
            new SkillData(0f, 0f, 0f, 0.1f, null, new SkillData.RangedData(ProjectileCode.gunBullet, 1f, 0.5f, 100f, 1f, 10, null, null), null),
        };

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 1, 30, 0.5f, 2f, true, skillDatas));
    }

    private void InitializePlayerData()
    {
        var itemDatas = new Dictionary<ItemType, List<ItemData>>()
        {
            {
                ItemType.ammo, new List<ItemData>()
                {
                    this.itemDatas[ItemCode.pistolAmmo],

                    this.itemDatas[ItemCode.shotgunAmmo],

                    this.itemDatas[ItemCode.submachineGunAmmo],
                }
            },

            {
                ItemType.consumable, new List<ItemData>()
                {
                    this.itemDatas[ItemCode.medikit]
                }
            },

            {
                ItemType.weapon, new List<ItemData>()
                {
                    this.itemDatas[ItemCode.pistol],

                    this.itemDatas[ItemCode.shotgun],

                    this.itemDatas[ItemCode.submachineGun],
                }
            },
        };

        var counts = new Dictionary<ItemType, List<int>>()
        {
            {
                ItemType.ammo, new List<int>()
                {
                    135,

                    27,

                    270,
                }
            },

            {
                ItemType.consumable, new List<int>()
                {
                    3,
                }
            },

            {
                ItemType.weapon, new List<int>()
                {
                    15,

                    3,

                    30,
                }
            },
        };

        playerData = new PlayerData(new Vector2(-55f, 0f), new Vector2(55f, 0f), characterDatas[CharacterCode.player], new PlayerInventoryData(itemDatas, counts));
    }

    private void InitializeStageDatas()
    {
        List<RoundData> roundDatas;

        List<WaveData> waveDatas;

        List<WaveData_SpawnEnemyData_Fixed> waveData_SpawnEnemyDatas_Fixed;

        //List<WaveData_SpawnEnemyData_Random> waveData_SpawnEnemyDatas_Random;

        //List<WaveData_SpawnDroppedItemData_Fixed> waveData_SpawnDroppedItemDatas_Fixed;

        //List<WaveData_SpawnDroppedItemData_Random> waveData_SpawnDroppedItemDatas_Random;

        //WaveSpawnDroppedItemsData waveData_DroppedItemSpawnData;

        #region City

        roundDatas = new List<RoundData>();

        #region Round 1

        waveDatas = new List<WaveData>();

        #region Wave 1

        waveData_SpawnEnemyDatas_Fixed = new List<WaveData_SpawnEnemyData_Fixed>()
        {
        new WaveData_SpawnEnemyData_Fixed(CharacterCode.crazySpider, 1, 1),
        };

        waveDatas.Add(new WaveData(180f, new WaveSpawnEnemiesData(1, waveData_SpawnEnemyDatas_Fixed, null), null));

        #endregion

        #region Wave 2

        waveData_SpawnEnemyDatas_Fixed = new List<WaveData_SpawnEnemyData_Fixed>()
        {
            new WaveData_SpawnEnemyData_Fixed(CharacterCode.crazySpider, 1, 4),
        };

        waveDatas.Add(new WaveData(180f, new WaveSpawnEnemiesData(4, waveData_SpawnEnemyDatas_Fixed, null), null));

        #endregion

        roundDatas.Add(new RoundData(180f, waveDatas));

        #endregion

        #region Round 2

        waveDatas = new List<WaveData>();

        #region Wave 1

        waveData_SpawnEnemyDatas_Fixed = new List<WaveData_SpawnEnemyData_Fixed>()
        {
            new WaveData_SpawnEnemyData_Fixed(CharacterCode.crazySpider, 2, 1),
        };

        waveDatas.Add(new WaveData(180f, new WaveSpawnEnemiesData(1, waveData_SpawnEnemyDatas_Fixed, null), null));

        #endregion

        #region Wave 2

        waveData_SpawnEnemyDatas_Fixed = new List<WaveData_SpawnEnemyData_Fixed>()
        {
            new WaveData_SpawnEnemyData_Fixed(CharacterCode.crazySpider, 2, 4),
        };

        waveDatas.Add(new WaveData(180f, new WaveSpawnEnemiesData(4, waveData_SpawnEnemyDatas_Fixed, null), null));

        #endregion

        roundDatas.Add(new RoundData(180f, waveDatas));

        #endregion

        #region Round 3

        waveDatas = new List<WaveData>();

        #region Wave 1

        waveData_SpawnEnemyDatas_Fixed = new List<WaveData_SpawnEnemyData_Fixed>()
        {
           new WaveData_SpawnEnemyData_Fixed(CharacterCode.crazySpider, 2, 2),
        };

        waveDatas.Add(new WaveData(180f, new WaveSpawnEnemiesData(2, waveData_SpawnEnemyDatas_Fixed, null), null));

        #endregion

        #region Wave 2

        waveData_SpawnEnemyDatas_Fixed = new List<WaveData_SpawnEnemyData_Fixed>()
        {
            new WaveData_SpawnEnemyData_Fixed(CharacterCode.crazySpider, 2, 8),
        };

        waveDatas.Add(new WaveData(180f, new WaveSpawnEnemiesData(8, waveData_SpawnEnemyDatas_Fixed, null), null));

        #endregion

        #region Wave 3

        waveData_SpawnEnemyDatas_Fixed = new List<WaveData_SpawnEnemyData_Fixed>()
        {
            new WaveData_SpawnEnemyData_Fixed(CharacterCode.crazySpider, 20, 1),
        };

        waveDatas.Add(new WaveData(180f, new WaveSpawnEnemiesData(1, waveData_SpawnEnemyDatas_Fixed, null), null));

        #endregion

        roundDatas.Add(new RoundData(180f, waveDatas));

        #endregion

        stageDatas.Add(SceneCode.city, new StageData(roundDatas));

        #endregion

        #region Desert

        roundDatas = new List<RoundData>();

        #region Round 1

        waveDatas = new List<WaveData>();

        #region Wave 1

        waveData_SpawnEnemyDatas_Fixed = new List<WaveData_SpawnEnemyData_Fixed>()
        {
            new WaveData_SpawnEnemyData_Fixed(CharacterCode.crazySpider, 1, 1),
        };

        waveDatas.Add(new WaveData(180f, new WaveSpawnEnemiesData(1, waveData_SpawnEnemyDatas_Fixed, null), null));

        #endregion

        roundDatas.Add(new RoundData(180f, waveDatas));

        #endregion

        stageDatas.Add(SceneCode.desert, new StageData(roundDatas));

        #endregion
    }
}