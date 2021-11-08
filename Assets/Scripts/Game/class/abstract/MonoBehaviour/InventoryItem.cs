
using System.Collections;

using UnityEngine;

public abstract class InventoryItem : Item
{
    [Space]

    [SerializeField] protected GameObject model = null;

    protected string stance;

    protected float cooldownTime;

    protected float drawingTime;

    protected float consumingTime;

    protected Player player;

    public override void Initialize()
    {
        itemData = GameMaster.instance.gameData.levelData.itemDatas[itemCode];

        player = Player.instance;
    }

    public override void Initialize(ItemInfo itemInfo)
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

    protected IEnumerator _Cooldown()
    {
        itemInfo.cooldownTimer = itemData.cooldownTime / itemInfo.cooldownSpeed;

        while (itemInfo.cooldownTimer > 0f)
        {
            yield return null;

            itemInfo.cooldownTimer -= Time.deltaTime;
        }

        itemInfo.cooldownTimer = 0f;

        _cooldown = null;
    }

    public virtual IEnumerator Draw() { yield return null; }

    public virtual IEnumerator Store() { yield return null; }

    public virtual IEnumerator Consum() { yield return null; }

    public virtual void StopConsum() { }

    public virtual IEnumerator Attack() { yield return null; }

    public virtual IEnumerator StopAttack(bool keepAiming) { yield return null; }

    public virtual IEnumerator Reload() { yield return null; }

    public virtual void StopReload() { }
}