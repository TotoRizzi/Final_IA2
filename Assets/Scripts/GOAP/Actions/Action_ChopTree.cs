using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_ChopTree : Action_Base
{
    List<System.Type> supportedGoals = new List<System.Type>() { typeof(Goal_ChopTree) };

    public override List<System.Type> GetSupportedGoals()
    {
        return supportedGoals;
    }

    public override void OnTick()
    {
        float currentStamina = worldState.floatValues[WorldStateValues.currentStamina];
        currentStamina -= Time.deltaTime;

        worldState.floatValues[WorldStateValues.currentStamina] = currentStamina;
        //worldState.intValues[WorldStateValues.currentWoodOnMe] = 5;  //YA LO HACE EN EL ITEM

        /*
        if(Vector3.Distance(arbol) < .1f)
        {
            worldState.intValues[WorldStateValues.currentWoodOnMe] += 5;
        }
        */
    }

    public override void OnActivated(Goal_Base _linkedGoal)
    {
        base.OnActivated(_linkedGoal);

        Debug.Log("Chop Tree");

        Vector3 dirToGo = worldState.items[Items.Tree].transform.position;

        //Que busque el camino con A* al arbol para cortarlo
        _guy.SetPath(dirToGo);
    }
}