using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_SaveLoot : Action_Base
{
    List<System.Type> supportedGoals = new List<System.Type>() { typeof(Goal_SaveLoot) };

    public override List<System.Type> GetSupportedGoals()
    {
        return supportedGoals;
    }

    public override void OnTick()
    {
        worldState.currentStamina -= Time.deltaTime;

        var chest = FindObjectOfType<WorldState_Chest>();
        chest.SaveLoot(worldState.currentWoodOnMe);

        worldState.currentWoodOnMe = 0;

        /*
         
        if(Vector3.Distance(cofre) < .1f)
        {
            worldState.intValues[WorldStateValues.woodOnChest] += WorldStateValues.currentWoodOnMe;
            worldState.intValues[WorldStateValues.currentWoodOnMe] = 0;
        }
         
         
         */
    }

    public override void OnActivated(Goal_Base _linkedGoal)
    {
        base.OnActivated(_linkedGoal);

        Debug.Log("Save loot");

        //Que busque el camino con A* al cofre para guardarlo
    }
}
