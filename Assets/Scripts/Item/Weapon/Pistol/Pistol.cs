
using System.Collections;

public class Pistol : Weapon
{
    private void Awake()
    {
        itemCode = ItemCode.PISTOL;
    }

    public override void Equip(bool state)
    {
        animator.SetBool("pistolStance", state);

        gameObject.SetActive(state);
    }

    protected override IEnumerator _Attack()
    {
        do
        {
            yield return null;

            if (itemData.magazinRest > 0)
            {
                --itemData.magazinRest;

                Projectile projectile = Instantiate(this.projectile, muzzle.position, muzzle.rotation);

                projectile.Launch(itemData.projectileDamage, itemData.projectileForce, itemData.projectileLifeTime);

                animator.SetBool("recoil", true);

                yield return _SetCooldown();

                animator.SetBool("recoil", false);
            }
        }
        while (isAttacking == true);
    }
}