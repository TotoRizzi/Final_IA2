using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Build : Action_Base
{
    List<System.Type> supportedGoals = new List<System.Type>() { typeof(Goal_Build) };

    public override List<System.Type> GetSupportedGoals()
    {
        return supportedGoals;
    }

    public override void OnTick()
    {
        worldState.currentStamina -= Time.deltaTime;

        worldState.bridgeBuilt = true;


        /*
        if(Vector3.Distance(maquina) < .1f)
        {
            worldState.intValues[WorldStateValues.bridgeBuilt] = true;
        }
        */
    }

    public override void OnActivated(Goal_Base _linkedGoal)
    {
        base.OnActivated(_linkedGoal);

        Debug.Log("Build");

        //Que busque el camino con A* a la maquina para activarla
    }
}