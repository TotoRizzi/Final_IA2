using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_SaveLoot : Goal_Base
{
    int _priority = 10;

    public override int CalculatePriority()
    {
        return _priority;
    }

    public override bool CanRun()
    {
        //Debug.Log("Can Run Save Loot");

        /*if (worldState.currentStamina <= 0) 
            return false;
        else return worldState.currentWoodOnMe == 5;*/

        if (worldState.floatValues[WorldStateValues.currentStamina] <= 0)
            return false;
        else return worldState.intValues[WorldStateValues.currentWoodOnMe] == 5;
    }
}
