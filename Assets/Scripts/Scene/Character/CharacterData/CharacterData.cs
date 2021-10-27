
using System.Collections.Generic;

public class CharacterData
{
    public DamageableData damageableData { get; private set; }

    public MovementData movementData { get; private set; } = null;

    public List<SkillData> skillDatas { get; private set; } = null;

    public CharacterData(DamageableData damageableData)
    {
        this.damageableData = new DamageableData(damageableData);
    }
    public CharacterData(DamageableData damageableData, MovementData movementData)
    {
        this.damageableData = new DamageableData(damageableData);

        this.movementData = new MovementData(movementData);
    }
    public CharacterData(DamageableData damageableData, List<SkillData> skillDatas)
    {
        this.damageableData = new DamageableData(damageableData);

        this.skillDatas = new List<SkillData>(skillDatas);
    }
    public CharacterData(DamageableData damageableData, MovementData movementData, List<SkillData> skillDatas)
    {
        this.damageableData = new DamageableData(damageableData);

        this.movementData = new MovementData(movementData);

        this.skillDatas = new List<SkillData>(skillDatas);
    }
    public CharacterData(CharacterData characterData)
    {
        damageableData = new DamageableData(characterData.damageableData);

        if(characterData.movementData != null)
        {
            movementData = new MovementData(characterData.movementData);
        }
        if (characterData.skillDatas != null)
        {
            skillDatas = new List<SkillData>(characterData.skillDatas);
        }
    }
}