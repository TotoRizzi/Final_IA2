using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Rest : Goal_Base
{
    int _priority = 10;

    public override int CalculatePriority()
    {
        return _priority;
    }

    public override bool CanRun()
    {
        //Debug.Log("Can Run Goal_Rest");

        //return !(worldState.currentStamina > 0);
        return !(worldState.floatValues[WorldStateValues.currentStamina] > 0);
    }
}
