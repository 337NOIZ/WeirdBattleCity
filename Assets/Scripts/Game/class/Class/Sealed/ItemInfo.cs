
using System.Collections.Generic;

public sealed class ItemInfo
{
    public ItemType itemType { get; private set; }

    public ItemCode itemCode { get; private set; }

    public float stackCount_Max
    {
        get => stackCount_Max_Calculated;

        set
        {
            stackCount_Max_Origin = value;

            stackCount_Max_Calculated = stackCount_Max_Origin * stackCount_Max_Multiple_Origin;
        }
    }

    private float stackCount_Max_Origin;

    private float stackCount_Max_Calculated;

    public float stackCount_Max_Multiple
    {
        get => stackCount_Max_Multiple_Origin;

        set
        {
            stackCount_Max_Multiple_Origin = value;

            stackCount_Max_Calculated = stackCount_Max_Origin * stackCount_Max_Multiple_Origin;
        }
    }

    private float stackCount_Max_Multiple_Origin;

    public float ammoCount_Max
    {
        get => ammoCount_Max_Calculated;

        set
        {
            ammoCount_Max_Origin = value;

            ammoCount_Max_Calculated = ammoCount_Max_Origin * ammoCount_Max_Multiple_Origin;
        }
    }

    private float ammoCount_Max_Origin;

    private float ammoCount_Max_Calculated;

    public float ammoCount_Max_Multiple
    {
        get => ammoCount_Max_Multiple_Origin;

        set
        {
            ammoCount_Max_Multiple_Origin = value;

            ammoCount_Max_Calculated = ammoCount_Max_Origin * ammoCount_Max_Multiple_Origin;
        }
    }

    private float ammoCount_Max_Multiple_Origin;

    public float drawingMotionTime
    {
        get => drawingMotionTime_Calculated;

        set
        {
            drawingMotionTime_Origin = value;

            drawingMotionTime_Calculated = drawingMotionTime_Origin / drawingMotionSpeed_Origin;
        }
    }

    private float drawingMotionTime_Origin;

    private float drawingMotionTime_Calculated;

    public float drawingMotionSpeed
    {
        get => drawingMotionSpeed_Origin;

        set
        {
            drawingMotionSpeed_Origin = value;

            drawingMotionTime_Calculated = drawingMotionTime_Origin / drawingMotionSpeed_Origin;
        }
    }

    private float drawingMotionSpeed_Origin;

    public float reloadingMotionTime
    {
        get => reloadingMotionTime_Calculated;

        set
        {
            reloadingMotionTime_Origin = value;

            reloadingMotionTime_Calculated = reloadingMotionTime_Origin / reloadingMotionSpeed_Origin;
        }
    }

    private float reloadingMotionTime_Origin;

    private float reloadingMotionTime_Calculated;

    public float reloadingMotionSpeed
    {
        get => reloadingMotionSpeed_Origin;

        set
        {
            reloadingMotionSpeed_Origin = value;

            reloadingMotionTime_Calculated = reloadingMotionTime_Origin / reloadingMotionSpeed_Origin;
        }
    }

    private float reloadingMotionSpeed_Origin;

    public bool autoSkill { get; set; }

    public List<SkillInfo> skillInfos { get; private set; } = null;

    public float stackCount { get; set; }

    public float ammoCount { get; set; }

    public ItemInfo(ItemData itemData, float stackCount)
    {
        itemType = itemData.itemType;

        itemCode = itemData.itemCode;

        stackCount_Max_Origin = itemData.stackCount_Max;

        stackCount_Max_Calculated = itemData.stackCount_Max;

        stackCount_Max_Multiple_Origin = 1f;

        ammoCount_Max_Origin = itemData.ammoCount_Max;

        ammoCount_Max_Calculated = itemData.ammoCount_Max;

        ammoCount_Max_Multiple_Origin = 1f;

        drawingMotionTime_Origin = itemData.drawingMotionTime;

        drawingMotionTime_Calculated = itemData.drawingMotionTime;

        drawingMotionSpeed_Origin = 1f;

        reloadingMotionTime_Origin = itemData.reloadingMotionTime;

        reloadingMotionTime_Calculated = itemData.reloadingMotionTime;

        reloadingMotionSpeed_Origin = 1f;

        autoSkill = itemData.autoSkill;

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

        Initialize(stackCount);
    }

    public ItemInfo(ItemInfo itemInfo)
    {
        itemType = itemInfo.itemType;

        itemCode = itemInfo.itemCode;

        stackCount_Max_Origin = itemInfo.stackCount_Max_Origin;

        stackCount_Max_Calculated = itemInfo.stackCount_Max_Calculated;

        stackCount_Max_Multiple_Origin = itemInfo.stackCount_Max_Multiple_Origin;

        ammoCount_Max_Origin = itemInfo.ammoCount_Max_Origin;

        ammoCount_Max_Calculated = itemInfo.ammoCount_Max_Calculated;

        ammoCount_Max_Multiple_Origin = itemInfo.ammoCount_Max_Multiple_Origin;

        drawingMotionTime_Origin = itemInfo.drawingMotionTime_Origin;

        drawingMotionTime_Calculated = itemInfo.drawingMotionTime_Calculated;

        drawingMotionSpeed_Origin = itemInfo.drawingMotionSpeed_Origin;

        reloadingMotionTime_Origin = itemInfo.reloadingMotionTime_Origin;

        reloadingMotionTime_Calculated = itemInfo.reloadingMotionTime_Calculated;

        reloadingMotionSpeed_Origin = itemInfo.reloadingMotionSpeed_Origin;

        autoSkill = itemInfo.autoSkill;

        if (itemInfo.skillInfos != null)
        {
            skillInfos = itemInfo.skillInfos.ConvertAll(skillInfo => new SkillInfo(skillInfo));
        }

        stackCount = itemInfo.stackCount;

        ammoCount = itemInfo.ammoCount;
    }

    public void Initialize(float stackCount)
    {
        this.stackCount = stackCount;

        ammoCount = ammoCount_Max;
    }
}