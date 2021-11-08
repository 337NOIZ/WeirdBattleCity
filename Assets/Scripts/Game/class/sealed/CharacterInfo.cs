
using System.Collections.Generic;

public sealed class CharacterInfo
{
    public int level { get; private set; } = 1;

    public DamageableInfo damageableInfo { get; private set; } = null;

    public TransformInfo transformInfo { get; private set; } = null;

    public MovementInfo movementInfo { get; private set; } = null;

    public List<SkillInfo> skillInfos { get; private set; } = null;

    public CharacterInfo(DamageableInfo damageableInfo, TransformInfo transformInfo, MovementInfo movementInfo, List<SkillInfo> skillInfos)
    {
        if (damageableInfo != null)
        {
            this.damageableInfo = new DamageableInfo(damageableInfo);
        }

        if(transformInfo != null)
        {
            this.transformInfo = new TransformInfo(transformInfo);
        }

        if(movementInfo != null)
        {
            this.movementInfo = new MovementInfo(movementInfo);
        }
        
        if(skillInfos != null)
        {
            this.skillInfos = new List<SkillInfo>(skillInfos);
        }
    }

    public CharacterInfo(CharacterInfo characterInfo)
    {
        if (characterInfo.damageableInfo != null)
        {
            damageableInfo = new DamageableInfo(characterInfo.damageableInfo);
        }

        if (characterInfo.transformInfo != null)
        {
            transformInfo = new TransformInfo(characterInfo.transformInfo);
        }

        if(characterInfo.movementInfo != null)
        {
            movementInfo = new MovementInfo(characterInfo.movementInfo);
        }

        if(characterInfo.skillInfos != null)
        {
            skillInfos = new List<SkillInfo>(characterInfo.skillInfos);
        }
    }

    public void Recycling()
    {
        if (damageableInfo != null)
        {
            damageableInfo.Recycling();
        }

        if (movementInfo != null)
        {
            movementInfo.Recycling();
        }

        if (skillInfos != null)
        {
            var count = skillInfos.Count;

            for (int index = 0; index < count; ++index)
            {
                skillInfos[index].Recycling();
            }
        }
    }

    public void LevelUp(CharacterInfo_LevelUpData characterInfo_LevelUpData)
    {
        level += characterInfo_LevelUpData.level;

        if (characterInfo_LevelUpData.damageableInfo_LevelUpData != null)
        {
            damageableInfo.LevelUp(characterInfo_LevelUpData.damageableInfo_LevelUpData);
        }

        if (characterInfo_LevelUpData.transformInfo_LevelUpData != null)
        {
            transformInfo.LevelUp(characterInfo_LevelUpData.transformInfo_LevelUpData);
        }

        if (characterInfo_LevelUpData.movementInfo_LevelUpData != null)
        {
            movementInfo.LevelUp(characterInfo_LevelUpData.movementInfo_LevelUpData);
        }

        var skillInfo_LevelUpDatas = characterInfo_LevelUpData.skillInfo_LevelUpDatas;

        if (skillInfo_LevelUpDatas != null)
        {
            var count = skillInfo_LevelUpDatas.Count;

            for(int index = 0; index < count; ++index)
            {
                skillInfos[index].LevelUp(skillInfo_LevelUpDatas[index]);
            }
        }
    }
}