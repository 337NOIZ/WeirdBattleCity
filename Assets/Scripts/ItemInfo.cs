using System.Collections.Generic;

public sealed class ItemInfo
{
    public ItemType itemType { get; private set; }

    public ItemCode itemCode { get; private set; }

    private int _stackCount_Max_Origin;

    private int _stackCount_Max;

    public int stackCount_Max
    {
        get => _stackCount_Max;

        set
        {
            _stackCount_Max_Origin = value;

            _stackCount_Max = _stackCount_Max_Origin * (int)_stackCount_Max_Multiple;
        }
    }

    private float _stackCount_Max_Multiple;

    public float stackCount_Max_Multiple
    {
        get => _stackCount_Max_Multiple;

        set
        {
            _stackCount_Max_Multiple = value;

            _stackCount_Max = _stackCount_Max_Origin * (int)_stackCount_Max_Multiple;
        }
    }

    private int ammoCount_Max_Origin;

    private int _ammoCount_Max;

    public int ammoCount_Max
    {
        get => _ammoCount_Max;

        set
        {
            ammoCount_Max_Origin = value;

            _ammoCount_Max = ammoCount_Max_Origin * (int)_ammoCount_Max_Multiple;
        }
    }

    private float _ammoCount_Max_Multiple;

    public float ammoCount_Max_Multiple
    {
        get => _ammoCount_Max_Multiple;

        set
        {
            _ammoCount_Max_Multiple = value;

            _ammoCount_Max = ammoCount_Max_Origin * (int)_ammoCount_Max_Multiple;
        }
    }

    public float drawingMotionTime;

    public float drawingMotionSpeed;

    public float reloadingMotionTime;

    public float reloadingMotionSpeed;

    public List<SkillInfo> skillInfos { get; private set; } = null;

    public int stackCount { get; set; }

    public int ammoCount { get; set; }

    public ItemInfo(ItemData itemData, int stackCount)
    {
        itemType = itemData.itemType;

        itemCode = itemData.itemCode;

        _stackCount_Max_Origin = itemData.stackCount_Max;

        _stackCount_Max = itemData.stackCount_Max;

        _stackCount_Max_Multiple = 1f;

        ammoCount_Max_Origin = itemData.ammoCount_Max;

        _ammoCount_Max = itemData.ammoCount_Max;

        _ammoCount_Max_Multiple = 1f;

        drawingMotionTime = itemData.drawingMotionTime;

        drawingMotionSpeed = itemData.drawingMotionSpeed;

        reloadingMotionTime = itemData.reloadingMotionTime;

        reloadingMotionSpeed = itemData.reloadingMotionSpeed;

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

        _stackCount_Max_Origin = itemInfo._stackCount_Max_Origin;

        _stackCount_Max = itemInfo._stackCount_Max;

        _stackCount_Max_Multiple = itemInfo._stackCount_Max_Multiple;

        ammoCount_Max_Origin = itemInfo.ammoCount_Max_Origin;

        _ammoCount_Max = itemInfo._ammoCount_Max;

        _ammoCount_Max_Multiple = itemInfo._ammoCount_Max_Multiple;

        drawingMotionTime = itemInfo.drawingMotionTime;

        drawingMotionSpeed = itemInfo.drawingMotionSpeed;

        reloadingMotionTime = itemInfo.reloadingMotionTime;

        reloadingMotionSpeed = itemInfo.reloadingMotionSpeed;

        if (itemInfo.skillInfos != null)
        {
            skillInfos = itemInfo.skillInfos.ConvertAll(skillInfo => new SkillInfo(skillInfo));
        }

        stackCount = itemInfo.stackCount;

        ammoCount = itemInfo.ammoCount;
    }

    public void Initialize(int stackCount)
    {
        this.stackCount = stackCount;

        ammoCount = ammoCount_Max;
    }
}