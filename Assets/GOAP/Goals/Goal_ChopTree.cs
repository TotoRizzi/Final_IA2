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

    public override bool CanRun()
    {
        if (worldState.floatValues[WorldStateValues.currentStamina] <= 0f || worldState.boolValues[WorldStateValues.hasAxe] == false || worldState.boolValues[WorldStateValues.bridgeBuilt] == true)
            return false;
        else if (!(worldState.intValues[WorldStateValues.currentWoodOnMe] == 5))
            return true;
        else return false;
    }
}