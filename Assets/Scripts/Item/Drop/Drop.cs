
using UnityEngine;

public class Drop : MonoBehaviour
{
    [Space]

    [SerializeField] private SpinAndFloat model = null;
    
    private ItemInfo itemInfo;

    public ItemCode itemCode { get; protected set; }

    public virtual void Initialize() { }

    public void Spawn(ItemInfo itemInfo, Vector3 transformPosition)
    {
        this.itemInfo = new ItemInfo(itemInfo);

        transform.position = transformPosition;

        gameObject.SetActive(true);

        model.Spining(new Vector3(0f, 45f, 0f));

        model.Floating(new Vector3(0f, 0.25f, 0f), 2f);
    }
}