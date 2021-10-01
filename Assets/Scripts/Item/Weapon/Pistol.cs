
public class Pistol : Weapon
{
    private void Awake()
    {
        itemCode = ItemCode.PISTOL;
    }

    protected override void __Attack()
    {
        if (itemData.magazinRest > 0)
        {
            --itemData.magazinRest;

            Projectile projectile = Instantiate(this.projectile, muzzle.position, muzzle.rotation);

            projectile.Launch(itemData.projectileDamage, itemData.projectileForce, itemData.projectileLifeTime);

            SetCooldown();
        }
    }
}