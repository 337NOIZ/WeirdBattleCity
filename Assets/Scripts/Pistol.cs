public sealed class Pistol : Weapon
{
    public override ItemCode itemCode { get => ItemCode.Pistol; }

    protected override string _motionTriggerName { get; } = "pistol";

    protected override ItemCode _ammo_itemCode { get => ItemCode.PistolAmmo; }

    public override void Awaken(Character character)
    {
        base.Awaken(character);

        _muzzle.Awaken(_character.aim);
    }
}