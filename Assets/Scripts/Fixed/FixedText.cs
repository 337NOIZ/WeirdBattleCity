
using UnityEngine;

using UnityEngine.UI;

public class FixedText : Text
{
    [SerializeField]
    
    private bool disableWordWrap = false;

    public override string text
    {
        get => base.text;

        set
        {
            if (disableWordWrap)
            {
                base.text = value.Replace(' ', '\u00A0');

                return;
            }

            base.text = value;
        }
    }
}