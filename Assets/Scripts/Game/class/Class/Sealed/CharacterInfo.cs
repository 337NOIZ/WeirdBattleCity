
using System.Collections.Generic;

public sealed class CharacterInfo
{
    public int characterLevel { get; private set; }

    public MovementInfo movementInfo { get; private set; } = null;

    public DamageableInfo damageableInfo { get; private set; } = null;

    public ExperienceInfo experienceInfo { get; private set; } = null;

    public float moneyAmount { get; set; }

    public List<SkillInfo> skillInfos { get; private set; } = null;

    public InventoryInfo inventoryInfo { get; private set; }

    public Dictionary<StatusEffectCode, List<StatusEffectInfo>> statusEffectInfos { get; private set; } = null;

    public TransformInfo transformInfo { get; set; } = null;

    public CharacterInfo(CharacterData characterData)
    {
        characterLevel = 1;

        if (characterData.movementData != null)
        {
            movementInfo = new MovementInfo(characterData.movementData);
        }

        if (characterData.damageableData != null)
        {
            damageableInfo = new DamageableInfo(characterData.damageableData);
        }

        if(characterData.experienceData != null)
        {
            experienceInfo = new ExperienceInfo(characterData.experienceData);
        }

        moneyAmount = characterData.moneyAmount;

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

        if(characterData.inventoryData != null)
        {
            inventoryInfo = new InventoryInfo(characterData.inventoryData);
        }
    }

    public CharacterInfo(CharacterInfo characterInfo)
    {
        characterLevel = characterInfo.characterLevel;

        if (characterInfo.movementInfo != null)
        {
            movementInfo = new MovementInfo(characterInfo.movementInfo);
        }

        if (characterInfo.damageableInfo != null)
        {
            damageableInfo = new DamageableInfo(characterInfo.damageableInfo);
        }

        if(characterInfo.experienceInfo != null)
        {
            experienceInfo = new ExperienceInfo(characterInfo.experienceInfo);
        }

        moneyAmount = characterInfo.moneyAmount;

        if (characterInfo.skillInfos != null)
        {
            skillInfos = characterInfo.skillInfos.ConvertAll(skillInfo => new SkillInfo(skillInfo));
        }

        if (characterInfo.inventoryInfo != null)
        {
            inventoryInfo = new InventoryInfo(characterInfo.inventoryInfo);
        }

        if (characterInfo.transformInfo != null)
        {
            transformInfo = new TransformInfo(characterInfo.transformInfo);
        }
    }

    public void Initialize()
    {
        if (movementInfo != null)
        {
            movementInfo.Initialize();
        }

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

        statusEffectInfos = new Dictionary<StatusEffectCode, List<StatusEffectInfo>>();
    }

    public sealed class LevelUpData
    {
        private int _level_;

        public int level
        {
            get => _level_;

            set
            {
                _level_ = value;

                moneyAmount = _moneyAmount_ * _level_;

                if (damageableInfo_LevelUpData != null)
                {
                    damageableInfo_LevelUpData.level = _level_;
                }

                if(experienceInfo_LevelUpData != null)
                {
                    experienceInfo_LevelUpData.level = _level_;
                }

                if (skillInfo_LevelUpDatas != null)
                {
                    int count = skillInfo_LevelUpDatas.Count;

                    for (int index = 0; index < count; ++index)
                    {
                        skillInfo_LevelUpDatas[index].level = _level_;
                    }
                }
            }
        }

        public DamageableInfo.LevelUpData damageableInfo_LevelUpData { get; private set; } = null;

        public ExperienceInfo.LevelUpData experienceInfo_LevelUpData { get; private set; } = null;

        private float _moneyAmount_;

        public float moneyAmount { get; private set; }

        public List<SkillInfo.LevelUpData> skillInfo_LevelUpDatas { get; private set; } = null;

        public LevelUpData(DamageableInfo.LevelUpData damageableInfo_LevelUpData, ExperienceInfo.LevelUpData experienceInfo_LevelUpData, float moneyAmount, List<SkillInfo.LevelUpData> skillInfo_LevelUpDatas)
        {
            _level_ = 1;

            if (damageableInfo_LevelUpData != null)
            {
                this.damageableInfo_LevelUpData = new DamageableInfo.LevelUpData(damageableInfo_LevelUpData);
            }

            if (experienceInfo_LevelUpData != null)
            {
                this.experienceInfo_LevelUpData = new ExperienceInfo.LevelUpData(experienceInfo_LevelUpData);
            }

            _moneyAmount_ = moneyAmount;

            this.moneyAmount = moneyAmount;

            if (skillInfo_LevelUpDatas != null)
            {
                this.skillInfo_LevelUpDatas = new List<SkillInfo.LevelUpData>(skillInfo_LevelUpDatas);
            }
        }

        public LevelUpData(LevelUpData levelUpData)
        {
            _level_ = levelUpData._level_;

            if (levelUpData.damageableInfo_LevelUpData != null)
            {
                damageableInfo_LevelUpData = new DamageableInfo.LevelUpData(levelUpData.damageableInfo_LevelUpData);
            }

            if (levelUpData.experienceInfo_LevelUpData != null)
            {
                experienceInfo_LevelUpData = new ExperienceInfo.LevelUpData(levelUpData.experienceInfo_LevelUpData);
            }

            _moneyAmount_ = levelUpData._moneyAmount_;

            moneyAmount = levelUpData.moneyAmount;

            if (levelUpData.skillInfo_LevelUpDatas != null)
            {
                skillInfo_LevelUpDatas = new List<SkillInfo.LevelUpData>(levelUpData.skillInfo_LevelUpDatas);
            }
        }
    }

    public void LevelUp(LevelUpData levelUpData)
    {
        characterLevel += levelUpData.level;

        if (levelUpData.damageableInfo_LevelUpData != null)
        {
            damageableInfo.LevelUp(levelUpData.damageableInfo_LevelUpData);
        }

        if (levelUpData.experienceInfo_LevelUpData != null)
        {
            experienceInfo.LevelUp(levelUpData.experienceInfo_LevelUpData);
        }

        moneyAmount += levelUpData.moneyAmount;

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