
using UnityEngine;

public class DropedMedikit : Drop
{
    public override void Initialize()
    {
        base.Initialize();

        itemCode = ItemCode.medikit;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}