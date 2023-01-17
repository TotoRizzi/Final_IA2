using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Chest : Item
{
    int currentWoodOnChest = 0;
    int maxWoodOnChest = 10;
    public override void Start()
    {
        base.Start();
        worldState.stringValues.Add(WorldStateValues.currentChestWood, "Empty");
        WorldState.instance.floatValues.Add(WorldStateValues.currentStamina, 0.0f);
    }

    public void SaveLoot()
    {
        currentWoodOnChest += 5;

        if (currentWoodOnChest >= maxWoodOnChest)
            WorldState.instance.stringValues[WorldStateValues.currentChestWood] = "Full";
        else
            WorldState.instance.stringValues[WorldStateValues.currentChestWood] = "Almost Full";
    }
    public override void ActionOnTrigger()
    {
        SaveLoot();
        worldState.intValues[WorldStateValues.currentWoodOnMe] = 0;
    }
}
