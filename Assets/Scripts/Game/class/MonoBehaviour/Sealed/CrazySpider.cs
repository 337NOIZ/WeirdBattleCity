
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public sealed class CrazySpider : Enemy
{
    public override CharacterCode characterCode { get { return CharacterCode.crazySpider; } }

    [Space]

    [SerializeField] private Transform muzzle = null;

    public override void Initialize()
    {
        base.Initialize();

        skillMotionTimes = new List<float>()
        {
            AnimationTools.FrameCountToSeconds(13),

            AnimationTools.FrameCountToSeconds(13),
        };

        skillMotionSpeeds = new List<float>()
        {
            0f,

            0f,
        };

        skillMotionNames = new List<string>()
        {
            "skillMotion_0",

            "skillMotion_0",
        };
    }

    protected override IEnumerator Thinking()
    {
        StartCoroutine(RepeatSetDestination(Player.instance.aimTarget));

        while (true)
        {
            yield return null;

            for (var index = 0; ; ++index)
            {
                if(index >= skillCount)
                {
                    animator.SetBool("isMoving", false);

                    navMeshAgent.velocity = Vector3.zero;

                    break;
                }

                if (characterInfo.skillInfos[index].cooldownTimer == 0f)
                {
                    skillNumber = index;

                    targetingRoutine = TargetingRoutine(index);

                    StartCoroutine(targetingRoutine);

                    while (targetingRoutine != null) yield return null;

                    break;
                }
            }
        }
    }

    protected override void Skill()
    {
        var rangedInfo = characterInfo.skillInfos[skillNumber].rangedInfo;

        Projectile projectile = ObjectPool.instance.Pop(rangedInfo.projectileCode);

        projectile.transform.position = muzzle.position;

        projectile.transform.rotation = muzzle.rotation;

        projectile.Launch(this, rangedInfo.force, rangedInfo.lifeTime, rangedInfo.damage, rangedInfo.statusEffectInfos);
    }
}