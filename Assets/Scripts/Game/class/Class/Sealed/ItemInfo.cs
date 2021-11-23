
using System.Collections.Generic;

public sealed class ItemInfo
{
    public ItemType itemType { get; private set; }

    public ItemCode itemCode { get; private set; }

    public int stackCount_Max { get; private set; }

    public int ammoCount_Max { get; private set; }

    public float drawingMotionTime { get; private set; }

    public float reloadingMotionTime { get; private set; }

    public bool autoAttack { get; set; }

    public float stackCount_Max_Multiple { get; set; } = 1f;

    public float ammoCount_Max_Multiple { get; set; } = 1f;

    public float drawingMotionSpeed { get; set; } = 1f;

    public float reloadingMotionSpeed { get; set; } = 1f;

    public List<SkillInfo> skillInfos { get; private set; } = null;

    public int stackCount { get; set; } = 0;

    public int ammoCount { get; set; } = 0;

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

                    if (skillInfos != null)
                    {
                        int count = skillInfos.Count;

                        for (int index = 0; index < count; ++index)
                        {
                            skillInfos[index].level = _level;
                        }
                    }
                }
            }
        }

        public List<SkillInfo.LevelUpData> skillInfos { get; private set; } = null;

        public LevelUpData(List<SkillInfo.LevelUpData> skillInfos)
        {
            if (skillInfos != null)
            {
                this.skillInfos = new List<SkillInfo.LevelUpData>(skillInfos);
            }
        }

        public LevelUpData(LevelUpData characterInfo_LevelUpData)
        {
            _level = characterInfo_LevelUpData._level;

            if (characterInfo_LevelUpData.skillInfos != null)
            {
                skillInfos = new List<SkillInfo.LevelUpData>(characterInfo_LevelUpData.skillInfos);
            }
        }
    }

    public ItemInfo(ItemData itemData)
    {
        itemType = itemData.itemType;

        itemCode = itemData.itemCode;

        stackCount_Max = itemData.stackCount_Max;

        ammoCount_Max = itemData.ammoCount_Max;

        drawingMotionTime = itemData.drawingTime;

        reloadingMotionTime = itemData.reloadingTime;

        autoAttack = itemData.autoAttack;

        var skillDatas = itemData.skillDatas;

        if (skillDatas != null)
        {
            skillInfos = new List<SkillInfo>();

            int count = skillDatas.Count;

            for(int index = 0; index < count; ++index)
            {
                skillInfos.Add(new SkillInfo(skillDatas[index]));
            }
        }
    }

    public ItemInfo(ItemInfo itemInfo)
    {
        itemType = itemInfo.itemType;

        itemCode = itemInfo.itemCode;

        stackCount_Max_Multiple = itemInfo.stackCount_Max_Multiple;

        ammoCount_Max_Multiple = itemInfo.ammoCount_Max_Multiple;

        reloadingMotionSpeed = itemInfo.reloadingMotionSpeed;

        if (itemInfo.skillInfos != null)
        {
            skillInfos = itemInfo.skillInfos.ConvertAll(skillInfo => new SkillInfo(skillInfo));
        }

        stackCount = itemInfo.stackCount;

        ammoCount = itemInfo.ammoCount;
    }

    public void Initialize(int count)
    {
        if (itemType == ItemType.weapon)
        {
            stackCount = 1;

            ammoCount = count;
        }

        else
        {
            stackCount = count;

            ammoCount = 0;
        }
    }

    public void LevelUp(LevelUpData levelUpData)
    {
        var skillInfos = levelUpData.skillInfos;

        if (skillInfos != null)
        {
            int count = skillInfos.Count;

            for (int index = 0; index < count; ++index)
            {
                this.skillInfos[index].LevelUp(skillInfos[index]);
            }
        }
    }
}