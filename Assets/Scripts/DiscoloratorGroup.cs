
using System.Collections.Generic;

using UnityEngine;

public class DiscoloratorGroup : MonoBehaviour
{
    [Space]

    [SerializeField] private List<Discolorator> discoloratorList = new List<Discolorator>();

    public void Discolor(Color targetColor, float lapTime)
    {
        for(int index = 0; index < discoloratorList.Count; ++index)
        {
            discoloratorList[index].StartDiscolor(targetColor, lapTime);
        }
    }
}
