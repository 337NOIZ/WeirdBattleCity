
using System.Collections;

using UnityEngine;

using UnityEngine.AI;

public abstract class Enemy : Character
{
    public override CharacterType characterType => CharacterType.enemy;

    [SerializeField] private Canvas _canvas = null;

    public NavMeshAgent navMeshAgent { get; private set; }

    protected Character skillTarget { get; private set; } = null;

    protected Transform destination { get; private set; } = null;

    private void FixedUpdate()
    {
        rigidbody.velocity = Vector3.zero;

        rigidbody.angularVelocity = Vector3.zero;
    }

    public override void Initialize()
    {
        base.Initialize();

        _canvas.worldCamera = Camera.main;

        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.updateRotation = false;

        characterData = GameMaster.instance.gameData.levelData.characterDatas[characterCode];
    }

    public override void Initialize(int characterLevel)
    {
        if (characterInfo == null)
        {
            characterInfo = new CharacterInfo(characterData, null);

            damageableInfo = characterInfo.damageableInfo;

            experienceInfo = characterInfo.experienceInfo;

            skillInfos = characterInfo.skillInfos;

            movementInfo = characterInfo.movementInfo;
        }

        else
        {
            characterInfo.Initialize();
        }

        _healthPointBar.fillAmount = 1f;

        LevelUp(characterLevel - characterInfo.characterLevel);

        StartCoroutine(Thinking());
    }

    protected override void Caching()
    {
        base.Caching();

        animator.SetFloat("movingMotionSpeed", characterInfo.movementInfo.movingSpeed_Multiply);

        navMeshAgent.speed = characterInfo.movementInfo.movingSpeed_Walk;
    }

    protected override bool Invincible() { return false; }

    protected override IEnumerator SkillCooldown(int skillNumber)
    {
        yield return base.SkillCooldown(skillNumber);

        if (skillNumber < this.skillNumber)
        {
            if (orderSkillRoutine != null && skillMotionRoutine == null)
            {
                StopCoroutine(orderSkillRoutine);

                orderSkillRoutine = null;
            }
        }
    }

    protected override void Dead()
    {
        StopAllCoroutines();

        StartCoroutine(_Dead());
    }

    private IEnumerator _Dead()
    {
        --EnemySpawner.instance.spawnCount;

        _ragDoll.AlignTransforms(_model);

        _model.gameObject.SetActive(false);

        _ragDoll.gameObject.SetActive(true);

        animator.Rebind();

        navMeshAgent.isStopped = true;

        if (attacker != null)
        {
            if (experienceInfo != null)
            {
                attacker.GetExperiencePoint(experienceInfo.experiencePoint_Drop);
            }

            attacker.GetMoney(characterInfo.moneyAmount);
        }

        yield return RefreshHealthPointBar();

        _canvas.gameObject.SetActive(false);

        yield return new WaitForSeconds(5f);

        gameObject.SetActive(false);

        _ragDoll.gameObject.SetActive(false);

        _model.gameObject.SetActive(true);

        _canvas.gameObject.SetActive(true);

        ObjectPool.instance.Push(this);
    }

    protected virtual IEnumerator Thinking() { yield return null; }

    protected void Tracking()
    {
        if (_tracking == null)
        {
            _tracking = _Tracking();
        }

        StartCoroutine(_tracking);
    }

    private IEnumerator _tracking = null;

    private IEnumerator _Tracking()
    {
        while (true)
        {
            if (destination != null)
            {
                Debug.DrawLine(transform.position, skillTarget.aimTarget.position, Color.red);

                navMeshAgent.SetDestination(destination.position);

                if (navMeshAgent.desiredVelocity != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(navMeshAgent.desiredVelocity), Time.deltaTime * 2f);
                }

                _aim.position = destination.position;
            }

            yield return null;
        }
    }

    protected void SetSkillTarget()
    {
        skillTarget = Player.instance;

        destination = skillTarget.aimTarget;
    }

    protected IEnumerator orderSkillRoutine = null;

    protected IEnumerator OrderSkillRoutine(int skillNumber)
    {
        var range = characterInfo.skillInfos[skillNumber].range;

        RaycastHit raycastHit;

        if (Physics.Linecast(transform.position, skillTarget.aimTarget.position, out raycastHit, attackableLayer) == true)
        {
            if (raycastHit.collider.gameObject != skillTarget.gameObject || raycastHit.distance > range)
            {
                animator.SetBool("isMoving", true);

                while (true)
                {
                    yield return null;

                    if (Physics.Linecast(transform.position, skillTarget.aimTarget.position, out raycastHit, attackableLayer) == true)
                    {
                        if (raycastHit.collider.gameObject == skillTarget.gameObject && raycastHit.distance <= range)
                        {
                            break;
                        }
                    }
                }

                animator.SetBool("isMoving", false);
            }
        }

        skillMotionRoutine = SkillMotionRoutine(skillNumber);

        yield return skillMotionRoutine;

        orderSkillRoutine = null;
    }

    protected IEnumerator skillMotionRoutine = null;

    protected IEnumerator SkillMotionRoutine(int skillNumber)
    {
        animator.SetFloat("skillMotionSpeed", skillMotionSpeeds[skillNumber]);

        animator.SetBool("isUsingSkill", true);

        animator.SetInteger("skillNumber", skillNumber);

        animator.SetTrigger("skillMotion");

        animationTools.SetEventAction(SkillEffect);

        while (animator.GetBool("isUsingSkill") == true)
        {
            navMeshAgent.velocity = Vector3.zero;

            yield return null;
        }

        StartCoroutine(SkillCooldown(skillNumber));

        skillMotionRoutine = null;
    }

    protected virtual void SkillEffect() { }
}