using System.Collections.Generic;

public sealed class ShopItemData
{
    public readonly ItemCode itemCode;

    public readonly int stackCount;

    public readonly int stock;

    public ShopItemData(ItemCode itemCode, int stackCount, int stock)
    {
        this.itemCode = itemCode;

        this.stackCount = stackCount;

        this.stock = stock;
    }

    public ShopItemData(ShopItemData shopItemData)
    {
        itemCode = shopItemData.itemCode;

        stackCount = shopItemData.stackCount;

        stock = shopItemData.stock;
    }
}

public sealed class ShopItemInfo
{
    public readonly ItemCode itemCode;

    public readonly int stackCount;

    public int stock { get; set; }

    public ShopItemInfo(ShopItemData shopItemData)
    {
        itemCode = shopItemData.itemCode;

        stackCount = shopItemData.stackCount;

        stock = shopItemData.stock;
    }

    public ShopItemInfo(ShopItemInfo shopItemInfo)
    {
        itemCode = shopItemInfo.itemCode;

        stackCount = shopItemInfo.stackCount;

        stock = shopItemInfo.stock;
    }
}

public sealed class ShopData
{
    public readonly Dictionary<ItemType, List<ShopItemData>> shopItemDatasDictionary;

    public ShopData(Dictionary<ItemType, List<ShopItemData>> shopItemDatasDictionary)
    {
        this.shopItemDatasDictionary = new Dictionary<ItemType, List<ShopItemData>>(shopItemDatasDictionary);
    }
}

public sealed class ShopInfo
{
    public Dictionary<ItemType, List<ShopItemInfo>> shopItemInfosDictionary { get; private set; }

    public ShopInfo(ShopData shopData)
    {
        var shopItemDatasDictionary = shopData.shopItemDatasDictionary;

        if(shopItemDatasDictionary != null)
        {
            shopItemInfosDictionary = new Dictionary<ItemType, List<ShopItemInfo>>();

            foreach (KeyValuePair<ItemType, List<ShopItemData>> keyValuePair in shopItemDatasDictionary)
            {
                shopItemInfosDictionary.Add(keyValuePair.Key, new List<ShopItemInfo>());

                int index_Max = keyValuePair.Value.Count;

                for (int index = 0; index < index_Max; ++index)
                {
                    shopItemInfosDictionary[keyValuePair.Key].Add(new ShopItemInfo(keyValuePair.Value[index]));
                }
            }
        }
    }

    public ShopInfo(ShopInfo shopInfo)
    {
        shopItemInfosDictionary = new Dictionary<ItemType, List<ShopItemInfo>>(shopInfo.shopItemInfosDictionary);
    }
}

public sealed class LevelData
{
    public Dictionary<ItemCode, ItemData> itemDatas { get; private set; } = new Dictionary<ItemCode, ItemData>();

    public Dictionary<CharacterCode, CharacterData> characterDatas { get; private set; } = new Dictionary<CharacterCode, CharacterData>();

    public Dictionary<SceneCode, StageData> stageDatas { get; private set; } = new Dictionary<SceneCode, StageData>();

    public ShopData shopData { get; private set; }

    public LevelData()
    {
        InitializeItemDatas();

        InitializeCharacterDatas();

        InitializeShopData();

        InitializeStageDatas();
    }

