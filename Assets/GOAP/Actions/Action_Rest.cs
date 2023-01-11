using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Rest : Action_Base
{
    List<System.Type> supportedGoals = new List<System.Type>() { typeof(Goal_Rest) };

    public override List<System.Type> GetSupportedGoals()
    {
        return supportedGoals;
    }

    public override void OnTick()
    {
        worldState.currentStamina = 5;
    }

    public override void OnActivated(Goal_Base _linkedGoal)
    {
        base.OnActivated(_linkedGoal);

        Debug.Log("Rest");
        //que cambie la fsm a idle
    }
}
