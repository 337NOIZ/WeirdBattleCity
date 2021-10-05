
using UnityEngine;

public class Drop : MonoBehaviour
{
    [Space]

    [SerializeField] private SpinAndFloat model = null;

    [Space]

    [SerializeField] private ItemData itemData;

    public ItemCode itemCode { get; protected set; }

    public virtual void Initialize() { }

    public void Spawn(ItemData itemData, Vector3 transformPosition)
    {
        this.itemData = new ItemData(itemData);

        transform.position = transformPosition;

        gameObject.SetActive(true);

        model.Spining(new Vector3(0f, 45f, 0f));

        model.Floating(new Vector3(0f, 0.25f, 0f), 2f);
    }
}