using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guy : MonoBehaviour
{
    [SerializeField] Node _startingNode;
    [SerializeField] Node _goalNode;
    [SerializeField] List<Node> _path = new List<Node>();

    Camera _mainCamera;
    private void Start()
    {
        _mainCamera = Camera.main;
    }
    void Update()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out RaycastHit hit, GameManager.Instance.GroundMask))
            SetPath(new Vector3(hit.point.x, 0, hit.point.z));

        if (_path.Count <= 0) return;

        Vector3 dir = _path[0].transform.position - transform.position;
        dir.y = 0;
        if (dir.magnitude < 0.1f)
            _path.RemoveAt(0);
        transform.position += dir.normalized * 5 * Time.deltaTime;
    }
    void SetPath(Vector3 goalPos)
    {
        AStar aStar = new AStar();
        if (_path == null) return;
        StartCoroutine(aStar.ConstructPathAStar(GetStartingNode(), GetGoalNode(goalPos), x => _path = x));
    }
    public Node GetStartingNode()
    {
        Node[] allNodes = FindObjectsOfType<Node>();
        float minValue = 0;
        for (int i = 0; i < allNodes.Length; i++)
        {
            float distance = Vector3.Distance(allNodes[i].transform.position, transform.position);
            if (minValue == 0)
            {
                minValue = distance;
                _startingNode = allNodes[i];
            }
            else if (distance < minValue)
            {
                minValue = distance;
                _startingNode = allNodes[i];
            }
        }
        return _startingNode;
    }
    public Node GetGoalNode(Vector3 goalPos)
    {
        Node[] allNodes = FindObjectsOfType<Node>();
        float minValue = 0;
        for (int i = 0; i < allNodes.Length; i++)
        {
            float distance = Vector3.Distance(goalPos, allNodes[i].transform.position);
            if (minValue == 0)
            {
                minValue = distance;
                _goalNode = allNodes[i];
            }
            else if (distance < minValue)
            {
                minValue = distance;
                _goalNode = allNodes[i];
            }
        }
        return _goalNode;
    }
}
