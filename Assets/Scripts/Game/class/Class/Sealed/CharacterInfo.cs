
using System.Collections.Generic;

public sealed class CharacterInfo
{
    public int level { get; private set; } = 1;

    public DamageableInfo damageableInfo { get; private set; } = null;

    public ExperienceInfo experienceInfo { get; private set; } = null;

    public MovementInfo movementInfo { get; private set; } = null;

    public List<SkillInfo> skillInfos { get; private set; } = null;

    public TransformInfo transformInfo { get; private set; } = null;

    public sealed class LevelUpData
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

                    if (movementInfo_LevelUpData != null)
                    {
                        movementInfo_LevelUpData.level = _level;
                    }

                    if (skillInfo_LevelUpDatas != null)
                    {
                        int count = skillInfo_LevelUpDatas.Count;

                        for (int index = 0; index < count; ++index)
                        {
                            skillInfo_LevelUpDatas[index].level = _level;
                        }
                    }
                }
            }
        }

        public DamageableInfo.LevelUpData damageableInfo_LevelUpData { get; private set; } = null;

        public ExperienceInfo.LevelUpData experienceInfo_LevelUpData { get; private set; } = null;

        public MovementInfo.LevelUpData movementInfo_LevelUpData { get; private set; } = null;

        public List<SkillInfo.LevelUpData> skillInfo_LevelUpDatas { get; private set; } = null;

        public LevelUpData(DamageableInfo.LevelUpData damageableInfo_LevelUpData, ExperienceInfo.LevelUpData experienceInfo_LevelUpData, MovementInfo.LevelUpData movementInfo_LevelUpData, List<SkillInfo.LevelUpData> skillInfo_LevelUpDatas)
        {
            if (damageableInfo_LevelUpData != null)
            {
                this.damageableInfo_LevelUpData = new DamageableInfo.LevelUpData(damageableInfo_LevelUpData);
            }

            if (experienceInfo_LevelUpData != null)
            {
                this.experienceInfo_LevelUpData = new ExperienceInfo.LevelUpData(experienceInfo_LevelUpData);
            }

            if (movementInfo_LevelUpData != null)
            {
                this.movementInfo_LevelUpData = new MovementInfo.LevelUpData(movementInfo_LevelUpData);
            }

            if (skillInfo_LevelUpDatas != null)
            {
                this.skillInfo_LevelUpDatas = new List<SkillInfo.LevelUpData>(skillInfo_LevelUpDatas);
            }
        }

        public LevelUpData(LevelUpData characterInfo_LevelUpData)
        {
            _level = characterInfo_LevelUpData._level;

            if (characterInfo_LevelUpData.damageableInfo_LevelUpData != null)
            {
                damageableInfo_LevelUpData = new DamageableInfo.LevelUpData(characterInfo_LevelUpData.damageableInfo_LevelUpData);
            }

            if (characterInfo_LevelUpData.experienceInfo_LevelUpData != null)
            {
                experienceInfo_LevelUpData = new ExperienceInfo.LevelUpData(characterInfo_LevelUpData.experienceInfo_LevelUpData);
            }

            if (characterInfo_LevelUpData.movementInfo_LevelUpData != null)
            {
                movementInfo_LevelUpData = new MovementInfo.LevelUpData(characterInfo_LevelUpData.movementInfo_LevelUpData);
            }

            if (characterInfo_LevelUpData.skillInfo_LevelUpDatas != null)
            {
                skillInfo_LevelUpDatas = new List<SkillInfo.LevelUpData>(characterInfo_LevelUpData.skillInfo_LevelUpDatas);
            }
        }
    }

    public CharacterInfo(CharacterData characterData, TransformInfo transformInfo)
    {
        if (characterData.damageableData != null)
        {
            damageableInfo = new DamageableInfo(characterData.damageableData);
        }

        if(characterData.experienceData != null)
        {
            experienceInfo = new ExperienceInfo(characterData.experienceData);
        }

        if(characterData.movementData != null)
        {
            movementInfo = new MovementInfo(characterData.movementData);
        }

        var skillDatas = characterData.skillDatas;

        if (skillDatas != null)
        {
            skillInfos = new List<SkillInfo>();

            int count = skillDatas.Count;

            for(int index = 0; index < count; ++index)
            {
                skillInfos.Add(new SkillInfo(skillDatas[index]));
            }
        }

        if (transformInfo != null)
        {
            this.transformInfo = new TransformInfo(transformInfo);
        }
    }

    public CharacterInfo(CharacterInfo characterInfo)
    {
        level = characterInfo.level;

        if (characterInfo.damageableInfo != null)
        {
            damageableInfo = new DamageableInfo(characterInfo.damageableInfo);
        }

        if(characterInfo.experienceInfo != null)
        {
            experienceInfo = new ExperienceInfo(characterInfo.experienceInfo);
        }

        if(characterInfo.movementInfo != null)
        {
            movementInfo = new MovementInfo(characterInfo.movementInfo);
        }

        if(characterInfo.skillInfos != null)
        {
            skillInfos = characterInfo.skillInfos.ConvertAll(skillInfo => new SkillInfo(skillInfo));
        }

        if (characterInfo.transformInfo != null)
        {
            transformInfo = new TransformInfo(characterInfo.transformInfo);
        }
    }

    public void Initialize()
    {
        if (damageableInfo != null)
        {
            damageableInfo.Initialize();
        }

        if(experienceInfo != null)
        {
            experienceInfo.Initialize();
        }

        if (movementInfo != null)
        {
            movementInfo.Initialize();
        }

        if (skillInfos != null)
        {
            int count = skillInfos.Count;

            for (int index = 0; index < count; ++index)
            {
                skillInfos[index].Initialize();
            }
        }
    }

    public void LevelUp(LevelUpData levelUpData)
    {
        level += levelUpData.level;

        if (levelUpData.damageableInfo_LevelUpData != null)
        {
            damageableInfo.LevelUp(levelUpData.damageableInfo_LevelUpData);
        }

        if (levelUpData.experienceInfo_LevelUpData != null)
        {
            experienceInfo.LevelUp(levelUpData.experienceInfo_LevelUpData);
        }

        if (levelUpData.movementInfo_LevelUpData != null)
        {
            movementInfo.LevelUp(levelUpData.movementInfo_LevelUpData);
        }

        var skillInfo_LevelUpDatas = levelUpData.skillInfo_LevelUpDatas;

        if (skillInfo_LevelUpDatas != null)
        {
            int count = skillInfo_LevelUpDatas.Count;

            for(int index = 0; index < count; ++index)
            {
                skillInfos[index].LevelUp(skillInfo_LevelUpDatas[index]);
            }
        }
    }
}