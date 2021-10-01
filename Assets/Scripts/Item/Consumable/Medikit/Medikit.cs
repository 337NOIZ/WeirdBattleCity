
using UnityEngine;

public class Medikit : Consumable
{
    private void Awake()
    {
        itemCode = ItemCode.MEDIKIT;
    }

    protected override void __Consum()
    {
        Debug.Log("Medikit Consum");

        --itemData.count;
    }
}