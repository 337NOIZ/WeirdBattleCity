
using System.Collections.Generic;

public sealed class ItemData
{
    public ItemType itemType { get; private set; }

    public ItemCode itemCode { get; private set; }

    public int stackCount_Max { get; private set; }

    public int ammoCount_Max { get; private set; }

    public float drawingTime { get; private set; }

    public float reloadingTime { get; private set; }

    public bool autoAttack { get; private set; }

    public List<SkillData> skillDatas { get; private set; } = null;

    public ItemData(ItemType itemType, ItemCode itemCode, int stackCount_Max, int ammoCount_Max, float drawingTime, float reloadingTime, bool autoAttack, List<SkillData> skillDatas)
    {
        this.itemType = itemType;

        this.itemCode = itemCode;

        this.stackCount_Max = stackCount_Max;

        this.ammoCount_Max = ammoCount_Max;

        this.drawingTime = drawingTime;

        this.reloadingTime = reloadingTime;

        this.autoAttack = autoAttack;

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

        drawingTime = itemData.drawingTime;

        reloadingTime = itemData.reloadingTime;

        autoAttack = itemData.autoAttack;

        if (itemData.skillDatas != null)
        {
            skillDatas = itemData.skillDatas.ConvertAll(skillData => new SkillData(skillData));
        }
    }
}