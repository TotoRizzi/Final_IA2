using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Bridge : Item
{
    [SerializeField] GameObject bridge;
    public override void Start()
    {
        base.Start();
        worldState.boolValues.Add(WorldStateValues.bridgeBuilt, false);
    }
    public override void ActionOnTrigger()
    {
        bridge.SetActive(true);
        worldState.boolValues[WorldStateValues.bridgeBuilt] = true;
    }
}
