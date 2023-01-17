using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_PickUpAxe : Goal_Base
{
    int _priority = 10;

    public override int CalculatePriority()
    {
        return _priority;
    }

    public override bool CanRun() => !worldState.boolValues[WorldStateValues.hasAxe];
}
