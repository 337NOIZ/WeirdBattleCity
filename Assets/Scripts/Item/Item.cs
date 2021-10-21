
using System.Collections;

using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [Space]

    [SerializeField] protected GameObject model = null;

    public ItemType itemType { get; protected set; }

    public ItemCode itemCode { get; protected set; }

    protected string stance;

    protected float cooldownTime_Seconds;

    protected float drawingTime_Seconds;

    protected float reloadingTime_Seconds;

    public ItemData itemData { get; protected set; }

    public Animator animator { get; set; }

    public ItemInfo itemInfo { get; protected set; }

    private void Start()
    {
        itemData = GameManager.instance.itemDatas[itemCode];
    }

    public void Initialize(ItemInfo itemInfo)
    {
        this.itemInfo = itemInfo;
    }

    protected void Cooldown()
    {
        if (_cooldown != null)
        {
            StopCoroutine(_cooldown);
        }

        _cooldown = _Cooldown();

        StartCoroutine(_cooldown);
    }

    protected IEnumerator _cooldown = null;

    private IEnumerator _Cooldown()
    {
        itemInfo.cooldownTime = itemData.cooldownTime_Seconds / itemInfo.cooldownSpeed;

        while (itemInfo.cooldownTime > 0f)
        {
            yield return null;

            itemInfo.cooldownTime -= Time.deltaTime;
        }

        itemInfo.cooldownTime = 0f;

        _cooldown = null;
    }

    public IEnumerator Draw()
    {
        animator.SetBool(stance, true);

        animator.SetFloat("drawingSpeed", drawingTime_Seconds / itemData.drawingTime_Seconds * itemInfo.drawingSpeed);

        animator.SetTrigger("draw");

        animator.SetBool("isDrawing", true);

        model.SetActive(true);

        while (animator.GetBool("isDrawing") == true) yield return null;
    }

    public virtual IEnumerator Store() { yield return null; }

    public virtual void Consum(bool state) { }

    public virtual void Attack(bool state) { }

    public virtual void Reload(bool state) { }
}