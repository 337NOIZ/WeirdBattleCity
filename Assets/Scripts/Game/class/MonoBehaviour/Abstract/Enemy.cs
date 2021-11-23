
using System.Collections;

using UnityEngine;

using UnityEngine.AI;

public abstract class Enemy : Character
{
    public override CharacterType characterType { get { return CharacterType.enemy; } }

    public NavMeshAgent navMeshAgent { get; private set; }

    protected Transform destinationTarget { get; private set; }

    public override void Initialize()
    {
        base.Initialize();

        characterData = GameMaster.instance.gameData.levelData.characterDatas[characterCode];

        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.updateRotation = false;
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = Vector3.zero;

        rigidbody.angularVelocity = Vector3.zero;
    }

    public override void Initialize(int level)
    {
        if (characterInfo != null && characterInfo.level == level)
        {
            characterInfo.Initialize();
        }

        else
        {
            characterInfo = new CharacterInfo(GameMaster.instance.gameData.levelData.characterDatas[characterCode], null);

            if (level > 1)
            {
                LevelUp(level - 1);
            }

            if (characterInfo.skillInfos != null)
            {
                skillCount = characterInfo.skillInfos.Count;
            }

            Caching();
        }

        animator.SetFloat("movingMotionSpeed", characterInfo.movementInfo.movingSpeed_Multiply);

        RefreshHealthPointBar();

        StartCoroutine(Thinking());
    }

    protected override void Caching()
    {
        base.Caching();

        navMeshAgent.speed = characterData.movementData.movingSpeed_Walk;
    }

    protected override bool Invincible() { return false; }

    public override void GainExperiencePoint(float experiencePoint) { }

    protected override IEnumerator SkillCooldown(int skillNumber)
    {
        yield return base.SkillCooldown(skillNumber);

        if (skillNumber < this.skillNumber)
        {
            if (targetingRoutine != null && skillRoutine == null)
            {
                StopCoroutine(targetingRoutine);

                targetingRoutine = null;
            }
        }
    }

    protected override void Dead()
    {
        if (attacker != null && characterInfo.experienceInfo != null)
        {
            attacker.GainExperiencePoint(characterInfo.experienceInfo.experiencePoint_Gain);
        }

        StopAllCoroutines();

        animator.Rebind();

        --EnemySpawner.instance.spawnCount;

        ObjectPool.instance.Push(this);
    }

    protected virtual IEnumerator Thinking() { yield return null; }

    protected IEnumerator RepeatSetDestination(Transform destinationTarget)
    {
        this.destinationTarget = destinationTarget;

        while (true)
        {
            navMeshAgent.SetDestination(destinationTarget.position);

            if (navMeshAgent.desiredVelocity != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(navMeshAgent.desiredVelocity), Time.deltaTime * 2f);
            }

            aim.position = destinationTarget.position;

            yield return null;
        }
    }

    protected IEnumerator targetingRoutine = null;

    protected IEnumerator TargetingRoutine(int skillNumber)
    {
        var range = characterInfo.skillInfos[skillNumber].range;

        if (Vector3.Distance(transform.position, destinationTarget.position) > range)
        {
            animator.SetBool("isMoving", true);

            do
            {
                yield return null;
            }
            while (Vector3.Distance(transform.position, destinationTarget.position) > range);

            animator.SetBool("isMoving", false);
        }

        skillRoutine = SkillRoutine(skillNumber);

        yield return skillRoutine;

        targetingRoutine = null;
    }

    protected IEnumerator skillRoutine = null;

    protected IEnumerator SkillRoutine(int skillNumber)
    {
        animator.SetFloat("skillMotionSpeed", skillMotionSpeeds[skillNumber]);

        animator.SetBool("isUsingSkill", true);

        animator.SetTrigger(skillMotionNames[skillNumber]);

        animationTools.SetEventAction(Skill);

        while (animator.GetBool("isUsingSkill") == true)
        {
            navMeshAgent.velocity = Vector3.zero;

            yield return null;
        }

        StartCoroutine(SkillCooldown(skillNumber));

        skillRoutine = null;
    }

    protected virtual void Skill() { }
}