
using UnityEngine;

public class BareFist : Weapon
{
    private void Awake()
    {
        itemCode = ItemCode.BARE_FIST;
    }

    protected override void __Attack()
    {
        SetCooldown();
    }
}
