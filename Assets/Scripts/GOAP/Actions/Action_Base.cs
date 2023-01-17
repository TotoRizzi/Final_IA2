using System.Collections.Generic;
using UnityEngine;
public class Action_Base : MonoBehaviour
{
    protected WorldState worldState;
    protected Goal_Base LinkedGoal;
    protected Guy _guy;
    [SerializeField] protected float cost = 0;

    void Awake()
    {
        _guy = GetComponent<Guy>();
        worldState = FindObjectOfType<WorldState>();
        // Sensors = GetComponent<AwarenessSystem>();
    }

    public virtual List<System.Type> GetSupportedGoals()
    {
        return null;
    }

    public virtual float GetCost()
    {
        return cost;
    }

    public virtual void OnActivated(Goal_Base _linkedGoal)
    {
        LinkedGoal = _linkedGoal;
    }

    public virtual void OnDeactivated()
    {
        LinkedGoal = null;
    }

    public virtual void OnTick()
    {

    }
}