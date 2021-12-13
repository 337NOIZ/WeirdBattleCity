
using UnityEngine;

public abstract class DroppedItem : Item
{
    [Space]

    [SerializeField] private SpinAndFloat model = null;

    private ItemData _itemData;

    private void OnCollisionEnter(Collision collision)
    {
        if (Player.instance.GetItem(_itemInfo) == true)
        {
            Disable();
        }
    }

    public void Awaken()
    {
        _itemData = GameMaster.instance.gameData.levelData.itemDatas[itemCode];
    }

    public void Initialize(int stackCount)
    {
        _itemInfo = new ItemInfo(_itemData, stackCount);

        model.Spining(new Vector3(0f, 45f, 0f));

        model.Floating(new Vector3(0f, 0.25f, 0f), 2f);
    }

    public void Disable()
    {
        model.StopFloating();

        model.StopSpining();

        gameObject.SetActive(false);

        ObjectPool.instance.Push(this);
    }
}