
using System.Collections;

using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Space]

    [SerializeField] private Animator _animator = null;

    public Animator animator { get { return _animator; } }

    public new Rigidbody rigidbody { get; protected set; }

    public abstract CharacterType characterType { get; }

    public abstract CharacterCode characterCode { get; }

    public CharacterData characterData { get; protected set; }

    public CharacterInfo characterInfo { get; protected set; }

    public int healthPoint_Max { get { return Mathf.FloorToInt(characterData.damageableData.healthPoint_Max * characterInfo.damageableInfo.healthPoint_Max_Multiple); } }

    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void LateUpdate()
    {
        characterInfo.transformInfo.position = rigidbody.transform.position;

        characterInfo.transformInfo.eulerAngles = rigidbody.transform.eulerAngles;
    }
    public virtual void Initialize(CharacterInfo characterInfo)
    {
        characterData = GameManager.instance.characterDatas[characterCode];

        this.characterInfo = characterInfo;

        transform.position = characterInfo.transformInfo.position;

        transform.eulerAngles = characterInfo.transformInfo.eulerAngles;

        this.characterInfo.damageableInfo.healthPoint = healthPoint_Max;
    }
    public void GetHealthPoint(int healthPoint)
    {
        if (healthPoint > 0)
        {
            characterInfo.damageableInfo.healthPoint += healthPoint;

            if (characterInfo.damageableInfo.healthPoint > healthPoint_Max)
            {
                characterInfo.damageableInfo.healthPoint = healthPoint_Max;
            }
        }

        else if (characterInfo.damageableInfo.invincibleTimer == 0f)
        {
            characterInfo.damageableInfo.healthPoint += healthPoint;

            if (characterInfo.damageableInfo.healthPoint <= 0)
            {
                characterInfo.damageableInfo.healthPoint = 0;

                Dead();
            }
        }
    }
    protected IEnumerator invincible = null;

    protected IEnumerator Invincible()
    {
        while (true)
        {
            if(characterInfo.damageableInfo.invincibleTimer > 0f)
            {
                characterInfo.damageableInfo.invincibleTimer -= Time.deltaTime;
            }
            else if (characterInfo.damageableInfo.invincibleTimer < 0f)
            {
                characterInfo.damageableInfo.invincibleTimer = 0f;
            }
            yield return null;
        }
    }
    protected virtual void Dead()
    {

    }
    protected IEnumerator SkillCooldown(int skillNumber)
    {
        characterInfo.skillInfos[skillNumber].skillCooldownTimer = characterData.skillDatas[skillNumber].skillCooldownTime / characterInfo.skillInfos[skillNumber].skillSpeed;

        while (characterInfo.skillInfos[skillNumber].skillCooldownTimer > 0f)
        {
            yield return null;

            characterInfo.skillInfos[skillNumber].skillCooldownTimer -= Time.deltaTime;
        }
        characterInfo.skillInfos[skillNumber].skillCooldownTimer = 0f;
    }
}