using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Build : Goal_Base
{
    int _priority = 10;

    public override int CalculatePriority()
    {
        return _priority;
    }

    public override bool CanRun()
    {
        Debug.Log("Can Run Goal_Build");

        return worldState.currentChestWood == "Full";
    }
}
