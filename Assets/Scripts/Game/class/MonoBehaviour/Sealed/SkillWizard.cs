
using System.Collections;

using UnityEngine;

using UnityEngine.Events;

public sealed class SkillWizard : MonoBehaviour
{
    public Animator animator { get; private set; }

    public AnimatorWizard animatorWizard { get; private set; }

    private SkillInfo _skillInfo = null;

    public void Awaken()
    {
        animatorWizard = GetComponent<AnimatorWizard>();

        animatorWizard.Awaken();

        animator = animatorWizard.animator;
    }

    public void Rebind()
    {
        StopAllCoroutines();

        animatorWizard.Rebind();
    }

    public bool TrySetSkill(SkillInfo skillInfo)
    {
        if (_skill == null)
        {
            _skillInfo = skillInfo;

            return true;
        }

        return false;
    }

    public IEnumerator SetSkillWhenSkillEnd(SkillInfo skillInfo)
    {
        yield return WaitForSkillEnd();

        _skillInfo = skillInfo;
    }

    public void StartSkill(string eventAction_Key)
    {
        if (TryStopSkill() == true)
        {
            _skill = Skill_(eventAction_Key);

            StartCoroutine(_skill);
        }
    }

    private IEnumerator _skill = null;

    private IEnumerator Skill_(string animatorStance)
    {
        animator.SetBool(animatorStance, true);

        if (_skillInfo.castingMotionTime > 0f)
        {
            animator.SetBool("isCasting", true);

            animator.SetFloat("castingMotionSpeed", _skillInfo.castingMotionSpeed);

            animator.SetInteger("castingMotionNumber", _skillInfo.castingMotionNumber);

            animator.SetTrigger("castingMotion");

            yield return CoroutineWizard.WaitForSeconds(_skillInfo.castingMotionTime);

            animator.SetBool("isCasting", false);
        }

        animator.SetBool("isUsingSkill", true);

        animator.SetFloat("skillMotionSpeed", _skillInfo.skillMotionSpeed);

        animator.SetInteger("skillMotionNumber", _skillInfo.skillMotionNumber);

        animator.SetTrigger("skillMotion");

        if (_skillInfo.skillMotionLoopTime > 0f)
        {
            yield return CoroutineWizard.WaitForSeconds(_skillInfo.skillMotionLoopTime);
        }

        else
        {
            yield return CoroutineWizard.WaitForSeconds(_skillInfo.skillMotionTime);
        }

        animator.SetBool("isUsingSkill", false);

        animator.SetBool(animatorStance, false);

        _skill = null;
    }

    public IEnumerator WaitForSkillEnd()
    {
        while (_skill != null) yield return null;
    }

    public bool TryStopSkill()
    {
        if (_skill != null)
        {
            if (animator.GetBool("isUsingSkill") == false)
            {
                StopCoroutine(_skill);

                animator.SetBool("isCasting", false);

                _skill = null;
            }

            else
            {
                return false;
            }
        }

        return true;
    }

    public void StartSkillCooldown(UnityAction<SkillInfo> unityAction)
    {
        StartCoroutine(SkillCooldown(_skillInfo, unityAction));
    }

    private IEnumerator SkillCooldown(SkillInfo skillInfo, UnityAction<SkillInfo> unityAction)
    {
        do
        {
            yield return null;

            skillInfo.cooldownTimer -= Time.deltaTime;
        }
        while (skillInfo.cooldownTimer > 0f);

        skillInfo.cooldownTimer = 0f;

        if(unityAction != null)
        {
            unityAction.Invoke(skillInfo);
        }
    }
}