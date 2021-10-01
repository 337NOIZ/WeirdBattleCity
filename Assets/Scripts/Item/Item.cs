
using System.Collections;

using UnityEngine;

public enum ItemType
{
    AMMO, CONSUMABLE, WEAPON,
}

public enum ItemCode
{
    ARROW, BARE_FIST, BOW, CROSSBOW, CROSSBOW_BOLT, GRENADE, MEDIKIT, PISTOL, PISTOL_AMMO, SHOTGUN_AMMO, SMG, SMG_AMMO,
}

public abstract class  Item : MonoBehaviour
{
    [Space, SerializeField] protected Transform grip = null;

    [SerializeField] protected Transform muzzle = null;

    [Space, SerializeField] protected Item pair = null;

    [Space, SerializeField] protected Projectile projectile = null;

    public ItemType itemType { get; protected set; }

    public ItemCode itemCode { get; protected set; }

    [Space] public ItemData itemData;

    public bool TryDualWield(bool dualWield)
    {
        if (pair != null)
        {
            pair.gameObject.SetActive(dualWield);

            itemData.dualWield = dualWield;

            return true;
        }

        return false;
    }

    public void SetCooldown()
    {
        if(_setCooldown != null)
        {
            StopCoroutine(_setCooldown);
        }

        _setCooldown = _SetCooldown();

        StartCoroutine(_setCooldown);
    }

    private IEnumerator _setCooldown = null;

    private IEnumerator _SetCooldown()
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