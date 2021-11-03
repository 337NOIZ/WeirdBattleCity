
using System.Collections.Generic;

public class CharacterInfo
{
    public TransformInfo transformInfo { get; private set; }

    public DamageableInfo damageableInfo { get; private set; }

    public MovementInfo movementInfo { get; private set; } = null;

    public List<SkillInfo> skillInfos { get; private set; } = null;

    public CharacterInfo(TransformInfo transformInfo, DamageableInfo damageableInfo)
    {
        this.transformInfo = new TransformInfo(transformInfo);

        this.damageableInfo = new DamageableInfo(damageableInfo);
    }
    public CharacterInfo(TransformInfo transformInfo, DamageableInfo damageableInfo, MovementInfo movementInfo)
    {
        this.transformInfo = new TransformInfo(transformInfo);

        this.damageableInfo = new DamageableInfo(damageableInfo);

        this.movementInfo = new MovementInfo(movementInfo);
    }
    public CharacterInfo(TransformInfo transformInfo, DamageableInfo damageableInfo, List<SkillInfo> skillInfos)
    {
        this.transformInfo = new TransformInfo(transformInfo);

        this.damageableInfo = new DamageableInfo(damageableInfo);

        this.skillInfos = new List<SkillInfo>(skillInfos);
    }
    public CharacterInfo(TransformInfo transformInfo, DamageableInfo damageableInfo, MovementInfo movementInfo, List<SkillInfo> skillInfos)
    {
        this.transformInfo = new TransformInfo(transformInfo);

        this.damageableInfo = new DamageableInfo(damageableInfo);

        this.movementInfo = new MovementInfo(movementInfo);

        this.skillInfos = new List<SkillInfo>(skillInfos);
    }
    public CharacterInfo(CharacterInfo characterInfo)
    {
        transformInfo = characterInfo.transformInfo;

        damageableInfo = new DamageableInfo(characterInfo.damageableInfo);

        if(characterInfo.movementInfo != null)
        {
            movementInfo = new MovementInfo(characterInfo.movementInfo);
        }
        if(characterInfo.skillInfos != null)
        {
            skillInfos = new List<SkillInfo>(characterInfo.skillInfos);
        }
    }
}