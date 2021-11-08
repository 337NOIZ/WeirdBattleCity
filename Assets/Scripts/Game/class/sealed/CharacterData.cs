
using System.Collections.Generic;

public sealed class CharacterData
{
    public DamageableData damageableData { get; private set; } = null;

    public MovementData movementData { get; private set; } = null;

    public List<SkillData> skillDatas { get; private set; } = null;

    public CharacterInfo_LevelUpData characterInfo_LevelUpData { get; private set; } = null;

    public CharacterData(DamageableData damageableData, MovementData movementData, List<SkillData> skillDatas, CharacterInfo_LevelUpData characterInfo_LevelUpData)
    {
        if (damageableData != null)
        {
            this.damageableData = new DamageableData(damageableData);
        }

        if (movementData != null)
        {
            this.movementData = new MovementData(movementData);
        }

        if (skillDatas != null)
        {
            this.skillDatas = new List<SkillData>(skillDatas);
        }

        if(characterInfo_LevelUpData != null)
        {
            this.characterInfo_LevelUpData = new CharacterInfo_LevelUpData(characterInfo_LevelUpData);
        }
    }

    public CharacterData(CharacterData characterData)
    {
        if (characterData.damageableData != null)
        {
            damageableData = new DamageableData(characterData.damageableData);
        }

        if(characterData.movementData != null)
        {
            movementData = new MovementData(characterData.movementData);
        }

        if (characterData.skillDatas != null)
        {
            skillDatas = new List<SkillData>(characterData.skillDatas);
        }

        if (characterData.characterInfo_LevelUpData != null)
        {
            characterInfo_LevelUpData = new CharacterInfo_LevelUpData(characterData.characterInfo_LevelUpData);
        }
    }
}