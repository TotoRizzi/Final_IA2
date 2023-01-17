using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_ChopTree : Goal_Base
{
    int _priority = 10;

    public override int CalculatePriority()
    {
        return _priority;
    }

    public override bool CanRun() => worldState.floatValues[WorldStateValues.currentStamina] <= 0f || !worldState.boolValues[WorldStateValues.hasAxe] || worldState.boolValues[WorldStateValues.bridgeBuilt] ?
            false : !(worldState.intValues[WorldStateValues.currentWoodOnMe] == 5) ? true : false;
    
}