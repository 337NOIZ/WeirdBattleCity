
using System.Collections;

using System.Collections.Generic;

public sealed class Grenade : Consumable
{
    public override ItemCode itemCode => ItemCode.grenade;

    public override void Initialize()
    {
        base.Initialize();

        stance = "grenadeStance";
    }

    public override void Initialize(ItemInfo itemInfo)
    {
        base.Initialize(itemInfo);

        skillMotionTimes = new List<float>()
        {
            AnimationTools.FrameCountToSeconds(30),
        };

        skillMotionSpeeds = new List<float>()
        {
            0f,
        };

        Caching();
    }

    protected override IEnumerator SkillRoutine(int skillNumber)
    {
        if (itemInfo.stackCount > 0)
        {
            --itemInfo.stackCount;

            LaunchProjectile(skillNumber);

            //player.animator.SetFloat("skillMotionSpeed", skillMotionSpeeds[skillNumber]);

            //player.animator.SetBool("isUsingSkill", true);

            //player.animator.SetInteger("skillNumber", skillNumber);

            //player.animator.SetTrigger("skillMotion");

            //while (player.animator.GetBool("isUsingSkill") == true) yield return null;

            //StartCoroutine(SkillCooldown(skillNumber));
        }

        skillRoutine = null;

        yield break;
    }
}