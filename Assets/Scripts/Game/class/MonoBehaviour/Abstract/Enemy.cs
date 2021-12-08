
using System.Collections;

using UnityEngine;

using UnityEngine.AI;

public abstract class Enemy : Character
{
    public override CharacterType characterType { get => CharacterType.enemy; }

    private NavMeshAgent _navMeshAgent;

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

    public override void Awaken()
    {
        if (_canvas != null)
        {
            _canvas.worldCamera = Camera.main;
        }

        base.Awaken();

        _navMeshAgent = GetComponent<NavMeshAgent>();

        if (_navMeshAgent != null)
        {
            _navMeshAgent.updateRotation = false;
        }
    }

    public override void Initialize()
    {
        _characterData = GameMaster.instance.gameData.levelData.characterDatas[characterCode];

        _characterInfo = new CharacterInfo(_characterData);

        base.Initialize();
    }

    public override void SetLevel(int characterLevel)
    {
        if (characterLevel != _characterInfo.characterLevel)
        {
            LevelUp(characterLevel - _characterInfo.characterLevel);
        }

        animator.SetFloat("movingMotionSpeed", _characterInfo.movementInfo.movingSpeed_Multiply);

        _navMeshAgent.speed = _characterInfo.movementInfo.movingSpeed_Walk;

        _healthPointBar.StartFillByLerp(1f - damageableInfo.healthPoint / damageableInfo.healthPoint_Max, 0.1f);

        StartCoroutine(Thinking());
    }

    protected override void Dead()
    {
        StopAllCoroutines();

        StartCoroutine(_Dead_());
    }

    private IEnumerator _Dead_()
    {
        --EnemySpawner.instance.spawnCount;

        TransformWizard.AlignTransforms(_ragDoll, _model);

        _model.gameObject.SetActive(false);

        _ragDoll.gameObject.SetActive(true);

        if (_attacker != null)
        {
            if (_experienceInfo != null)
            {
                _attacker.GetExperiencePoint(_experienceInfo.experiencePoint_Drop);
            }

            _attacker.GetMoney(_characterInfo.moneyAmount);
        }

        _healthPointBar.StartFillByLerp(1f - damageableInfo.healthPoint / damageableInfo.healthPoint_Max, 0.1f);

        while (_healthPointBar.fillByLerp != null) yield return null;

        _canvas.gameObject.SetActive(false);

        yield return new WaitForSeconds(5f);

        gameObject.SetActive(false);

        _model.gameObject.SetActive(true);

        _ragDoll.gameObject.SetActive(false);

        _canvas.gameObject.SetActive(true);

        _healthPointBar.fillAmount = 1f;

        animatorWizard.Rebind();

        _attacker = null;

        _isMoving = false;

        ObjectPool.instance.Push(this);
    }

    protected override bool SearchSkillTarget()
    {
        skillTarget = Player.instance;

        return true;
    }

    protected IEnumerator Thinking()
    {
        StartCoroutine(Tracking());

        int index = 0;

        while (true)
        {
            if (_skillInfos[index].cooldownTimer == 0f)
            {
                yield return OrderSkill(_skillInfos[index]);

                index = 0;
            }

            else if (++index >= _skillInfos_Count)
            {
                index = 0;
            }

            yield return null;
        }
    }

    protected IEnumerator Tracking()
    {
        while (true)
        {
            SearchSkillTarget();

            while(_skillTarget != null)
            {
                _navMeshAgent.SetDestination(_skillTarget_transform.position);

                if (_isMoving == false)
                {
                    _navMeshAgent.velocity = Vector3.zero;
                }

                if (_navMeshAgent.desiredVelocity != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_navMeshAgent.desiredVelocity), Time.deltaTime * 2f);
                }

                _aim.position = _skillTarget_aimTarget.position;

                yield return null;
            }

            yield return null;
        }
    }

    protected IEnumerator orderSkill = null;

    protected IEnumerator OrderSkill(SkillInfo skillInfo)
    {
        yield return skillWizard.SetSkillWhenSkillEnd(skillInfo);

        _skillInfo = skillInfo;

        if (IsSkillTargetWithinRange(skillInfo.range) == false || IsSkillValid() == false)
        {
            isMoving = true;

            while (true)
            {
                yield return null;

                if (IsSkillTargetWithinRange(skillInfo.range) == true && IsSkillValid() == true)
                {
                    break;
                }
            }

            isMoving = false;
        }

        animatorWizard.AddEventAction(_animatorStance, SkillEventAction);

        skillWizard.StartSkill(_animatorStance);

        yield return skillWizard.WaitForSkillEnd();

        animatorWizard.RemoveEventAction(_animatorStance);

        _skillInfo.SetCoolTimer();

        skillWizard.StartSkillCooldown(CompareSkillPriority);

        orderSkill = null;
    }

    protected void CompareSkillPriority(SkillInfo skillInfo)
    {
        if(skillInfo.priority < _skillInfo.priority)
        {
            //skillWizard.TryStopSkill();
        }
    }
}