using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField] LayerMask _itemMask, _groundMask;

    public static GameManager Instance;
    public LayerMask ItemMask { get { return _itemMask; } }
    public LayerMask GroundMask { get { return _groundMask; } }
    private void Awake()
    {
        Instance = this;
    }
}
