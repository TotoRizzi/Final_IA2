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
        float currentStamina = worldState.floatValues[WorldStateValues.currentStamina];
        currentStamina -= Time.deltaTime;

        worldState.floatValues[WorldStateValues.currentStamina] = currentStamina;
        //worldState.boolValues[WorldStateValues.bridgeBuilt] = true;   //YA LO HACE EN EL ITEM

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

        Vector3 dirToGo = worldState.items[Items.Bridge].transform.position;

        //Que busque el camino con A* a la maquina para activarla
        _guy.SetPath(dirToGo);
    }
}