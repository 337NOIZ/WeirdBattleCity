
using System.Collections;

using UnityEngine;

public sealed class Minotauros : Enemy
{
    public override CharacterCode characterCode { get => CharacterCode.minotauros; }

    public override void Initialize()
    {
        base.Initialize();
    }

    protected override bool IsInvincible() { return false; }

    protected override void SkillEventAction()
    {

    }
}