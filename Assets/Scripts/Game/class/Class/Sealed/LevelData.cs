
using System.Collections.Generic;

public sealed class LevelData
{
    public Dictionary<ItemCode, ItemData> itemDatas { get; private set; } = new Dictionary<ItemCode, ItemData>();

    public Dictionary<CharacterCode, CharacterData> characterDatas { get; private set; } = new Dictionary<CharacterCode, CharacterData>();

    public Dictionary<SceneCode, StageData> stageDatas { get; private set; } = new Dictionary<SceneCode, StageData>();

    public LevelData()
    {
        InitializeItemDatas();

        InitializeCharacterDatas();

        InitializeStageDatas();
    }

    private void InitializeItemDatas()
    {
        ItemType itemType;

        ItemCode itemCode;

        List<SkillData> skillDatas;

        itemType = ItemType.ammo;

        itemCode = ItemCode.pistolAmmo;

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 1f, 50f, 0f, 0f, 0f, 0f, null));

        itemCode = ItemCode.shotgunAmmo;

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 15f, 0f, 0f, 0f, 0f, 0f, null));

        itemCode = ItemCode.submachineGunAmmo;

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 150f, 0f, 0f, 0f, 0f, 0f, null));

        itemType = ItemType.consumable;

        itemCode = ItemCode.grenade;

        skillDatas = new List<SkillData>()
        {
            new SkillData(0, 0f, 10f, 0, 0f, 0f, 0f, 0, AnimatorWizard.FrameCountToSeconds(30), 1f, 0f, null, new SkillData.RangedData(ProjectileCode.grenade, 1f, 0f, new ProjectileData(10f, 3f, 0, new DamageableData(1f, 0f), new ExplosionData(ParticleEffectCode.explosion, 5f, 100, 1f, null), null)), null),
        };

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 5f, 0f, 0f, 0f, 0f, 0f, skillDatas));

        itemCode = ItemCode.medikit;

        skillDatas = new List<SkillData>()
        {
            new SkillData(0, 0f, 0f, 0, 0f, 0f, 0f, 0, 0f, 0f, 0f, null, null, null),
        };

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 5f, 0f, 0f, 0f, 0f, 0f, skillDatas));

        itemType = ItemType.weapon;

        itemCode = ItemCode.pistol;

        skillDatas = new List<SkillData>()
        {
            new SkillData(0, 0f, 0f, 0, 0f, 0f, 0f, 0, AnimatorWizard.FrameCountToSeconds(10), 0.3f, 0f, null, new SkillData.RangedData(ProjectileCode.gunBullet, 1f, 0.5f, new ProjectileData(100f,  1f, 10, null, null, null)), null),
        };

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 1f, 10f, AnimatorWizard.FrameCountToSeconds(40), 0.5f, AnimatorWizard.FrameCountToSeconds(125), 2f, skillDatas));

        itemCode = ItemCode.shotgun;

        skillDatas = new List<SkillData>()
        {
            new SkillData(0, 0f, 0f, 0, 0f, 0f, 0f, 0, AnimatorWizard.FrameCountToSeconds(45), 1f, 0f, null, new SkillData.RangedData(ProjectileCode.gunBullet, 15f, 5f, new ProjectileData(100f, 0.5f, 10, null, null, null)), null),
        };

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 1f, 3f, AnimatorWizard.FrameCountToSeconds(40), 0.5f, AnimatorWizard.FrameCountToSeconds(125), 2.5f, skillDatas));

        itemCode = ItemCode.submachineGun;

        skillDatas = new List<SkillData>()
        {
            new SkillData(0, 0f, 0f, 0, 0f, 0f, 0f, 0, AnimatorWizard.FrameCountToSeconds(10), 0.1f, 0f, null, new SkillData.RangedData(ProjectileCode.gunBullet, 1f, 0.5f, new ProjectileData(100f, 1f, 10, null, null, null)), null),
        };

        itemDatas.Add(itemCode, new ItemData(itemType, itemCode, 1, 30, AnimatorWizard.FrameCountToSeconds(40), 0.5f, AnimatorWizard.FrameCountToSeconds(125), 2f, skillDatas));
    }

    private void InitializeCharacterDatas()
    {
        List<SkillData> skillDatas;

        List<StatusEffectData> statusEffectDatas;

        List<SkillInfo.LevelUpData> skillInfo_LevelUpDatas;

        List<StatusEffectInfo.LevelUpData> statusEffectInfo_LevelUpDatas;

        Dictionary<ItemType, List<ItemData>> itemDatas;

        Dictionary<ItemType, List<int>> counts;

        CharacterInfo.LevelUpData characterInfo_LevelUpData;

        #region Bird

        skillDatas = new List<SkillData>();

        skillDatas.Add(new SkillData(0, 0f, 0f, 0, 0f, 0f, 0f, 0, 0f, 0f, 0f, null, null, null));

        skillInfo_LevelUpDatas = new List<SkillInfo.LevelUpData>();

        skillInfo_LevelUpDatas.Add(new SkillInfo.LevelUpData(null, null, null));

        characterInfo_LevelUpData = new CharacterInfo.LevelUpData(new DamageableInfo.LevelUpData(0f), new ExperienceInfo.LevelUpData(0f, 0f), 0f, skillInfo_LevelUpDatas);

        characterDatas.Add(CharacterCode.bird, new CharacterData(new MovementData(1f, 1f), new DamageableData(100, 0f), new ExperienceData(0f, 0f), 0f, skillDatas, null, characterInfo_LevelUpData));

        #endregion

        #region Minotauros

        skillDatas = new List<SkillData>();

        skillDatas.Add(new SkillData(0, 20f, 10f, 0, AnimatorWizard.FrameCountToSeconds(70), 0f, 0f, 3, AnimatorWizard.FrameCountToSeconds(20), 0f, 5f, null, null, null));

        skillDatas.Add(new SkillData(1, 20f, 3f, 0, 0f, 0f, 0f, 2, AnimatorWizard.FrameCountToSeconds(30), 0f, 0f, null, null, null));

        skillDatas.Add(new SkillData(2, 2f, 1.5f, 0, 0f, 0f, 0f, 0, AnimatorWizard.FrameCountToSeconds(70), 0f, 0f, null, null, null));

        skillDatas.Add(new SkillData(2, 2f, 1.5f, 0, 0f, 0f, 0f, 1, AnimatorWizard.FrameCountToSeconds(70), 0f, 0f, null, null, null));

        skillInfo_LevelUpDatas = new List<SkillInfo.LevelUpData>();

        skillInfo_LevelUpDatas.Add(new SkillInfo.LevelUpData(null, null, null));

        characterInfo_LevelUpData = new CharacterInfo.LevelUpData(new DamageableInfo.LevelUpData(0f), new ExperienceInfo.LevelUpData(0f, 0f), 0f, skillInfo_LevelUpDatas);

        characterDatas.Add(CharacterCode.minotauros, new CharacterData(new MovementData(3f, 3f), new DamageableData(100f, 0f), new ExperienceData(0f, 0f), 0f, skillDatas, null, characterInfo_LevelUpData));

        #endregion

        #region Spider

        skillDatas = new List<SkillData>();

        statusEffectDatas = new List<StatusEffectData>()
        {
            new StatusEffectData(StatusEffectCode.slow, 0.25f, 3f),
        };

        skillDatas.Add(new SkillData(0, 10f, 5f, 0, 0f, 0f, 0f, 0, AnimatorWizard.FrameCountToSeconds(13), 1f, 0f, null, new SkillData.RangedData(ProjectileCode.spiderWebBullet, 0f, 0f, new ProjectileData(25f, 1f, 0f, null, null, statusEffectDatas)), null));

        skillDatas.Add(new SkillData(1, 10f, 1.5f, 0, 0f, 0f, 0f, 0, AnimatorWizard.FrameCountToSeconds(13), 1f, 0f, null, new SkillData.RangedData(ProjectileCode.poisonBullet, 0f, 0f, new ProjectileData(25f, 1f, 3f, null, null, null)), null));

        skillInfo_LevelUpDatas = new List<SkillInfo.LevelUpData>();

        statusEffectInfo_LevelUpDatas = new List<StatusEffectInfo.LevelUpData>()
        {
            new StatusEffectInfo.LevelUpData(0f, 0f),
        };

        skillInfo_LevelUpDatas.Add(new SkillInfo.LevelUpData(null, new SkillInfo.RangedInfo.LevelUpData(new ProjectileInfo.LevelUpData(0f, null, null, statusEffectInfo_LevelUpDatas)), null));

        skillInfo_LevelUpDatas.Add(new SkillInfo.LevelUpData(null, new SkillInfo.RangedInfo.LevelUpData(new ProjectileInfo.LevelUpData(1f, null, null, null)), null));

        characterInfo_LevelUpData = new CharacterInfo.LevelUpData(new DamageableInfo.LevelUpData(10f), new ExperienceInfo.LevelUpData(0f, 5f), 1f, skillInfo_LevelUpDatas);

        characterDatas.Add(CharacterCode.spider, new CharacterData(new MovementData(2f, 0f), new DamageableData(100f, 0f), new ExperienceData(0f, 15f), 10f, skillDatas, null, characterInfo_LevelUpData));

        #endregion

        #region Player

        itemDatas = new Dictionary<ItemType, List<ItemData>>()
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

        counts = new Dictionary<ItemType, List<int>>()
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

        characterInfo_LevelUpData = new CharacterInfo.LevelUpData(new DamageableInfo.LevelUpData(0f), new ExperienceInfo.LevelUpData(100f, 0f), 0f, null);

        characterDatas.Add(CharacterCode.player, new CharacterData(new MovementData(3f, 6f, 1, 5f), new DamageableData(100f, 0f), new ExperienceData(100f, 0f), 0f, null, new InventoryData(itemDatas, counts), characterInfo_LevelUpData));

        #endregion
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
        new WaveData.EnemySpawnData.SpawnEnemyData(CharacterCode.spider, 1, 4, 0),
        };

        waveDatas.Add(new WaveData(180f, new WaveData.EnemySpawnData(1, spawnEnemyData), null));

        #endregion

        #region Wave 2

        spawnEnemyData = new List<WaveData.EnemySpawnData.SpawnEnemyData>()
        {
            new WaveData.EnemySpawnData.SpawnEnemyData(CharacterCode.spider, 2, 4, 0),
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
            new WaveData.EnemySpawnData.SpawnEnemyData(CharacterCode.spider, 3, 4, 0),
        };

        waveDatas.Add(new WaveData(180f, new WaveData.EnemySpawnData(1, spawnEnemyData), null));

        #endregion

        #region Wave 2

        spawnEnemyData = new List<WaveData.EnemySpawnData.SpawnEnemyData>()
        {
            new WaveData.EnemySpawnData.SpawnEnemyData(CharacterCode.spider, 4, 4, 0),
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
           new WaveData.EnemySpawnData.SpawnEnemyData(CharacterCode.spider, 5, 4, 0),
        };

        waveDatas.Add(new WaveData(180f, new WaveData.EnemySpawnData(2, spawnEnemyData), null));

        #endregion

        #region Wave 2

        spawnEnemyData = new List<WaveData.EnemySpawnData.SpawnEnemyData>()
        {
            new WaveData.EnemySpawnData.SpawnEnemyData(CharacterCode.spider, 6, 4, 0),
        };

        waveDatas.Add(new WaveData(180f, new WaveData.EnemySpawnData(8, spawnEnemyData), null));

        #endregion

        #region Wave 3

        spawnEnemyData = new List<WaveData.EnemySpawnData.SpawnEnemyData>()
        {
            new WaveData.EnemySpawnData.SpawnEnemyData(CharacterCode.spider, 20, 1, 0),
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
            new WaveData.EnemySpawnData.SpawnEnemyData(CharacterCode.minotauros, 1, 1, 0),
        };

        waveDatas.Add(new WaveData(180f, new WaveData.EnemySpawnData(1, spawnEnemyData), null));

        #endregion

        roundDatas.Add(new RoundData(180f, waveDatas));

        #endregion

        stageDatas.Add(SceneCode.desert, new StageData(roundDatas));

        #endregion
    }
}