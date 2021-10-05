
using System.Collections;

public class BareFist : Weapon
{
    private void Awake()
    {
        itemCode = ItemCode.BARE_FIST;
    }

    public override void Equip(bool state)
    {
        animator.SetBool("bareFistStance", state);

        gameObject.SetActive(state);
    }

    protected override IEnumerator _Attack()
    {
        do
        {
            yield return null;

            yield return _SetCooldown();
        }
        while (isAttacking == true);
    }
}