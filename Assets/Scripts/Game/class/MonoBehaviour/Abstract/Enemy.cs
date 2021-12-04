
using System.Collections;

using UnityEngine;

using UnityEngine.AI;

public abstract class Enemy : Character
{
    public override CharacterType characterType => CharacterType.enemy;

    [SerializeField] private Transform[] _muzzles = null;

    [SerializeField] private Canvas _canvas = null;

    private NavMeshAgent _navMeshAgent;

    private Character target_Skill = null;

    private Transform target_Skill_transform = null;

    private Transform target_Skill_target_Aim = null;

    private bool _isMoving;

    private bool isMoving
    {
        set
        {
            _isMoving = value;

            if (_isMoving == true)
            {
                animator.SetBool("isMoving", true);
            }

            else
            {
                animator.SetBool("isMoving", false);
            }
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = Vector3.zero;

        _rigidbody.angularVelocity = Vector3.zero;
    }

    public override void Initialize()
    {
        base.Initialize();

        if (_canvas != null)
        {
            _canvas.worldCamera = Camera.main;
        }

        _navMeshAgent = GetComponent<NavMeshAgent>();

        if (_navMeshAgent != null)
        {
            _navMeshAgent.updateRotation = false;
        }

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

            if(skillInfos != null)
            {
                skillInfos_Count = skillInfos.Count;
            }

            movementInfo = characterInfo.movementInfo;
        }

        else
        {
            characterInfo.Initialize();
        }

        if (characterLevel != characterInfo.characterLevel)
        {
            LevelUp(characterLevel - characterInfo.characterLevel);
        }

        animator.SetFloat("movingMotionSpeed", characterInfo.movementInfo.movingSpeed_Multiply);

        _navMeshAgent.speed = characterInfo.movementInfo.movingSpeed_Walk;

        _healthPointBar.fillAmount = 1f;

        StartCoroutine(_healthPointBar.FillByLerp(1f - damageableInfo.healthPoint / damageableInfo.healthPoint_Max, 0.1f));

        StartCoroutine(Thinking());
    }

    protected override IEnumerator SkillCooldown(SkillInfo skillInfo)
    {
        yield return base.SkillCooldown(skillInfo);

        if (skillInfo.priority > this.skillInfo.priority)
        {
            OrderSkill(skillInfo);
        }
    }

    protected override void Dead()
    {
        StopAllCoroutines();

        StartCoroutine(_Dead_());
    }

    private IEnumerator _Dead_()
    {
        --EnemySpawner.instance.spawnCount;

        _ragDoll.AlignTransforms(_model);

        _model.gameObject.SetActive(false);

        _ragDoll.gameObject.SetActive(true);

        if (attacker != null)
        {
            if (experienceInfo != null)
            {
                attacker.GetExperiencePoint(experienceInfo.experiencePoint_Drop);
            }

            attacker.GetMoney(characterInfo.moneyAmount);
        }

        yield return _healthPointBar.FillByLerp(1f - damageableInfo.healthPoint / damageableInfo.healthPoint_Max, 0.1f);

        _canvas.gameObject.SetActive(false);

        yield return new WaitForSeconds(5f);

        gameObject.SetActive(false);

        _ragDoll.gameObject.SetActive(false);

        _model.gameObject.SetActive(true);

        _canvas.gameObject.SetActive(true);

        animator.Rebind();

        target_Skill = null;

        target_Skill_transform = null;

        target_Skill_target_Aim = null;

        _isMoving = false;

        ObjectPool.instance.Push(this);
    }

    private IEnumerator Thinking()
    {
        StartCoroutine(Tracking());

        int index = 0;

        while (true)
        {
            if (skillInfos[index].cooldownTimer == 0f)
            {
                skillInfo = skillInfos[index];

                _orderSkill_ = _OrderSkill_();

                StartCoroutine(_orderSkill_);

                while (_orderSkill_ != null) yield return null;

                index = 0;
            }

            else if(++index >= skillInfos_Count)
            {
                index = 0;
            }

            yield return null;
        }
    }

    private IEnumerator Tracking()
    {
        while (true)
        {
            if (target_Skill != null)
            {
                _navMeshAgent.SetDestination(target_Skill_transform.position);

                if(_isMoving == false)
                {
                    _navMeshAgent.velocity = Vector3.zero;
                }

                if (_navMeshAgent.desiredVelocity != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_navMeshAgent.desiredVelocity), Time.deltaTime * 2f);
                }

                _aim.position = target_Skill_target_Aim.position;
            }

            yield return null;
        }
    }

    private void SetSkillTarget(Character target_Skill)
    {
        this.target_Skill = target_Skill;

        target_Skill_transform = target_Skill.transform;

        target_Skill_target_Aim = target_Skill.target_Aim;
    }

    private void OrderSkill(SkillInfo skillInfo)
    {
        if (StopOrderSkill() == true)
        {
            this.skillInfo = skillInfo;

            _orderSkill_ = _OrderSkill_();

            StartCoroutine(_orderSkill_);
        }
    }

    private IEnumerator _orderSkill_ = null;

    private IEnumerator _OrderSkill_()
    {
        var range = skillInfo.range;

        if(SkillValidityCheck() == false)
        {
            isMoving = true;

            do
            {
                yield return null;
            }
            while (SkillValidityCheck() == true);

            isMoving = false;
        }

        if (skillInfo.castingMotionTime > 0f)
        {
            castingMotionRoutine = CastingMotionRoutine();

            yield return castingMotionRoutine;
        }

        skillMotion = SkillMotion();

        yield return skillMotion;

        _orderSkill_ = null;
    }

    protected virtual bool SkillValidityCheck()
    {
        RaycastHit raycastHit;

        if (Physics.Raycast(_target_Aim.position, target_Skill_target_Aim.position, out raycastHit, skillInfo.range, attackable) == true)
        {
            if (raycastHit.collider.gameObject == target_Skill.gameObject)
            {
                return true;
            }
        }

        return false;
    }

    private bool StopOrderSkill()
    {
        if (_orderSkill_ == null)
        {
            return true;
        }

        else if (castingMotionRoutine == null && skillMotion == null)
        {
            StopCoroutine(_orderSkill_);

            return true;
        }

        return false;
    }

    private IEnumerator castingMotionRoutine = null;

    private IEnumerator CastingMotionRoutine()
    {
        animator.SetFloat("castingMotionSpeed", skillInfo.castingMotionSpeed);

        animator.SetInteger("castingMotionNumber", skillInfo.castingMotionNumber);

        animator.SetTrigger("castingMotion");

        animator.SetBool("isCasting", true);

        while (animator.GetBool("isCasting") == true) yield return null;

        castingMotionRoutine = null;
    }

    private IEnumerator skillMotion = null;

    private IEnumerator SkillMotion()
    {
        animator.SetFloat("skillMotionSpeed", skillInfo.skillMotionSpeed);

        animator.SetInteger("skillMotionNumber", skillInfo.skillMotionNumber);

        animator.SetTrigger("skillMotion");

        animator.SetBool("isUsingSkill", true);

        animationTools.SetEventAction(SkillEffect);

        while (animator.GetBool("isUsingSkill") == true) yield return null;

        StartCoroutine(SkillCooldown(skillInfo));

        target_Skill = null;

        skillMotion = null;
    }

    protected virtual void SkillEffect() { }
}