
using System.Collections;

using UnityEngine;

using UnityEngine.AI;

public abstract class Enemy : Character
{
    public override CharacterType characterType { get { return CharacterType.enemy; } }

    public NavMeshAgent navMeshAgent { get; private set; }

    protected Transform trackingTarget;

    protected override void Awake()
    {
        base.Awake();

        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void FixedUpdate()
    {
        rigidbody.velocity = Vector3.zero;

        rigidbody.angularVelocity = Vector3.zero;
    }
    public override void Initialize(CharacterInfo characterInfo)
    {
        this.characterInfo = new CharacterInfo(characterInfo);

        base.Initialize(characterInfo);

        if (navMeshAgent != null)
        {
            navMeshAgent.speed = characterData.movementData.movingSpeed_walk;
        }
        gameObject.SetActive(true);

        Thinking();
    }
    protected override void Dead()
    {
        base.Dead();

        --EnemySpawner.instance.enemyCount;

        gameObject.SetActive(false);
    }
    private void Thinking()
    {
        if (_thinking == null)
        {
            _thinking = _Thinking();

            StartCoroutine(_thinking);
        }
    }
    protected IEnumerator _thinking = null;

    protected virtual IEnumerator _Thinking() { yield return null; }

    protected void SetDestination()
    {
        StopSetDestination();

        _setDestination = _SetDestination();

        StartCoroutine(_setDestination);
    }
    protected IEnumerator _setDestination = null;

    protected IEnumerator _SetDestination()
    {
        while(true)
        {
            navMeshAgent.SetDestination(trackingTarget.position);

            yield return null;
        }
    }
    protected void StopSetDestination()
    {
        if (_setDestination != null)
        {
            StopCoroutine(_setDestination);

            _setDestination = null;
        }
    }
}