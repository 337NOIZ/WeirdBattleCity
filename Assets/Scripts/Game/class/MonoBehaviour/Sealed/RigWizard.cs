
using UnityEngine;

using UnityEngine.Animations.Rigging;

public sealed class RigWizard : MonoBehaviour
{
    public MultiParentConstraint multiParentConstraint { get; private set; }

    public void Awaken()
    {
        multiParentConstraint = GetComponent<MultiParentConstraint>();
    }

    public void SetMultiParentConstraint(MultiParentConstraintData multiParentConstraintData)
    {
        multiParentConstraint.data = multiParentConstraintData;
    }
}