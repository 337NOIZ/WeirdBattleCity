
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public abstract class InventoryItem : Item
{
    [SerializeField] protected GameObject _model = null;

    protected virtual string _motionTriggerName { get; }

    protected Character _character;

    protected Animator _animator;

    protected AnimatorWizard _animatorWizard;

    protected SkillWizard _skillWizard;

    protected List<SkillInfo> _skillInfos;

    protected int _skillCount;

    public virtual void Awaken(Character character)
    {
        _character = character;

        _animator = character.animator;

        _animatorWizard = character.animatorWizard;

        _skillWizard = character.skillWizard;
    }

    public virtual void Initialize(ItemInfo itemInfo)
    {
        _itemInfo = itemInfo;

        _skillInfos = itemInfo.skillInfos;

        if (_skillInfos != null)
        {
            _skillCount = _skillInfos.Count;
        }
    }

    public virtual IEnumerator Draw() { yield return null; }

    public virtual IEnumerator Store() { yield return null; }

    public virtual void StartSkill(int skillNumber)
    {
        if (_skill == null)
        {
            _skill = _Skill(skillNumber);

            StartCoroutine(_skill);
        }
    }

    protected IEnumerator _skill = null;

    protected virtual IEnumerator _Skill(int skillNumber) { yield return _skill = null; }

    public void StopSkill(bool keepAiming)
    {
        if (_stopSkill == null)
        {
            _stopSkill = _StopSkill(keepAiming);

            StartCoroutine(_stopSkill);
        }
    }

    protected IEnumerator _stopSkill = null;

    protected virtual IEnumerator _StopSkill(bool keepAiming) { yield return _stopSkill = null; }

    public virtual void StartReload() { }

    protected IEnumerator _reload = null;

    protected virtual IEnumerator _Reload() { yield return null; }
}