
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

    public override void Initialize(int level)
    {
        if(characterInfo != null && characterInfo.level == level)
        {
            characterInfo.Recycling();
        }

        else
        {
            characterInfo = new CharacterInfo(GameMaster.instance.gameData.levelData.characterInfos[characterCode]);

            if (level > 1)
            {
                LevelUp(level - 1);
            }

            Caching();
        }

        GainHealthPoint(healthPoint_Max);

        if (navMeshAgent != null)
        {
            navMeshAgent.speed = characterData.movementData.movingSpeed_Walk;
        }

        Thinking();
    }

    protected override void Dead()
    {
        StopAllCoroutines();

        --EnemySpawner.instance.spawnCount;

        gameObject.SetActive(false);
    }

    private void Thinking()
    {
        if (_thinking != null)
        {
            StopCoroutine(_thinking);
        }

        _thinking = _Thinking();

        StartCoroutine(_thinking);
    }

    protected IEnumerator _thinking = null;

    protected virtual IEnumerator _Thinking() { yield return null; }

    protected void Destination()
    {
        if (_destination != null)
        {
            StopCoroutine(_destination);
        }

        _destination = _Destination();

        StartCoroutine(_destination);
    }

    protected IEnumerator _destination = null;

    protected IEnumerator _Destination()
    {
        while(true)
        {
            navMeshAgent.SetDestination(trackingTarget.position);

            yield return null;
        }
    }
}