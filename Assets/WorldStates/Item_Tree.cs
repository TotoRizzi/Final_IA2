using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Tree : Item
{
    public override void Start()
    {
        base.Start();
        worldState.intValues.Add(WorldStateValues.currentWoodOnMe, 0);
    }
    public override void ActionOnTrigger()
    {
        worldState.intValues[WorldStateValues.currentWoodOnMe] = 5;
    }
}
