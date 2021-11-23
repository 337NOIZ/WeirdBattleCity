
using System.Collections.Generic;

public sealed class CharacterData
{
    public DamageableData damageableData { get; private set; } = null;

    public ExperienceData experienceData { get; private set; } = null;

    public MovementData movementData { get; private set; } = null;

    public List<SkillData> skillDatas { get; private set; } = null;

    public CharacterInfo.LevelUpData levelUpData { get; private set; } = null;

    public CharacterData(DamageableData damageableData, ExperienceData experienceData, MovementData movementData, List<SkillData> skillDatas, CharacterInfo.LevelUpData levelUpData)
    {
        if (damageableData != null)
        {
            this.damageableData = new DamageableData(damageableData);
        }

        if (experienceData != null)
        {
            this.experienceData = new ExperienceData(experienceData);
        }

        if (movementData != null)
        {
            this.movementData = new MovementData(movementData);
        }

        if (skillDatas != null)
        {
            this.skillDatas = new List<SkillData>(skillDatas);
        }

        if (levelUpData != null)
        {
            this.levelUpData = new CharacterInfo.LevelUpData(levelUpData);
        }
    }

    public CharacterData(CharacterData characterData)
    {
        if (characterData.damageableData != null)
        {
            damageableData = new DamageableData(characterData.damageableData);
        }

        if (characterData.experienceData != null)
        {
            experienceData = new ExperienceData(characterData.experienceData);
        }

        if (characterData.movementData != null)
        {
            movementData = new MovementData(characterData.movementData);
        }

        if (characterData.skillDatas != null)
        {
            skillDatas = new List<SkillData>(characterData.skillDatas);
        }

        if (characterData.levelUpData != null)
        {
            levelUpData = new CharacterInfo.LevelUpData(characterData.levelUpData);
        }
    }
}