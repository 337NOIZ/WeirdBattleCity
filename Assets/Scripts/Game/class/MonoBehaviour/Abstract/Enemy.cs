
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

        _Awaken_();
    }

    protected override void _Awaken_()
    {
        _characterData = GameMaster.instance.gameData.levelData.characterDatas[characterCode];

        characterInfo = new CharacterInfo(_characterData);

        base._Awaken_();
    }

    public override void Initialize(int characterLevel)
    {
        if (characterLevel != characterInfo.characterLevel)
        {
            LevelUp(characterLevel - characterInfo.characterLevel);
        }

        characterInfo.Initialize();
    }

    protected override IEnumerator _Dead_()
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

        _navMeshAgent.isStopped = true;

        _navMeshAgent.velocity = Vector3.zero;

        skillWizard.Rebind();

        _skillInfo = null;

        _attacker = null;

        _destination = null;

        _skillTarget = null;

        _skillTarget_Head = null;

        _isMoving = false;

        yield return base._Dead_();

        _canvas.gameObject.SetActive(false);

        yield return new WaitForSeconds(5f);

        gameObject.SetActive(false);

        _model.gameObject.SetActive(true);

        _ragDoll.gameObject.SetActive(false);

        _canvas.gameObject.SetActive(true);

        _healthPointBar.fillAmount = 0f;

        ObjectPool.instance.Push(this);
    }

    protected override IEnumerator _Launce_()
    {
        StartCoroutine(_Thinking_());

        while (true)
        {
            yield return CoroutineWizard.WaitForFixedUpdate;

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

    protected IEnumerator _Thinking_()
    {
        StartCoroutine(_SearchSkillTarget_());

        while (true)
        {
            _SelectSkill_();

            if (_skillInfo == null)
            {
                isMoving = true;

                do
                {
                    yield return null;

                    _SelectSkill_();
                }
                while (_skillInfo == null);

                isMoving = false;
            }

            animatorWizard.AddEventAction(_motionTriggerName, SkillEventAction);

            skillWizard.TrySetSkill(_skillInfo);

            skillWizard.StartSkill(_motionTriggerName);

            yield return skillWizard.WaitForSkillEnd();

            animatorWizard.RemoveEventAction(_motionTriggerName);

            _skillInfo = null;

            yield return null;
        }
    }

    protected IEnumerator _SearchSkillTarget_()
    {
        skillTarget = Player.instance;

        while(true)
        {


            yield return null;
        }
    }

    protected void _SelectSkill_()
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