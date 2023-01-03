using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class WorkerPlanner : MonoBehaviour
{
    private readonly List<Tuple<Vector3, Vector3>> _debugRayList = new List<Tuple<Vector3, Vector3>>();

    private void Start()
    {
        StartCoroutine(Plan());
    }

    private void Check(Dictionary<string, bool> state, ItemType type)
    {

        var items = Navigation.instance.AllItems(); //Todos los items
        var inventories = Navigation.instance.AllInventories(); //Todos los items que tiene el Pj
        var floorItems = items.Except(inventories);//devuelve Todos los items pero removiendo los que tiene el Pj
        var item = floorItems.FirstOrDefault(x => x.type == type);//El primer item que sea del mismo tipo que le pasa por parametro
        var here = transform.position;
        state["accessible" + type.ToString()] = item != null && Navigation.instance.Reachable(here, item.transform.position, _debugRayList);

        var inv = inventories.Any(x => x.type == type);
        state["otherHas" + type.ToString()] = inv;

        state["dead" + type.ToString()] = false;
    }

    private IEnumerator Plan()
    {
        yield return new WaitForSeconds(0.2f);

        var observedState = new Dictionary<string, bool>();

        var nav = Navigation.instance;
        var floorItems = nav.AllItems();
        var inventory = nav.AllInventories();
        var everything = nav.AllItems().Union(nav.AllInventories());// .Union() une 2 colecciones sin agregar duplicados(eso incluye duplicados en la misma coleccion)

        //Chequeo los booleanos para cada Item, generando mi modelo de mundo (mi diccionario de bools) en ObservedState
        Check(observedState, ItemType.Loot);
        Check(observedState, ItemType.Chest);
        Check(observedState, ItemType.Axe);
        //Check(observedState, ItemType.PastaFrola);
        //Check(observedState, ItemType.Door);

        var actions = CreatePossibleActionsList();

        //Seteas el estado del mundo cuando se crea
        GoapState initial = new GoapState();
        initial.worldState = new WorldState()
        {
            playerHP = 10,
            stamina = 10,
            resourses = 0,
            hasAxe = false,
            chestResourses = "",
            bridgeBuilt = false,
            values = new Dictionary<string, bool>()
        };

        initial.worldState.values = observedState; //le asigno los valores actuales, conseguidos antes
        initial.worldState.values["doorOpen"] = false; //agrego el bool "doorOpen"

        foreach (var item in initial.worldState.values)
        {
            Debug.Log(item.Key + " ---> " + item.Value);
        }

        GoapState goal = new GoapState();
        goal.worldState.values["built" + ItemType.Bridge.ToString()] = true;

        Func<GoapState, float> heuristc = (curr) =>
        {
            int count = 0;
            string key = "built" + ItemType.Bridge.ToString();
            if (!curr.worldState.values.ContainsKey(key) || !curr.worldState.values[key])
                count++;
            if (curr.worldState.playerHP <= 45)
                count++;
            return count;
        };

        Func<GoapState, bool> objectice = (curr) =>
        {
            string key = "built" + ItemType.Bridge.ToString();
            return curr.worldState.values.ContainsKey(key) && curr.worldState.values[key]
                   && curr.worldState.playerHP > 45;
        };




        var actDict = new Dictionary<string, ActionEntity>() {
              { "Kill"  , ActionEntity.Kill }
            , { "Pickup", ActionEntity.PickUp }
            , { "Open"  , ActionEntity.Open }
        };

        var plan = Goap.Execute(initial, null, objectice, heuristc, actions);

        if (plan == null)
            Debug.Log("Couldn't plan");
        else
        {
            GetComponent<Guy>().ExecutePlan(
                plan
                .Select(a =>
                {
                    Item i2 = everything.FirstOrDefault(i => i.type == a.item);
                    if (actDict.ContainsKey(a.Name) && i2 != null)
                    {
                        return Tuple.Create(actDict[a.Name], i2);
                    }
                    else
                    {
                        return null;
                    }
                }).Where(a => a != null)
                .ToList()
            );
        }
    }

    private List<GoapAction> CreatePossibleActionsList()
    {
        return new List<GoapAction>()
        {
              new GoapAction("ChopChop")
                .SetCost(2f)
                .SetItem(ItemType.Loot)

                .Pre(x =>  x.worldState.resourses < 6 &&
                           x.worldState.hasAxe)

                .Effect(x =>
                {
                    x.worldState.resourses += 5;
                    return x;
                })
                
            , new GoapAction("SaveLoot")
                .SetCost(2f)
                .SetItem(ItemType.Chest)

                .Pre(x =>  x.worldState.resourses >= 5)

                .Effect(x =>
                {
                    x.worldState.chestResourses += 5;
                    x.worldState.resourses -= 5;
                    return x;
                })

            , new GoapAction("PickUpAxe")
                .SetCost(1f)
                .SetItem(ItemType.Entity)

                .Pre(x => !x.worldState.hasAxe)

                .Effect(x =>
                {
                    x.worldState.hasAxe = true;
                    return x;
                })

            , new GoapAction("BuildBridge")
                .SetCost(10f)
                .SetItem(ItemType.Entity)

                .Pre(x =>  x.worldState.chestResourses == "Full" &&
                          !x.worldState.bridgeBuilt)

                .Effect(x =>
                {
                    x.worldState.chestResourses = "Empty";
                    x.worldState.bridgeBuilt = true;
                    return x;
                })


        };
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        foreach (var t in _debugRayList)
        {
            Gizmos.DrawRay(t.Item1, (t.Item2 - t.Item1).normalized);
            Gizmos.DrawCube(t.Item2 + Vector3.up, Vector3.one * 0.2f);
        }
    }
}
