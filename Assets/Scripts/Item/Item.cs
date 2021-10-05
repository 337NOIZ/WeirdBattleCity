
using System.Collections;

using UnityEngine;

public abstract class  Item : MonoBehaviour
{
    [Space]

    [SerializeField] protected Transform grip = null;

    [SerializeField] protected Transform muzzle = null;

    [Space]

    [SerializeField] protected Item pair = null;

    [Space]

    [SerializeField] protected Projectile projectile = null;

    [Space] public ItemData itemData;

    public ItemType itemType { get; protected set; }

    public ItemCode itemCode { get; protected set; }

    protected Animator animator;

    public void Initialize(Animator animator)
    {
        this.animator = animator;

        gameObject.SetActive(false);
    }

    public bool TryDualWield(bool dualWield)
    {
        if (pair != null)
        {
            itemData.dualWield = dualWield;

            return true;
        }

        return false;
    }

    protected void SetCooldown()
    {
        if(_setCooldown != null)
        {
            StopCoroutine(_setCooldown);
        }

        _setCooldown = _SetCooldown();

        StartCoroutine(_setCooldown);
    }

    private IEnumerator _setCooldown = null;

    protected IEnumerator _SetCooldown()
    {
        itemData.cooldown = itemData.cooldownTime;

        while (itemData.cooldown > 0f)
        {
            itemData.cooldown -= Time.deltaTime;

            yield return null;
        }

        itemData.cooldown = 0f;

        _setCooldown = null;
    }

    public virtual void Equip(bool state) { }

    public virtual bool Consum(bool state)
    {
        return false;
    }

    public virtual void Attack(bool state) { }

    public virtual bool Reload(bool state)
    {
        return false;
    }
}