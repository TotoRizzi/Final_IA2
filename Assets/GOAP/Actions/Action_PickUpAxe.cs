using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_PickUpAxe : Action_Base
{
    List<System.Type> supportedGoals = new List<System.Type>() { typeof(Goal_PickUpAxe) };

    public override List<System.Type> GetSupportedGoals()
    {
        return supportedGoals;
    }

    public override void OnTick()
    {
        worldState.currentStamina -= Time.deltaTime;

        worldState.hasAxe = true;


        /*
        if (Vector3.Distance(Hacha) < .1f)
        {
            worldState.intValues[WorldStateValues.HasAxe] = true;
        }
        */
    }

    public override void OnActivated(Goal_Base _linkedGoal)
    {
        base.OnActivated(_linkedGoal);

        Debug.Log("Has Axe");

        //Que busque el camino con A* al hacha para agarrala
    }
}
