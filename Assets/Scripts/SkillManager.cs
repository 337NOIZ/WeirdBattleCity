using System.Collections;

using UnityEngine;

public sealed class SkillManager : MonoBehaviour
{
    public Animator animator { get; private set; }

    public AnimatorManager animatorManager { get; private set; }

    private SkillInfo _skillInfo = null;

    public void Awaken()
    {
        animatorManager = GetComponent<AnimatorManager>();

        animatorManager.Awaken();

        animator = animatorManager.animator;
    }

    public void Rebind()
    {
        StopAllCoroutines();

        animatorManager.Rebind();
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
            _skill = _Skill(animatorStance);

            StartCoroutine(_skill);
        }
    }

    private IEnumerator _skill = null;

    private IEnumerator _Skill(string motionTriggerName)
    {
        if (_skillInfo.castingMotionTime > 0f)
        {
            animator.SetFloat("castingMotionSpeed", _skillInfo.castingMotionSpeed);

            animator.SetInteger("castingMotionNumber", _skillInfo.castingMotionNumber);

            animator.SetBool("isCasting", true);

            animator.SetTrigger(motionTriggerName);

            if (_skillInfo.castingMotionLoopTime > 0f)
            {
                yield return CoroutineManager.WaitForSeconds(_skillInfo.castingMotionLoopTime);

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
            animatorManager.InvokeEventAction(motionTriggerName);

            yield return CoroutineManager.WaitForSeconds(_skillInfo.skillMotionLoopTime);

            animatorManager.InvokeEventAction(motionTriggerName);

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
        StartCoroutine(_SkillCooldown(skillInfo));
    }

    private IEnumerator _SkillCooldown(SkillInfo skillInfo)
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