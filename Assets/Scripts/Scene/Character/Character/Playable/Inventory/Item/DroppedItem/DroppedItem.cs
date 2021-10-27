
using UnityEngine;

public class DroppedItem : Item
{
    [Space]

    [SerializeField] private SpinAndFloat model = null;

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player.inventory.GetItem(itemInfo) == true)
        {
            model.gameObject.SetActive(false);

            model.StopFloating();

            model.StopSpining();

            transform.localPosition = Vector3.zero;

            itemInfo = null;
        }
    }

    public void Initialize(ItemInfo itemInfo)
    {
        itemInfo = new ItemInfo(itemInfo);

        transform.position = itemInfo.transformPosition;

        model.Spining(new Vector3(0f, 45f, 0f));

        model.Floating(new Vector3(0f, 0.25f, 0f), 2f);

        model.gameObject.SetActive(true);
    }
}