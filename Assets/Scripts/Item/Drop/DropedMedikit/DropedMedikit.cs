
using UnityEngine;

public class DropedMedikit : Drop
{
    public override void Initialize()
    {
        base.Initialize();

        itemCode = ItemCode.MEDIKIT;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}