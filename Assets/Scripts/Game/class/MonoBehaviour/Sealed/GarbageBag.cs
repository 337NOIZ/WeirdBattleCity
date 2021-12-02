
public sealed class GarbageBag : Neutrality
{
    public override CharacterCode characterCode => CharacterCode.garbageBag;

    protected override void Dead()
    {
        Destroy(this.gameObject);
    }
}