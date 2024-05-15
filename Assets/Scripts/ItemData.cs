using System.Collections.Generic;

public sealed class ItemData
{
    public readonly ItemType itemType;

    public readonly ItemCode itemCode;

    public readonly int stackCount_Max;

    public readonly int ammoCount_Max;

    public readonly float drawingMotionTime_Origin;

    public readonly float drawingMotionTime;

    public readonly float drawingMotionSpeed;

    public readonly float reloadingMotionTime_Origin;

    public readonly float reloadingMotionTime;

    public readonly float reloadingMotionSpeed;

    public readonly List<SkillData> skillDatas;

    public readonly float price;

    public ItemData(ItemType itemType, ItemCode itemCode, int stackCount_Max, int ammoCount_Max, float drawingMotionTime_Origin, float drawingMotionTime, float reloadingMotionTime_Origin,  float reloadingMotionTime, List<SkillData> skillDatas, float price)
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

        this.price = price;
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

        price = itemData.price;
    }
}