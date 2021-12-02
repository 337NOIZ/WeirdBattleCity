
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
        List<SkillData> skillDatas;

        List<StatusEffectData> statusEffectDatas;

        List<SkillInfo.LevelUpData> skillInfo_LevelUpDatas;

        List<StatusEffectInfo.LevelUpData> statusEffectInfo_LevelUpDatas;

        CharacterInfo.LevelUpData characterInfo_LevelUpData;

        #region Crazy Bird

        skillDatas = new List<SkillData>();

        skillDatas.Add(new SkillData(0f, 0f, 0f, 0f, null, null, null));

        skillInfo_LevelUpDatas = new List<SkillInfo.LevelUpData>();

        skillInfo_LevelUpDatas.Add(new SkillInfo.LevelUpData(null, null, null));

        characterInfo_LevelUpData = new CharacterInfo.LevelUpData(0f, new DamageableInfo.LevelUpData(0f), new ExperienceInfo.LevelUpData(0f, 0f), skillInfo_LevelUpDatas);

        characterDatas.Add(CharacterCode.crazyBird, new CharacterData(0f, new DamageableData(100, 0f), new ExperienceData(0f, 0f), skillDatas, new MovementData(1f, 1f), characterInfo_LevelUpData));

        #endregion

        #region Crazy Cow

        skillDatas = new List<SkillData>();

        skillDatas.Add(new SkillData(0f, 0f, 0f, 0f, null, null, null));

        skillInfo_LevelUpDatas = new List<SkillInfo.LevelUpData>();

        skillInfo_LevelUpDatas.Add(new SkillInfo.LevelUpData(null, null, null));

        characterInfo_LevelUpData = new CharacterInfo.LevelUpData(0f, new DamageableInfo.LevelUpData(0f), new ExperienceInfo.LevelUpData(0f, 0f), skillInfo_LevelUpDatas);

        characterDatas.Add(CharacterCode.crazyCow, new CharacterData(0f, new DamageableData(100f, 0f), new ExperienceData(0f, 0f), skillDatas, new MovementData(1f, 1f), characterInfo_LevelUpData ));

        #endregion

        #region Crazy Spider

        skillDatas = new List<SkillData>();

        statusEffectDatas = new List<StatusEffectData>()
        {
            new StatusEffectData(StatusEffectCode.slow, 0.25f, 3f),
        };

        skillDatas.Add(new SkillData(10f, 5f, 0f, 0.25f, null, new SkillData.RangedData(ProjectileCode.spiderWebBullet, 0f, 0f, new ProjectileData(25f, 1f, 0f, null, null, statusEffectDatas)), null));

        skillDatas.Add(new SkillData(10f, 1f, 0f, 0.25f, null, new SkillData.RangedData(ProjectileCode.poisonBullet, 0f, 0f, new ProjectileData(25f, 1f, 3f, null, null, null)), null));

        skillInfo_LevelUpDatas = new List<SkillInfo.LevelUpData>();

        statusEffectInfo_LevelUpDatas = new List<StatusEffectInfo.LevelUpData>()
        {
            new StatusEffectInfo.LevelUpData(0f, 0f),
        };

        skillInfo_LevelUpDatas.Add(new SkillInfo.LevelUpData(null, new SkillInfo.RangedInfo.LevelUpData(new ProjectileInfo.LevelUpData(0f, null, null, statusEffectInfo_LevelUpDatas)), null));

        skillInfo_LevelUpDatas.Add(new SkillInfo.LevelUpData(null, new SkillInfo.RangedInfo.LevelUpData(new ProjectileInfo.LevelUpData(1f, null, null, null)), null));

        characterInfo_LevelUpData = new CharacterInfo.LevelUpData(1f, new DamageableInfo.LevelUpData(10f), new ExperienceInfo.LevelUpData(0f, 5f), skillInfo_LevelUpDatas);

        characterDatas.Add(CharacterCode.crazySpider, new CharacterData(10f, new DamageableData(100f, 0f), new ExperienceData(0f, 15f), skillDatas, new MovementData(2f, 0f), characterInfo_LevelUpData));

        #endregion

        #region Dummy

        characterDatas.Add(CharacterCode.dummy, new CharacterData(0f, new DamageableData(100f, 0f), null, null, new MovementData(), null));

        #endregion

        #region Garbage Bag

        characterDatas.Add(CharacterCode.garbageBag, new CharacterData(0f, new DamageableData(1f, 0f), null, null, null, null));

        #endregion

        #region Player

        characterInfo_LevelUpData = new CharacterInfo.LevelUpData(0f, new DamageableInfo.LevelUpData(0f), new ExperienceInfo.LevelUpData(100f, 0f), null);

        characterDatas.Add(CharacterCode.player, new CharacterData(0f, new DamageableData(100f, 0f), new ExperienceData(100f, 0f), null, new MovementData(3f, 6f, 1, 5f), characterInfo_LevelUpData));

        #endregion
    }

    private void InitializeItemDatas()
    {
        ItemType itemType;

        ItemCode itemCode;

        List<SkillData> skillDatas;

        itemType = ItemType.ammo;

        itemCode = ItemCode.pistolAmmo;

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 50f, 0f, 0f, 0f, false, null));

        itemCode = ItemCode.shotgunAmmo;

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 15f, 0f, 0f, 0f, false, null));

        itemCode = ItemCode.submachineGunAmmo;

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 150f, 0f, 0f, 0f, false, null));

        itemType = ItemType.consumable;

        itemCode = ItemCode.grenade;

        skillDatas = new List<SkillData>()
        {
            new SkillData(0f, 10f, 0f, 1f, null, new SkillData.RangedData(ProjectileCode.grenade, 1f, 0f, new ProjectileData(10f, 3f, 0, new DamageableData(1f, 0f), new ExplosionData(ParticleEffectCode.explosion, 5f, 100, 1f, null), null)), null),
        };

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 5, 0, 0f, 0f, false, skillDatas));

        itemCode = ItemCode.medikit;

        skillDatas = new List<SkillData>()
        {
            new SkillData(0f, 0f, 0f, 0f, null, null, null),
        };

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 5, 0, 0f, 0f, false, skillDatas));

        itemType = ItemType.weapon;

        itemCode = ItemCode.pistol;

        skillDatas = new List<SkillData>()
        {
            new SkillData(0f, 0f, 0f, 0.3f, null, new SkillData.RangedData(ProjectileCode.gunBullet, 1f, 0.5f, new ProjectileData(100f,  1f, 10, null, null, null)), null),
        };

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 1, 10, 0.5f, 2f, false, skillDatas));

        itemCode = ItemCode.shotgun;

        skillDatas = new List<SkillData>()
        {
            new SkillData(0f, 0f, 0f, 1f, null, new SkillData.RangedData(ProjectileCode.gunBullet, 15f, 5f, new ProjectileData(100f, 0.5f, 10, null, null, null)), null),
        };

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 1, 3, 0.5f, 2.5f, false, skillDatas));

        itemCode = ItemCode.submachineGun;

        skillDatas = new List<SkillData>()
        {
            new SkillData(0f, 0f, 0f, 0.1f, null, new SkillData.RangedData(ProjectileCode.gunBullet, 1f, 0.5f, new ProjectileData(100f, 1f, 10, null, null, null)), null),
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
                    this.itemDatas[ItemCode.grenade],

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
                    4000,

                    1200,

                    12000,
                }
            },

            {
                ItemType.consumable, new List<int>()
                {
                    500,

                    500,
                }
            },

            {
                ItemType.weapon, new List<int>()
                {
                    10,

                    3,

                    30,
                }
            },
        };

        playerData = new PlayerData(characterDatas[CharacterCode.player], new PlayerData.InventoryData(itemDatas, counts));
    }

    private void InitializeStageDatas()
    {
        List<RoundData> roundDatas;

        List<WaveData> waveDatas;

        List<WaveData.EnemySpawnData.SpawnEnemyData> spawnEnemyData;

        #region City

        roundDatas = new List<RoundData>();

        #region Round 1

        waveDatas = new List<WaveData>();

        #region Wave 1

        spawnEnemyData = new List<WaveData.EnemySpawnData.SpawnEnemyData>()
        {
        new WaveData.EnemySpawnData.SpawnEnemyData(CharacterCode.crazySpider, 1, 4, 0),
        };

        waveDatas.Add(new WaveData(180f, new WaveData.EnemySpawnData(1, spawnEnemyData), null));

        #endregion

        #region Wave 2

        spawnEnemyData = new List<WaveData.EnemySpawnData.SpawnEnemyData>()
        {
            new WaveData.EnemySpawnData.SpawnEnemyData(CharacterCode.crazySpider, 2, 4, 0),
        };

        waveDatas.Add(new WaveData(180f, new WaveData.EnemySpawnData(4, spawnEnemyData), null));

        #endregion

        roundDatas.Add(new RoundData(180f, waveDatas));

        #endregion

        #region Round 2

        waveDatas = new List<WaveData>();

        #region Wave 1

        spawnEnemyData = new List<WaveData.EnemySpawnData.SpawnEnemyData>()
        {
            new WaveData.EnemySpawnData.SpawnEnemyData(CharacterCode.crazySpider, 3, 4, 0),
        };

        waveDatas.Add(new WaveData(180f, new WaveData.EnemySpawnData(1, spawnEnemyData), null));

        #endregion

        #region Wave 2

        spawnEnemyData = new List<WaveData.EnemySpawnData.SpawnEnemyData>()
        {
            new WaveData.EnemySpawnData.SpawnEnemyData(CharacterCode.crazySpider, 4, 4, 0),
        };

        waveDatas.Add(new WaveData(180f, new WaveData.EnemySpawnData(4, spawnEnemyData), null));

        #endregion

        roundDatas.Add(new RoundData(180f, waveDatas));

        #endregion

        #region Round 3

        waveDatas = new List<WaveData>();

        #region Wave 1

        spawnEnemyData = new List<WaveData.EnemySpawnData.SpawnEnemyData>()
        {
           new WaveData.EnemySpawnData.SpawnEnemyData(CharacterCode.crazySpider, 5, 4, 0),
        };

        waveDatas.Add(new WaveData(180f, new WaveData.EnemySpawnData(2, spawnEnemyData), null));

        #endregion

        #region Wave 2

        spawnEnemyData = new List<WaveData.EnemySpawnData.SpawnEnemyData>()
        {
            new WaveData.EnemySpawnData.SpawnEnemyData(CharacterCode.crazySpider, 6, 4, 0),
        };

        waveDatas.Add(new WaveData(180f, new WaveData.EnemySpawnData(8, spawnEnemyData), null));

        #endregion

        #region Wave 3

        spawnEnemyData = new List<WaveData.EnemySpawnData.SpawnEnemyData>()
        {
            new WaveData.EnemySpawnData.SpawnEnemyData(CharacterCode.crazySpider, 20, 1, 0),
        };

        waveDatas.Add(new WaveData(180f, new WaveData.EnemySpawnData(1, spawnEnemyData), null));

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

        spawnEnemyData = new List<WaveData.EnemySpawnData.SpawnEnemyData>()
        {
            new WaveData.EnemySpawnData.SpawnEnemyData(CharacterCode.crazySpider, 1, 1, 0),
        };

        waveDatas.Add(new WaveData(180f, new WaveData.EnemySpawnData(1, spawnEnemyData), null));

        #endregion

        roundDatas.Add(new RoundData(180f, waveDatas));

        #endregion

        stageDatas.Add(SceneCode.desert, new StageData(roundDatas));

        #endregion
    }
}