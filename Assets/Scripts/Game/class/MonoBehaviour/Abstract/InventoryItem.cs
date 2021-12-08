
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public abstract class InventoryItem : Item
{
    [SerializeField] protected GameObject _model = null;

    protected Character _character;

    protected Animator _animator;

    protected AnimatorWizard _animatorWizard;

    protected SkillWizard _skillWizard;

    protected List<SkillInfo> _skillInfos;

    protected int _skillCount;

    protected string _animatorStance;

    protected SkillInfo _skillInfo;

    public override void Awaken(Character character)
    {
        _character = character;

        _animator = character.animator;

        _animatorWizard = character.animatorWizard;

        _skillWizard = character.skillWizard;
    }

    public override void Initialize(ItemInfo itemInfo)
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
        if (skill == null)
        {
            skill = Skill(skillNumber);

            StartCoroutine(skill);
        }
    }

    public IEnumerator skill { get; protected set; } = null;

    protected virtual IEnumerator Skill(int skillNumber) { yield return null; }

    public virtual IEnumerator StopSkill() { yield return null; }

    public virtual IEnumerator StopSkill(bool keepAiming) { yield return null; }

    protected virtual void _SkillEventAction_() { }

    public virtual IEnumerator Reload() { yield return null; }
}