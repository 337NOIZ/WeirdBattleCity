
using System.Collections.Generic;

public sealed class ItemData
{
    public ItemType itemType { get; private set; }

    public ItemCode itemCode { get; private set; }

    public float stackCount_Max { get; private set; }

    public float ammoCount_Max { get; private set; }

    public float drawingMotionTime { get; private set; }

    public float reloadingMotionTime { get; private set; }

    public bool autoSkill { get; private set; }

    public List<SkillData> skillDatas { get; private set; } = null;

    public ItemData(ItemType itemType, ItemCode itemCode, float stackCount_Max, float ammoCount_Max, float drawingMotionTime, float reloadingMotionTime, bool autoSkill, List<SkillData> skillDatas)
    {
        this.itemType = itemType;

        this.itemCode = itemCode;

        this.stackCount_Max = stackCount_Max;

        this.ammoCount_Max = ammoCount_Max;

        this.drawingMotionTime = drawingMotionTime;

        this.reloadingMotionTime = reloadingMotionTime;

        this.autoSkill = autoSkill;

        if(skillDatas != null)
        {
            this.skillDatas = skillDatas.ConvertAll(skillData => new SkillData(skillData));
        }
    }

    public ItemData(ItemData itemData)
    {
        itemType = itemData.itemType;

        itemCode = itemData.itemCode;

        stackCount_Max = itemData.stackCount_Max;

        ammoCount_Max = itemData.ammoCount_Max;

        drawingMotionTime = itemData.drawingMotionTime;

        reloadingMotionTime = itemData.reloadingMotionTime;

        autoSkill = itemData.autoSkill;

        if (itemData.skillDatas != null)
        {
            skillDatas = itemData.skillDatas.ConvertAll(skillData => new SkillData(skillData));
        }
    }
}