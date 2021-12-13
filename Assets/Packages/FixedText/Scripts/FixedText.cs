
using UnityEngine;

using UnityEngine.UI;

public sealed class FixedText : Text
{
    [SerializeField] private bool wordwrap = true;

    public override string text
    {
        get => base.text;

        set
        {
            if (wordwrap == true)
            {
                base.text = value;
            }

            else
            {
                base.text = value.Replace(' ', '\u00A0');
            }
        }
    }
}