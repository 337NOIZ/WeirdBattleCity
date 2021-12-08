
using UnityEngine;

public abstract class DroppedItem : Item
{
    [Space]

    [SerializeField] private SpinAndFloat model = null;

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player.GetItem(_itemInfo) == true)
        {
            Disable();
        }
    }

    public override void Initialize(float stackCount)
    {
        _itemInfo = new ItemInfo(GameMaster.instance.gameData.levelData.itemDatas[itemCode], stackCount);

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