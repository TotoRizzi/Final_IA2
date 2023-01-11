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
    public Dictionary<WorldStateValues, int> intValues = new Dictionary<WorldStateValues, int>();
    public Dictionary<WorldStateValues, float> floatValues = new Dictionary<WorldStateValues, float>();
    public Dictionary<WorldStateValues, string> stringValues = new Dictionary<WorldStateValues, string>();

    public float currentStamina = 0f;
    public string currentChestWood = "Empty";
    public int currentWoodOnMe = 0;
    public bool hasAxe = false;
    public bool bridgeBuilt = false;

    private void Awake()
    {
        instance = this;
    }

    public void ModifyBoolValues(WorldStateValues key, bool value)
    {
        if (boolValues.ContainsKey(key)) boolValues[key] = value;
        else boolValues.Add(key, value);
    }

    public void ModifyFloatValues(WorldStateValues key, float value)
    {
        if (floatValues.ContainsKey(key)) floatValues[key] = value;
        else floatValues.Add(key, value);
    }

    public void ModifyIntValues(WorldStateValues key, int value)
    {
        if (intValues.ContainsKey(key)) intValues[key] = value;
        else intValues.Add(key, value);
    }

    public void ModifyStringValues(WorldStateValues key, string value)
    {
        if (stringValues.ContainsKey(key)) stringValues[key] = value;
        else stringValues.Add(key, value);
    }
}
