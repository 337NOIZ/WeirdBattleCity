
using System.Collections.Generic;

public sealed class CharacterInfo_LevelUpData
{
    private int _level = 1;

    public int level
    {
        get
        {
            return _level;
        }

        set
        {
            if (value != _level)
            {
                _level = value;

                if (damageableInfo_LevelUpData != null)
                {
                    damageableInfo_LevelUpData.level = _level;
                }

                if (transformInfo_LevelUpData != null)
                {
                    transformInfo_LevelUpData.level = _level;
                }

                if (movementInfo_LevelUpData != null)
                {
                    movementInfo_LevelUpData.level = _level;
                }

                if (skillInfo_LevelUpDatas != null)
                {
                    var count = skillInfo_LevelUpDatas.Count;

                    for (int index = 0; index < count; ++index)
                    {
                        skillInfo_LevelUpDatas[index].level = _level;
                    }
                }
            }
        }
    }

    public DamageableInfo_LevelUpData damageableInfo_LevelUpData { get; private set; } = null;

    public TransformInfo_LevelUpData transformInfo_LevelUpData { get; private set; } = null;

    public MovementInfo_LevelUpData movementInfo_LevelUpData { get; private set; } = null;

    public List<SkillInfo_LevelUpData> skillInfo_LevelUpDatas { get; private set; } = null;

    public CharacterInfo_LevelUpData(DamageableInfo_LevelUpData damageableInfo_LevelUpData, TransformInfo_LevelUpData transformInfo_LevelUpData, MovementInfo_LevelUpData movementInfo_LevelUpData, List<SkillInfo_LevelUpData> skillInfo_LevelUpDatas)
    {
        if (damageableInfo_LevelUpData != null)
        {
            this.damageableInfo_LevelUpData = new DamageableInfo_LevelUpData(damageableInfo_LevelUpData);
        }

        if (transformInfo_LevelUpData != null)
        {
            this.transformInfo_LevelUpData = new TransformInfo_LevelUpData(transformInfo_LevelUpData);
        }

        if (movementInfo_LevelUpData != null)
        {
            this.movementInfo_LevelUpData = new MovementInfo_LevelUpData(movementInfo_LevelUpData);
        }

        if (skillInfo_LevelUpDatas != null)
        {
            this.skillInfo_LevelUpDatas = new List<SkillInfo_LevelUpData>(skillInfo_LevelUpDatas);
        }
    }

    public CharacterInfo_LevelUpData(CharacterInfo_LevelUpData characterInfo_LevelUpData)
    {
        _level = characterInfo_LevelUpData._level;

        if (characterInfo_LevelUpData.damageableInfo_LevelUpData != null)
        {
            damageableInfo_LevelUpData = new DamageableInfo_LevelUpData(characterInfo_LevelUpData.damageableInfo_LevelUpData);
        }

        if (characterInfo_LevelUpData.transformInfo_LevelUpData != null)
        {
            transformInfo_LevelUpData = new TransformInfo_LevelUpData(characterInfo_LevelUpData.transformInfo_LevelUpData);
        }

        if (characterInfo_LevelUpData.movementInfo_LevelUpData != null)
        {
            movementInfo_LevelUpData = new MovementInfo_LevelUpData(characterInfo_LevelUpData.movementInfo_LevelUpData);
        }

        if (characterInfo_LevelUpData.skillInfo_LevelUpDatas != null)
        {
            skillInfo_LevelUpDatas = new List<SkillInfo_LevelUpData>(characterInfo_LevelUpData.skillInfo_LevelUpDatas);
        }
    }
}