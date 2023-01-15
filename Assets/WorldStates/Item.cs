using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Items
{
    Axe,
    Chest,
    Tree,
    Bridge
}
public class Item : MonoBehaviour
{
    [SerializeField] Items _myItem;
    protected WorldState worldState;


    public virtual void Start()
    {
        worldState = WorldState.instance;
        worldState.items.Add(_myItem, this);
    }
    public virtual void ActionOnTrigger()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        ActionOnTrigger();
    }
}
