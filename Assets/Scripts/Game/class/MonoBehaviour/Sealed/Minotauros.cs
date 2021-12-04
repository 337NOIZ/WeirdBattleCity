
using UnityEngine;

public class Minotauros : Enemy
{
    public override CharacterCode characterCode => CharacterCode.minotauros;

    [SerializeField] private Transform muzzle = null;

    protected override bool Invincible() { return false; }

    protected override void SkillEffect()
    {

    }
}