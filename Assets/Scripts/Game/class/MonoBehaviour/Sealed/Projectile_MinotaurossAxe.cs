
public sealed class Projectile_MinotaurossAxe : Projectile
{
    public override ProjectileCode projectileCode { get => ProjectileCode.MinotaurossAxe; }

    protected override void ActionOnHit(HitBox hitBox)
    {
        if(hitBox != null)
        {
            _actionOnHit.Invoke(hitBox);
        }
    }
}