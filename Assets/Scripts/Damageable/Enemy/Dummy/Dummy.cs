
public class Dummy : Enemy
{
    public override void Initialize()
    {
        base.Initialize();

        enemyCode = EnemyCode.DUMMY;
    }
}