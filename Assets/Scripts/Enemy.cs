using System.Collections;

using UnityEngine;

using UnityEngine.AI;

public abstract class Enemy : Character
{
    public override CharacterType characterType { get => CharacterType.Enemy; }

    protected NavMeshAgent _navMeshAgent;

    protected Transform _destination = null;

    protected Character _skillTarget = null;

    protected Transform _skillTarget_Head = null;

    protected Character skillTarget
    {
        set
        {
            _skillTarget = value;

            if (_skillTarget != null)
            {
                _destination = _skillTarget.transform;

                _skillTarget_Head = _skillTarget.head;
            }

            else
            {
                _destination = null;

                _skillTarget_Head = null;
            }
        }
    }

    [SerializeField] protected bool _isMoving;

    protected bool isMoving
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

    protected int _skillNumber = 0;

    protected void FixedUpdate()
    {
        _rigidbody.velocity = Vector3.zero;

        _rigidbody.angularVelocity = Vector3.zero;
    }

    public override void Awaken()
    {
        base.Awaken();

        if (_canvas != null)
        {
            _canvas.worldCamera = Camera.main;
        }

        _navMeshAgent = GetComponent<NavMeshAgent>();

        if (_navMeshAgent != null)
        {
            _navMeshAgent.updateRotation = false;
        }

        _Awaken();
    }

    protected override void _Awaken()
    {
        _characterData = GameManager.instance.gameData.levelData.characterDatas[characterCode];

        characterInfo = new CharacterInfo(_characterData);

        base._Awaken();
    }

    public override void Initialize(int characterLevel)
    {
        if (characterLevel != characterInfo.characterLevel)
        {
            LevelUp(characterLevel - characterInfo.characterLevel);
        }

        characterInfo.Initialize();
    }

    protected override IEnumerator _Dead()
    {
        --EnemySpawner.instance.spawnCount;

        if (_attacker != null)
        {
            if (_experienceInfo != null)
            {
                _attacker.GetExperiencePoint(_experienceInfo.experiencePoint_Drop);
            }

            _attacker.GetMoney(characterInfo.moneyAmount);
        }

        _navMeshAgent.velocity = Vector3.zero;

        _navMeshAgent.enabled = false;

        skillManager.Rebind();

        _skillInfo = null;

        _attacker = null;

        _destination = null;

        _skillTarget = null;

        _skillTarget_Head = null;

        _isMoving = false;

        yield return base._Dead();

        _canvas.gameObject.SetActive(false);

        yield return new WaitForSeconds(5f);

        gameObject.SetActive(false);

        _navMeshAgent.enabled = true;

        _model.gameObject.SetActive(true);

        _ragDoll.gameObject.SetActive(false);

        _canvas.gameObject.SetActive(true);

        _healthPointBar.fillAmount = 0f;

        ObjectPool.instance.Push(this);
    }

    protected override IEnumerator _Launch()
    {
        StartCoroutine(_Thinking());

        while (true)
        {
            yield return CoroutineManager.WaitForFixedUpdate;

            if (_skillTarget_Head != null)
            {
                _aim.position = _skillTarget_Head.position;
            }

            if (_destination != null)
            {
                _navMeshAgent.SetDestination(_destination.position);

                if(_isMoving == true)
                {
                    if (_isRunning == true)
                    {
                        _navMeshAgent.speed = _movementInfo.movingSpeed_Run;
                    }

                    else
                    {
                        _navMeshAgent.speed = _movementInfo.movingSpeed_Walk;
                    }

                    animator.SetFloat("movingMotionSpeed", characterInfo.movementInfo.movingSpeed_Multiply);
                }

                else
                {
                    _navMeshAgent.velocity = Vector3.zero;
                }

                if (_navMeshAgent.desiredVelocity != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_navMeshAgent.desiredVelocity), Time.deltaTime * 2f);
                }
            }
        }
    }

    protected IEnumerator _Thinking()
    {
        StartCoroutine(_SearchSkillTarget());

        while (true)
        {
            _SelectSkill();

            if (_skillInfo == null)
            {
                isMoving = true;

                do
                {
                    yield return null;

                    _SelectSkill();
                }
                while (_skillInfo == null);

                isMoving = false;
            }

            animatorManager.AddEventAction(_motionTriggerName, SkillEventAction);

            skillManager.TrySetSkill(_skillInfo);

            skillManager.StartSkill(_motionTriggerName);

            yield return skillManager.WaitForSkillEnd();

            animatorManager.RemoveEventAction(_motionTriggerName);

            _skillInfo = null;

            yield return null;
        }
    }

    protected IEnumerator _SearchSkillTarget()
    {
        skillTarget = Player.instance;

        while(true)
        {


            yield return null;
        }
    }

    protected void _SelectSkill()
    {
        for (_skillNumber = 0; _skillNumber < _skillInfos_Count; ++_skillNumber)
        {
            if (IsSkillValid(_skillNumber) == true)
            {
                _skillInfo = _skillInfos[_skillNumber];

                break;
            }
        }
    }
}