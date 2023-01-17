using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Axe : Item
{
    public override void Start()
    {
        base.Start();
        worldState.boolValues.Add(WorldStateValues.hasAxe, false);
    }
    public override void ActionOnTrigger()
    {
        worldState.items.Remove(Items.Axe);
        worldState.boolValues[WorldStateValues.hasAxe] = true;
        Destroy(gameObject);
    }
}
