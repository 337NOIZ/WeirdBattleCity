using System.Collections.Generic;

public sealed class CharacterData
{
    public float moneyAmount { get; private set; }

    public DamageableData damageableData { get; private set; } = null;

    public ExperienceData experienceData { get; private set; } = null;

    public List<SkillData> skillDatas { get; private set; } = null;

    public MovementData movementData { get; private set; } = null;

    public InventoryData inventoryData { get; private set; }

    public CharacterInfo.LevelUpData levelUpData { get; private set; } = null;

    public CharacterData(MovementData movementData, DamageableData damageableData, ExperienceData experienceData, float moneyAmount, List<SkillData> skillDatas, InventoryData inventoryData, CharacterInfo.LevelUpData levelUpData)
    {
        if (movementData != null)
        {
            this.movementData = new MovementData(movementData);
        }

        if (damageableData != null)
        {
            this.damageableData = new DamageableData(damageableData);
        }

        if (experienceData != null)
        {
            this.experienceData = new ExperienceData(experienceData);
        }

        this.moneyAmount = moneyAmount;

        if (skillDatas != null)
        {
            this.skillDatas = new List<SkillData>(skillDatas);
        }

        if(inventoryData != null)
        {
            this.inventoryData = new InventoryData(inventoryData);
        }

        if (levelUpData != null)
        {
            this.levelUpData = new CharacterInfo.LevelUpData(levelUpData);
        }
    }

    public CharacterData(CharacterData characterData)
    {
        if (characterData.movementData != null)
        {
            movementData = new MovementData(characterData.movementData);
        }

        if (characterData.damageableData != null)
        {
            damageableData = new DamageableData(characterData.damageableData);
        }

        if (characterData.experienceData != null)
        {
            experienceData = new ExperienceData(characterData.experienceData);
        }

        moneyAmount = characterData.moneyAmount;

        if (characterData.skillDatas != null)
        {
            skillDatas = new List<SkillData>(characterData.skillDatas);
        }

        if (characterData.inventoryData != null)
        {
            inventoryData = new InventoryData(characterData.inventoryData);
        }

        if (characterData.levelUpData != null)
        {
            levelUpData = new CharacterInfo.LevelUpData(characterData.levelUpData);
        }
    }
}