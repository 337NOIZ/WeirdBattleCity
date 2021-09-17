
using UnityEngine.UI;

public class CustomButton : Button
{
    public delegate void Delegate();

    public event Delegate onClickDelegate = null;
}