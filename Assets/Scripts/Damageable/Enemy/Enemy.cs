
public class Enemy : Damageable
{
    protected override void Dead()
    {
        Destroy(gameObject);
    }
}