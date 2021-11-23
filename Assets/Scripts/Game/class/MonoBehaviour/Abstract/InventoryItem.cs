
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public abstract class InventoryItem : Item
{
    [Space]

    [SerializeField] protected GameObject model = null;

    protected Player player;

    protected string stance;

    protected float drawingMotionTime;

    protected float drawingMotionSpeed;

    protected float reloadingMotionTime;

    protected float reloadingMotionSpeed;

    protected int skillCount;

    protected List<float> skillMotionTimes;

    protected List<float> skillMotionSpeeds;

    protected List<string> skillMotionNames;

    public override void Initialize()
    {
        player = Player.instance;
    }

    public override void Initialize(ItemInfo itemInfo)
    {
        this.itemInfo = itemInfo;

        if (itemInfo.skillInfos != null)
        {
            skillCount = itemInfo.skillInfos.Count;
        }
    }

    protected virtual void Caching()
    {
        drawingMotionSpeed = drawingMotionTime / itemInfo.drawingMotionTime;

        reloadingMotionSpeed = reloadingMotionTime / itemInfo.reloadingMotionTime;

        for (int index = 0; index < skillCount; ++index)
        {
            skillMotionSpeeds[index] = skillMotionTimes[index] / itemInfo.skillInfos[index].skillMotionTime;
        }
    }

    protected IEnumerator cooldown = null;

    protected IEnumerator Cooldown(int skillNumber)
    {
        itemInfo.skillInfos[skillNumber].SetCoolTimer();

        while (itemInfo.skillInfos[skillNumber].cooldownTimer > 0f)
        {
            yield return null;

            itemInfo.skillInfos[skillNumber].cooldownTimer -= Time.deltaTime;
        }

        itemInfo.skillInfos[skillNumber].cooldownTimer = 0f;

        cooldown = null;
    }

    public virtual IEnumerator Draw() { yield return null; }

    public virtual IEnumerator Store() { yield return null; }

    public virtual IEnumerator Skill(int skillNumber) { yield return null; }

    protected IEnumerator skillRoutine = null;

    protected virtual IEnumerator SkillRoutine(int skillNumber) { yield return null; }

    public virtual IEnumerator StopSkill() { yield return null; }

    public virtual IEnumerator StopSkill(bool keepAiming) { yield return null; }

    public virtual IEnumerator Reload() { yield return null; }

    public virtual void StopReload() { }
}