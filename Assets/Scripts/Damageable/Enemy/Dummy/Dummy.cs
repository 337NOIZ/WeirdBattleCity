
public class Dummy : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        enemyCode = EnemyCode.dummy;
    }
}