    private void InitializeItemDatas()
    {
        ItemType itemType;

        ItemCode itemCode;

        List<StatusEffectData> statusEffectDatas;

        List<SkillData> skillDatas;

        SkillData.RangedData skillData_RangedData;

        #region Ammo

        itemType = ItemType.Ammo;

        itemCode = ItemCode.PistolAmmo;

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 1, 50, 0f, 0f, 0f, 0f, null, 0f));

        itemCode = ItemCode.ShotgunAmmo;

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 15, 0, 0f, 0f, 0f, 0f, null, 0f));

        itemCode = ItemCode.SubmachineGunAmmo;

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 150, 0, 0f, 0f, 0f, 0f, null, 0f));

        #endregion

        #region Comsumable

        itemType = ItemType.Consumable;

        #region Grenade

        itemCode = ItemCode.Grenade;

        skillDatas = new List<SkillData>()
        {
            new SkillData(0f, 10f, 0, 0f, 0f, 0f, 0, AnimatorManager.FrameCountToSeconds(30), 1f, 0f, null, null, new SkillData.RangedData(ProjectileCode.Grenade, 1f, 0f, new ProjectileData(10f, 3f, 0f, null, new ExplosionData(5f, 100, 1f, null)))),
        };

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 5, 0, 0f, 0f, 0f, 0f, skillDatas, 0f));

        #endregion

        #region Medikit

        itemCode = ItemCode.Medikit;

        statusEffectDatas = new List<StatusEffectData>();

        statusEffectDatas.Add(new StatusEffectData(StatusEffectCode.Healing, 0.2f, 0f));

        skillDatas = new List<SkillData>()
        {
            new SkillData(0f, 0f, 0, 0f, 0f, 0f, 0, 0f, 0f, 0f, statusEffectDatas, null, null),
        };

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 5, 0, 0f, 0f, 0f, 0f, skillDatas, 0f));

        #endregion

        #endregion

        #region Weapon

        #region Pistol

        itemCode = ItemCode.Pistol;

        skillDatas = new List<SkillData>();

        skillData_RangedData = new SkillData.RangedData(ProjectileCode.GunBullet, 1f, 0f, new ProjectileData(100f, 1f, 10f, null, null));

        skillDatas.Add(new SkillData(0f, 0f, 0, 0f, 0f, 0f, 0, AnimatorManager.FrameCountToSeconds(10), 0.5f, 0f, null, null, skillData_RangedData));

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 2, 10, AnimatorManager.FrameCountToSeconds(40), 0.5f, AnimatorManager.FrameCountToSeconds(125), 2f, skillDatas, 0f));

        #endregion

        #region Shotgun

        itemCode = ItemCode.Shotgun;

        skillDatas = new List<SkillData>();

        skillData_RangedData = new SkillData.RangedData(ProjectileCode.GunBullet, 10f, 5f, new ProjectileData(100f, 0.25f, 5f, null, null));

        skillDatas.Add(new SkillData(0f, 0f, 0, 0f, 0f, 0f, 0, AnimatorManager.FrameCountToSeconds(45), 1f, 0f, null, null, skillData_RangedData));

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 1, 3, AnimatorManager.FrameCountToSeconds(40), 0.5f, AnimatorManager.FrameCountToSeconds(125), 2.5f, skillDatas, 0f));

        #endregion

        #region Submachine Gun

        itemCode = ItemCode.SubmachineGun;

        skillDatas = new List<SkillData>();

        skillData_RangedData = new SkillData.RangedData(ProjectileCode.GunBullet, 1f, 1f, new ProjectileData(100f, 1f, 5f, null, null));

        skillDatas.Add(new SkillData(0f, 0f, 0, 0f, 0f, 0f, 0, AnimatorManager.FrameCountToSeconds(10), 0.1f, 0f, null, null, skillData_RangedData));

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 1, 30, AnimatorManager.FrameCountToSeconds(40), 0.5f, AnimatorManager.FrameCountToSeconds(125), 2f, skillDatas, 0f));

        #endregion

        #endregion
    }

    private void InitializeCharacterDatas()
    {
        List<SkillData> skillDatas;

        List<StatusEffectData> statusEffectDatas;

        SkillData.RangedData skillData_RangedData;

        List<SkillInfo.LevelUpData> skillInfo_LevelUpDatas;

        Dictionary<ItemType, List<ItemData>> itemDatas;

        Dictionary<ItemType, List<int>> counts;

        CharacterInfo.LevelUpData characterInfo_LevelUpData;

        #region CrazyRabbit

        skillDatas = new List<SkillData>();

        skillDatas.Add(new SkillData(1f, 0f, 0, 0f, 0f, 0f, 0, AnimatorManager.FrameCountToSeconds(35), 0f, 0f, null, new SkillData.MeleeData(5f, null), null));

        skillInfo_LevelUpDatas = new List<SkillInfo.LevelUpData>();

        skillInfo_LevelUpDatas.Add(new SkillInfo.LevelUpData(new SkillInfo.MeleeInfo.LevelUpData(2.5f), null));

        characterInfo_LevelUpData = new CharacterInfo.LevelUpData(new DamageableInfo.LevelUpData(12.5f), new ExperienceInfo.LevelUpData(0f, 7.25f), 7.25f, skillInfo_LevelUpDatas);

        characterDatas.Add(CharacterCode.CrazyRabbit, new CharacterData(new MovementData(3f, 3f), new DamageableData(25f, 0f), new ExperienceData(0f, 12.5f), 12.5f, skillDatas, null, characterInfo_LevelUpData));

        #endregion

        #region GiantSpider

        skillDatas = new List<SkillData>();

        statusEffectDatas = new List<StatusEffectData>();

        statusEffectDatas.Add(new StatusEffectData(StatusEffectCode.MovementSpeedDown, 0.50f, 3f));

        skillData_RangedData = new SkillData.RangedData(ProjectileCode.SpiderWeb, 1f, 0f, new ProjectileData(25f, 1f, 0f, statusEffectDatas, null));

        skillDatas.Add(new SkillData(10f, 5f, 0, 0f, 0f, 0f, 0, AnimatorManager.FrameCountToSeconds(13), 1f, 0f, null, null, skillData_RangedData));

        skillData_RangedData = new SkillData.RangedData(ProjectileCode.PoisonBullet, 1f, 0f, new ProjectileData(25f, 1f, 5f, null, null));

        skillDatas.Add(new SkillData(10f, 1f, 0, 0f, 0f, 0f, 0, AnimatorManager.FrameCountToSeconds(13), 1f, 0f, null, null, skillData_RangedData));

        skillInfo_LevelUpDatas = new List<SkillInfo.LevelUpData>();

        skillInfo_LevelUpDatas.Add(new SkillInfo.LevelUpData(null, new SkillInfo.RangedInfo.LevelUpData(new ProjectileInfo.LevelUpData(0f, null))));

        skillInfo_LevelUpDatas.Add(new SkillInfo.LevelUpData(null, new SkillInfo.RangedInfo.LevelUpData(new ProjectileInfo.LevelUpData(1f, null))));

        characterInfo_LevelUpData = new CharacterInfo.LevelUpData(new DamageableInfo.LevelUpData(25f), new ExperienceInfo.LevelUpData(0f, 12.5f), 12.5f, skillInfo_LevelUpDatas);

        characterDatas.Add(CharacterCode.GiantSpider, new CharacterData(new MovementData(3f, 3f), new DamageableData(50f, 0f), new ExperienceData(0f, 25f), 25f, skillDatas, null, characterInfo_LevelUpData));

        #endregion

        #region Minotauros

        skillDatas = new List<SkillData>();

        skillDatas.Add(new SkillData(2f, 1f, 0, 0f, 0f, 0f, 0, AnimatorManager.FrameCountToSeconds(70), 0f, 0f, null, new SkillData.MeleeData(10f, null), null));

        skillDatas.Add(new SkillData(2f, 1f, 0, 0f, 0f, 0f, 1, AnimatorManager.FrameCountToSeconds(70), 0f, 0f, null, new SkillData.MeleeData(10f, null), null));

        statusEffectDatas = new List<StatusEffectData>();

        statusEffectDatas.Add(new StatusEffectData(StatusEffectCode.MovementSpeedUp, 2f, 5f));

        skillDatas.Add(new SkillData(20f, 10f, 0, AnimatorManager.FrameCountToSeconds(70), 0f, 0f, 3, AnimatorManager.FrameCountToSeconds(20), 0f, 5f, statusEffectDatas, new SkillData.MeleeData(10f, null), null));

        skillData_RangedData = new SkillData.RangedData(ProjectileCode.MinotaurossAxe, 1f, 0f, new ProjectileData(35f, 5f, 10f, null, null));

        skillDatas.Add(new SkillData(20f, 5f, 0, 0f, 0f, 0f, 2, AnimatorManager.FrameCountToSeconds(30), 0f, 0f, null, null, skillData_RangedData));

        skillInfo_LevelUpDatas = new List<SkillInfo.LevelUpData>();

        skillInfo_LevelUpDatas.Add(new SkillInfo.LevelUpData(new SkillInfo.MeleeInfo.LevelUpData(5f), null));

        skillInfo_LevelUpDatas.Add(new SkillInfo.LevelUpData(new SkillInfo.MeleeInfo.LevelUpData(5f), null));

        skillInfo_LevelUpDatas.Add(new SkillInfo.LevelUpData(new SkillInfo.MeleeInfo.LevelUpData(5f), null));

        skillInfo_LevelUpDatas.Add(new SkillInfo.LevelUpData(null, new SkillInfo.RangedInfo.LevelUpData(new ProjectileInfo.LevelUpData(2f, null))));

        characterInfo_LevelUpData = new CharacterInfo.LevelUpData(new DamageableInfo.LevelUpData(250f), new ExperienceInfo.LevelUpData(0f, 75f), 75f, skillInfo_LevelUpDatas);

        characterDatas.Add(CharacterCode.Minotauros, new CharacterData(new MovementData(3f, 3f), new DamageableData(500f, 0f), new ExperienceData(0f, 250f), 250f, skillDatas, null, characterInfo_LevelUpData));

        #endregion

        #region Player

        itemDatas = new Dictionary<ItemType, List<ItemData>>()
        {
            {
                ItemType.Ammo, new List<ItemData>()
                {
                    this.itemDatas[ItemCode.PistolAmmo],

                    this.itemDatas[ItemCode.ShotgunAmmo],

                    this.itemDatas[ItemCode.SubmachineGunAmmo],
                }
            },

            {
                ItemType.Consumable, new List<ItemData>()
                {
                    this.itemDatas[ItemCode.Grenade],

                    this.itemDatas[ItemCode.Medikit]
                }
            },

            {
                ItemType.Weapon, new List<ItemData>()
                {
                    this.itemDatas[ItemCode.Pistol],

                    this.itemDatas[ItemCode.Shotgun],

                    this.itemDatas[ItemCode.SubmachineGun],
                }
            },
        };

        counts = new Dictionary<ItemType, List<int>>()
        {
            {
                ItemType.Ammo, new List<int>()
                {
                    4000,

                    1200,

                    12000,
                }
            },

            {
                ItemType.Consumable, new List<int>()
                {
                    500,

                    500,
                }
            },

            {
                ItemType.Weapon, new List<int>()
                {
                    10,

                    3,

                    30,
                }
            },
        };

        characterInfo_LevelUpData = new CharacterInfo.LevelUpData(new DamageableInfo.LevelUpData(20f), new ExperienceInfo.LevelUpData(20f, 0f), 0f, null);

        characterDatas.Add(CharacterCode.Player, new CharacterData(new MovementData(3f, 4.5f, 1, 5f), new DamageableData(100f, 0f), new ExperienceData(100f, 0f), 0f, null, new InventoryData(itemDatas, counts), characterInfo_LevelUpData));

        #endregion
    }

    private void InitializeShopData()
    {
        Dictionary<ItemType, List<ShopItemData>> shopItemInfosDictionary = new Dictionary<ItemType, List<ShopItemData>>();

        List<ShopItemData> shopItemDatas;

        #region Ammo

        shopItemDatas = new List<ShopItemData>()
        {
            new ShopItemData(ItemCode.ShotgunAmmo, 3, -1),

            new ShopItemData(ItemCode.SubmachineGunAmmo, 30, -1),
        };

        shopItemInfosDictionary.Add(ItemType.Ammo, shopItemDatas);

        #endregion

        #region Consumable

        shopItemDatas = new List<ShopItemData>()
        {
            new ShopItemData(ItemCode.Grenade, 1, -1),

            new ShopItemData(ItemCode.Medikit, 1, -1),
        };

        shopItemInfosDictionary.Add(ItemType.Consumable, shopItemDatas);

        #endregion

        #region Weapon

        shopItemDatas = new List<ShopItemData>()
        {
            new ShopItemData(ItemCode.ShotgunAmmo, 1, 1),

            new ShopItemData(ItemCode.SubmachineGunAmmo, 1, 1),
        };

        shopItemInfosDictionary.Add(ItemType.Weapon, shopItemDatas);

        #endregion

        shopData = new ShopData(shopItemInfosDictionary);
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
        new WaveData.EnemySpawnData.SpawnEnemyData(CharacterCode.GiantSpider, 1, 4, 0),
        };

        waveDatas.Add(new WaveData(180f, new WaveData.EnemySpawnData(2, spawnEnemyData), null));

        #endregion

        #region Wave 2

        spawnEnemyData = new List<WaveData.EnemySpawnData.SpawnEnemyData>()
        {
            new WaveData.EnemySpawnData.SpawnEnemyData(CharacterCode.GiantSpider, 2, 8, 0),
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
            new WaveData.EnemySpawnData.SpawnEnemyData(CharacterCode.Minotauros, 3, 1, 0),
        };

        waveDatas.Add(new WaveData(180f, new WaveData.EnemySpawnData(1, spawnEnemyData), null));

        #endregion

        roundDatas.Add(new RoundData(180f, waveDatas));

        #endregion

        stageDatas.Add(SceneCode.City, new StageData(roundDatas));

        #endregion

        #region Desert

        roundDatas = new List<RoundData>();

        #region Round 1

        waveDatas = new List<WaveData>();

        #region Wave 1

        spawnEnemyData = new List<WaveData.EnemySpawnData.SpawnEnemyData>()
        {
            new WaveData.EnemySpawnData.SpawnEnemyData(CharacterCode.CrazyRabbit, 2, 4, 0),

            new WaveData.EnemySpawnData.SpawnEnemyData(CharacterCode.GiantSpider, 2, 1, 0),
        };

        waveDatas.Add(new WaveData(180f, new WaveData.EnemySpawnData(8, spawnEnemyData), null));

        #endregion

        #region Wave 2

        spawnEnemyData = new List<WaveData.EnemySpawnData.SpawnEnemyData>()
        {
            new WaveData.EnemySpawnData.SpawnEnemyData(CharacterCode.CrazyRabbit, 2, 8, 0),

            new WaveData.EnemySpawnData.SpawnEnemyData(CharacterCode.GiantSpider, 2, 2, 0),
        };

        waveDatas.Add(new WaveData(180f, new WaveData.EnemySpawnData(10, spawnEnemyData), null));

        #endregion

        roundDatas.Add(new RoundData(180f, waveDatas));

        #endregion

        #region Round 2

        waveDatas = new List<WaveData>();

        #region Wave 1

        spawnEnemyData = new List<WaveData.EnemySpawnData.SpawnEnemyData>()
        {
            new WaveData.EnemySpawnData.SpawnEnemyData(CharacterCode.Minotauros, 2, 1, 0),
        };

        waveDatas.Add(new WaveData(180f, new WaveData.EnemySpawnData(1, spawnEnemyData), null));

        #endregion

        roundDatas.Add(new RoundData(180f, waveDatas));

        #endregion

        stageDatas.Add(SceneCode.Desert, new StageData(roundDatas));

        #endregion
    }
}