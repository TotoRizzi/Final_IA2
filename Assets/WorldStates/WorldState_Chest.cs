using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldState_Chest : MonoBehaviour
{
    int currentWoodOnChest = 0;
    int maxWoodOnChest = 10;
    private void Start()
    {
        WorldState.instance.ModifyStringValues(WorldStateValues.currentChestWood, "Empty");
        WorldState.instance.ModifyFloatValues(WorldStateValues.currentStamina, 0f);

        WorldState.instance.currentChestWood = "Empty";
        WorldState.instance.currentStamina = 0;
    }

    public void SaveLoot(int value)
    {
        currentWoodOnChest += value;
        if (currentWoodOnChest >= maxWoodOnChest)
            WorldState.instance.currentChestWood = "Full";
        else
            WorldState.instance.currentChestWood = "Almost Full";
    }
}
