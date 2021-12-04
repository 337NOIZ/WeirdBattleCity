
using System.Collections.Generic;

public sealed class CharacterInfo
{
    public int characterLevel { get; private set; }

    public float moneyAmount { get; set; }

    public DamageableInfo damageableInfo { get; private set; } = null;

    public ExperienceInfo experienceInfo { get; private set; } = null;

    public List<SkillInfo> skillInfos { get; private set; } = null;

    public MovementInfo movementInfo { get; private set; } = null;

    public TransformInfo transformInfo { get; private set; } = null;

    public Dictionary<StatusEffectCode, List<StatusEffectInfo>> statusEffectInfos { get; private set; } = null;

    public CharacterInfo(CharacterData characterData, TransformInfo transformInfo)
    {
        characterLevel = 1;

        moneyAmount = characterData.moneyAmount;

        if (characterData.damageableData != null)
        {
            damageableInfo = new DamageableInfo(characterData.damageableData);
        }

        if(characterData.experienceData != null)
        {
            experienceInfo = new ExperienceInfo(characterData.experienceData);
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

        if (characterData.movementData != null)
        {
            movementInfo = new MovementInfo(characterData.movementData);
        }

        if (transformInfo != null)
        {
            this.transformInfo = new TransformInfo(transformInfo);
        }
    }

    public CharacterInfo(CharacterInfo characterInfo)
    {
        characterLevel = characterInfo.characterLevel;

        moneyAmount = characterInfo.moneyAmount;

        if (characterInfo.damageableInfo != null)
        {
            damageableInfo = new DamageableInfo(characterInfo.damageableInfo);
        }

        if(characterInfo.experienceInfo != null)
        {
            experienceInfo = new ExperienceInfo(characterInfo.experienceInfo);
        }

        if(characterInfo.skillInfos != null)
        {
            skillInfos = characterInfo.skillInfos.ConvertAll(skillInfo => new SkillInfo(skillInfo));
        }

        if (characterInfo.movementInfo != null)
        {
            movementInfo = new MovementInfo(characterInfo.movementInfo);
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

        if (skillInfos != null)
        {
            int count = skillInfos.Count;

            for (int index = 0; index < count; ++index)
            {
                skillInfos[index].Initialize();
            }
        }

        if (movementInfo != null)
        {
            movementInfo.Initialize();
        }

        statusEffectInfos = new Dictionary<StatusEffectCode, List<StatusEffectInfo>>();
    }

    public sealed class LevelUpData
    {
        public int level
        {
            get => level_Origin;

            set
            {
                level_Origin = value;

                moneyAmount = moneyAmount_Origin * level_Origin;

                if (damageableInfo_LevelUpData != null)
                {
                    damageableInfo_LevelUpData.level = level_Origin;
                }

                if(experienceInfo_LevelUpData != null)
                {
                    experienceInfo_LevelUpData.level = level_Origin;
                }

                if (skillInfo_LevelUpDatas != null)
                {
                    int count = skillInfo_LevelUpDatas.Count;

                    for (int index = 0; index < count; ++index)
                    {
                        skillInfo_LevelUpDatas[index].level = level_Origin;
                    }
                }
            }
        }

        private int level_Origin;

        public float moneyAmount { get; private set; }

        private float moneyAmount_Origin;

        public DamageableInfo.LevelUpData damageableInfo_LevelUpData { get; private set; } = null;

        public ExperienceInfo.LevelUpData experienceInfo_LevelUpData { get; private set; } = null;

        public List<SkillInfo.LevelUpData> skillInfo_LevelUpDatas { get; private set; } = null;

        public LevelUpData(float moneyAmount, DamageableInfo.LevelUpData damageableInfo_LevelUpData, ExperienceInfo.LevelUpData experienceInfo_LevelUpData, List<SkillInfo.LevelUpData> skillInfo_LevelUpDatas)
        {
            level_Origin = 1;

            this.moneyAmount = moneyAmount;

            moneyAmount_Origin = moneyAmount;

            if (damageableInfo_LevelUpData != null)
            {
                this.damageableInfo_LevelUpData = new DamageableInfo.LevelUpData(damageableInfo_LevelUpData);
            }

            if (experienceInfo_LevelUpData != null)
            {
                this.experienceInfo_LevelUpData = new ExperienceInfo.LevelUpData(experienceInfo_LevelUpData);
            }

            if (skillInfo_LevelUpDatas != null)
            {
                this.skillInfo_LevelUpDatas = new List<SkillInfo.LevelUpData>(skillInfo_LevelUpDatas);
            }
        }

        public LevelUpData(LevelUpData levelUpData)
        {
            level_Origin = levelUpData.level_Origin;

            moneyAmount = levelUpData.moneyAmount;

            moneyAmount_Origin = levelUpData.moneyAmount_Origin;

            if (levelUpData.damageableInfo_LevelUpData != null)
            {
                damageableInfo_LevelUpData = new DamageableInfo.LevelUpData(levelUpData.damageableInfo_LevelUpData);
            }

            if (levelUpData.experienceInfo_LevelUpData != null)
            {
                experienceInfo_LevelUpData = new ExperienceInfo.LevelUpData(levelUpData.experienceInfo_LevelUpData);
            }

            if (levelUpData.skillInfo_LevelUpDatas != null)
            {
                skillInfo_LevelUpDatas = new List<SkillInfo.LevelUpData>(levelUpData.skillInfo_LevelUpDatas);
            }
        }
    }

    public void LevelUp(LevelUpData levelUpData)
    {
        characterLevel += levelUpData.level;

        moneyAmount += levelUpData.moneyAmount;

        if (levelUpData.damageableInfo_LevelUpData != null)
        {
            damageableInfo.LevelUp(levelUpData.damageableInfo_LevelUpData);
        }

        if (levelUpData.experienceInfo_LevelUpData != null)
        {
            experienceInfo.LevelUp(levelUpData.experienceInfo_LevelUpData);
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