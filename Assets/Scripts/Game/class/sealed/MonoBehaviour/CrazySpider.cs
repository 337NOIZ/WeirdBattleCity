
using System.Collections;

using UnityEngine;

public sealed class CrazySpider : Enemy
{
    public override CharacterCode characterCode { get { return CharacterCode.crazySpider; } }

    protected override IEnumerator _Thinking()
    {
        trackingTarget = Player.instance.transform;

        Destination();

        while (true)
        {
            yield return null;

            if (navMeshAgent.remainingDistance < characterData.skillDatas[0].range * characterInfo.skillInfos[0].range_Multiple)
            {
                animator.SetBool("isMoving", false);

                do
                {
                    navMeshAgent.velocity = Vector3.zero;

                    if (characterInfo.skillInfos[0].cooldownTimer == 0)
                    {
                        animator.SetFloat("attackingSpeed", AnimationTools.FrameCountToSeconds(13) / characterData.skillDatas[0].castingTime * characterInfo.skillInfos[0].cooldownSpeed);

                        animator.SetBool("isAttacking", true);

                        animator.SetTrigger("attack");

                        StartCoroutine(SkillCooldown(0));

                        while (animator.GetBool("isAttacking") == true)
                        {
                            navMeshAgent.velocity = Vector3.zero;

                            yield return null;
                        }
                    }

                    yield return null;
                }
                while (navMeshAgent.remainingDistance < characterData.skillDatas[0].range * characterInfo.skillInfos[0].range_Multiple);
            }

            animator.SetFloat("movingSpeed", characterInfo.movementInfo.movingSpeed_Multiply);

            animator.SetBool("isMoving", true);
        }
    }
}