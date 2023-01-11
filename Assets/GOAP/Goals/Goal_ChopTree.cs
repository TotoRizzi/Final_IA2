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
        Debug.Log("Can Run Goal_ChopTree");

        if (worldState.currentStamina <= 0 || worldState.hasAxe == false || worldState.bridgeBuilt == true) 
            return false;
        else return !(worldState.currentWoodOnMe == 5);
    }
}