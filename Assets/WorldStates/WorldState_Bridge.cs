using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldState_Bridge : MonoBehaviour
{
    private void Start()
    {
        WorldState.instance.bridgeBuilt = false;
    }
}
