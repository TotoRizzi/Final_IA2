using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    Node[,] _grid;
    [SerializeField] int _width;
    [SerializeField] int _height;
    [SerializeField] float _separation;
    [SerializeField] float _offsetX;
    [SerializeField] float _offsetZ;
    [SerializeField] Node _nodePrefab;

    static Grid Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    void Start()
    {
        _grid = new Node[_width, _height];
        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _height; z++)
            {
                Node node = Instantiate(_nodePrefab);
                node.transform.SetParent(transform);
                node.name += $"{x}{z}";
                _grid[x, z] = node;
                node.transform.position = new Vector3((x * node.transform.localScale.x * _separation) + _offsetX, transform.position.y, (z * node.transform.localScale.z * _separation) + _offsetZ);
            }
        }
    }
}
