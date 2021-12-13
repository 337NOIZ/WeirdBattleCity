
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

    public void StartSkill(string animatorStance)
    {
        if (_skill == null)
        {
            _skill = Skill_(animatorStance);

            StartCoroutine(_skill);
        }
    }

    private IEnumerator _skill = null;

    private IEnumerator Skill_(string motionTriggerName)
    {
        if (_skillInfo.castingMotionTime > 0f)
        {
            animator.SetFloat("castingMotionSpeed", _skillInfo.castingMotionSpeed);

            animator.SetInteger("castingMotionNumber", _skillInfo.castingMotionNumber);

            animator.SetBool("isCasting", true);

            animator.SetTrigger(motionTriggerName);

            if (_skillInfo.castingMotionLoopTime > 0f)
            {
                yield return CoroutineWizard.WaitForSeconds(_skillInfo.castingMotionLoopTime);

                animator.SetBool("isCasting", false);
            }

            else
            {
                while (animator.GetBool("isCasting") == true) yield return null;
            }
        }

        animator.SetFloat("skillMotionSpeed", _skillInfo.skillMotionSpeed);

        animator.SetInteger("skillMotionNumber", _skillInfo.skillMotionNumber);

        animator.SetBool("isUsingSkill", true);

        animator.SetTrigger(motionTriggerName);

        if (_skillInfo.skillMotionLoopTime > 0f)
        {
            animatorWizard.InvokeEventAction(motionTriggerName);

            yield return CoroutineWizard.WaitForSeconds(_skillInfo.skillMotionLoopTime);

            animatorWizard.InvokeEventAction(motionTriggerName);

            animator.SetBool("isUsingSkill", false);
        }

        else
        {
            while (animator.GetBool("isUsingSkill") == true) yield return null;
        }

        _skillInfo.SetCoolTimer();

        StartSkillCooldown(_skillInfo);

        _skill = null;
    }

    public IEnumerator WaitForSkillEnd()
    {
        while (_skill != null) yield return null;
    }

    public void StopSkill()
    {
        if (_skill != null)
        {
            if(animator.GetBool("isUsingSkill") == false)
            {
                StopCoroutine(_skill);

                animator.SetBool("isCasting", false);

                _skill = null;
            }
        }
    }

    public void StartSkillCooldown(SkillInfo skillInfo)
    {
        StartCoroutine(_SkillCooldown_(skillInfo));
    }

    private IEnumerator _SkillCooldown_(SkillInfo skillInfo)
    {
        do
        {
            yield return null;

            skillInfo.cooldownTimer -= Time.deltaTime;
        }
        while (skillInfo.cooldownTimer > 0f);

        skillInfo.cooldownTimer = 0f;
    }
}