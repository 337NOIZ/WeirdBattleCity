
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public sealed class CrazySpider : Enemy
{
    public override CharacterCode characterCode => CharacterCode.crazySpider;

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
    }

    protected override IEnumerator Thinking()
    {
        Tracking();

        while (true)
        {
            SetSkillTarget();

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

                    orderSkillRoutine = OrderSkillRoutine(index);

                    StartCoroutine(orderSkillRoutine);

                    while (orderSkillRoutine != null) yield return null;

                    break;
                }
            }
        }
    }

    protected override void SkillEffect()
    {
        var rangedInfo = characterInfo.skillInfos[skillNumber].rangedInfo;

        Projectile projectile = ObjectPool.instance.Pop(rangedInfo.projectileCode);

        projectile.transform.position = muzzle.position;

        projectile.transform.rotation = muzzle.rotation;

        projectile.Launch(this, rangedInfo.projectileInfo);
    }
}