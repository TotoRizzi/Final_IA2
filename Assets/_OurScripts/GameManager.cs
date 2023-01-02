using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] LayerMask _wallLayer;
    public LayerMask WallLayer { get { return _wallLayer; } }
    void Awake()
    {
        Instance = this;
    }
}
