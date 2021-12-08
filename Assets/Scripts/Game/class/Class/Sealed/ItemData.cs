
using System.Collections.Generic;

public sealed class ItemData
{
    public ItemType itemType { get; private set; }

    public ItemCode itemCode { get; private set; }

    public float stackCount_Max { get; private set; }

    public float ammoCount_Max { get; private set; }

    public float drawingMotionTime_Origin { get; private set; }

    public float drawingMotionTime { get; private set; }

    public float drawingMotionSpeed { get; private set; }

    public float reloadingMotionTime_Origin { get; private set; }

    public float reloadingMotionTime { get; private set; }

    public float reloadingMotionSpeed { get; private set; }

    public List<SkillData> skillDatas { get; private set; } = null;

    public ItemData(ItemType itemType, ItemCode itemCode, float stackCount_Max, float ammoCount_Max, float drawingMotionTime_Origin, float drawingMotionTime, float reloadingMotionTime_Origin,  float reloadingMotionTime, List<SkillData> skillDatas)
    {
        this.itemType = itemType;

        this.itemCode = itemCode;

        this.stackCount_Max = stackCount_Max;

        this.ammoCount_Max = ammoCount_Max;

        this.drawingMotionTime_Origin = drawingMotionTime_Origin;

        if(drawingMotionTime > 0f)
        {
            this.drawingMotionTime = drawingMotionTime;

            drawingMotionSpeed = drawingMotionTime_Origin / drawingMotionTime;
        }

        else
        {
            this.drawingMotionTime = drawingMotionTime_Origin;

            drawingMotionSpeed = 1f;
        }

        this.reloadingMotionTime_Origin = reloadingMotionTime_Origin;

        this.reloadingMotionTime = reloadingMotionTime;

        if(reloadingMotionTime > 0f)
        {
            this.reloadingMotionTime = reloadingMotionTime;

            reloadingMotionSpeed = reloadingMotionTime_Origin / reloadingMotionTime;
        }

        else
        {
            this.reloadingMotionTime = reloadingMotionTime_Origin;

            reloadingMotionSpeed = 1f;
        }

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

        drawingMotionSpeed = itemData.drawingMotionSpeed;

        reloadingMotionTime = itemData.reloadingMotionTime;

        reloadingMotionSpeed = itemData.reloadingMotionSpeed;

        if (itemData.skillDatas != null)
        {
            skillDatas = itemData.skillDatas.ConvertAll(skillData => new SkillData(skillData));
        }
    }
}