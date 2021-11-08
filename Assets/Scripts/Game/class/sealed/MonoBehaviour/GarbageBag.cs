
public sealed class GarbageBag : Neutrality
{
    public override CharacterCode characterCode { get { return CharacterCode.garbageBag; } }

    protected override void Dead()
    {
        Destroy(this.gameObject);
    }
}