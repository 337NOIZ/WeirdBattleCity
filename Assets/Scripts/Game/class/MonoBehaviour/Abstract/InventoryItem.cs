
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.Animations.Rigging;

public abstract class InventoryItem : Item
{
    [Space]

    [SerializeField] protected GameObject model = null;

    [Space]

    [SerializeField] protected Transform muzzle = null;

    protected MultiAimConstraint multiAimConstraint;

    protected Player player;

    protected string stance;

    protected float drawingMotionTime;

    protected float drawingMotionSpeed;

    protected float reloadingMotionTime;

    protected float reloadingMotionSpeed;

    protected int skillCount;

    protected List<float> skillMotionTimes;

    protected List<float> skillMotionSpeeds;

    public override void Initialize()
    {
        multiAimConstraint = GetComponent<MultiAimConstraint>();

        player = Player.instance;
    }

    public override void Initialize(ItemInfo itemInfo)
    {
        this.itemInfo = itemInfo;

        skillInfos = itemInfo.skillInfos;
    }

    protected virtual void Caching()
    {
        if (itemInfo.drawingMotionTime > 0f)
        {
            drawingMotionSpeed = drawingMotionTime / itemInfo.drawingMotionTime;
        }

        if (itemInfo.reloadingMotionTime > 0f)
        {
            reloadingMotionSpeed = reloadingMotionTime / itemInfo.reloadingMotionTime;
        }

        if (skillInfos != null)
        {
            skillCount = skillInfos.Count;
        }

        for (int index = 0; index < skillCount; ++index)
        {
            skillMotionSpeeds[index] = skillMotionTimes[index] / skillInfos[index].skillMotionTime;
        }
    }

    protected IEnumerator SkillCooldown(int skillNumber)
    {
        var skillInfo = itemInfo.skillInfos[skillNumber];

        skillInfo.SetCoolTimer();

        while (skillInfo.cooldownTimer > 0f)
        {
            yield return null;

            skillInfo.cooldownTimer -= Time.deltaTime;
        }

        skillInfo.cooldownTimer = 0f;
    }

    public virtual IEnumerator Draw() { yield return null; }

    public virtual IEnumerator Store() { yield return null; }

    public virtual IEnumerator Skill(int skillNumber) { yield return null; }

    protected IEnumerator skillRoutine = null;

    protected virtual IEnumerator SkillRoutine(int skillNumber) { yield return null; }

    public virtual IEnumerator StopSkill() { yield return null; }

    public virtual IEnumerator StopSkill(bool keepAiming) { yield return null; }

    protected void LaunchProjectile(int skillNumber)
    {
        Projectile projectile;

        var rangedInfo = itemInfo.skillInfos[skillNumber].rangedInfo;

        int count = Mathf.FloorToInt(rangedInfo.division);

        for (int index = 0; index < count; ++index)
        {
            projectile = ObjectPool.instance.Pop(rangedInfo.projectileCode);

            projectile.transform.position = muzzle.position;

            projectile.transform.rotation = muzzle.rotation;

            if (rangedInfo.diffusion > 0f)
            {
                projectile.transform.rotation *= Quaternion.Euler(Random.insideUnitSphere * rangedInfo.diffusion);
            }

            projectile.Launch(player, rangedInfo.projectileInfo);
        }
    }

    public virtual IEnumerator Reload() { yield return null; }
}