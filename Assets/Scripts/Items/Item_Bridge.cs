using System.Collections;
using UnityEngine;
public class Item_Bridge : Item
{
    [SerializeField] GameObject bridge;
    public override void Start()
    {
        base.Start();
        worldState.boolValues.Add(WorldStateValues.bridgeBuilt, false);
        StartCoroutine(SetActive());
    }
    public override void ActionOnTrigger()
    {
        bridge.SetActive(true);
        worldState.boolValues[WorldStateValues.bridgeBuilt] = true;
        Guy guy = FindObjectOfType<Guy>();
        guy.SendInputToFsm(PlayerActions.SUCCESS);
    }
    IEnumerator SetActive()
    {
        yield return new WaitForSeconds(.1f);
        bridge.SetActive(false);
    }
}
