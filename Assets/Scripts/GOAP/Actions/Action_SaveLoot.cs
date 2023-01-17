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
        float currentStamina = worldState.floatValues[WorldStateValues.currentStamina];
        currentStamina -= Time.deltaTime;

        worldState.floatValues[WorldStateValues.currentStamina] = currentStamina;

        //var chest = FindObjectOfType<Item_Chest>();
        //chest.SaveLoot();
        //worldState.intValues[WorldStateValues.currentWoodOnMe] = 0; // YA LO HACE EN EL ITEM

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

        Vector3 dirToGo = worldState.items[Items.Chest].transform.position; // NO SE POR QUE NO FUNCIONA
        //Que busque el camino con A* al cofre para guardarlo
        Debug.Log(dirToGo);
        _guy.SetPath(dirToGo);
    }
}
