using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WorldStateValues
{
    currentWoodOnMe,
    currentChestWood,
    currentStamina,
    hasAxe,
    bridgeBuilt
}
public class WorldState : MonoBehaviour
{
    public static WorldState instance;
    public Dictionary<WorldStateValues, bool> boolValues = new Dictionary<WorldStateValues, bool>();
    public Dictionary<WorldStateValues, float> floatValues = new Dictionary<WorldStateValues, float>();
    public Dictionary<WorldStateValues, int> intValues = new Dictionary<WorldStateValues, int>();
    public Dictionary<WorldStateValues, string> stringValues = new Dictionary<WorldStateValues, string>();

    public Dictionary<Items, Item> items = new Dictionary<Items, Item>();

    private void Awake()
    {
        instance = this;
    }
}
