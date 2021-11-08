
using UnityEngine;

public abstract class DroppedItem : Item
{
    [Space]

    [SerializeField] private SpinAndFloat model = null;

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player.playerInventory.GetItem(itemInfo) == true)
        {
            RecoveryToPool();
        }
    }

    public override void Initialize(int itemLevel)
    {
        itemInfo = new ItemInfo(GameMaster.instance.gameData.levelData.itemInfos[itemCode]);

        model.Spining(new Vector3(0f, 45f, 0f));

        model.Floating(new Vector3(0f, 0.25f, 0f), 2f);
    }

    public void RecoveryToPool()
    {
        model.StopFloating();

        model.StopSpining();

        itemInfo = null;

        gameObject.SetActive(false);
    }
}