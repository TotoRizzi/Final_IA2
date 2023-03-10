using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar
{
    public IEnumerator ConstructPathAStar(Node startingNode, Node goalNode, System.Action<List<Node>> callBack)
    {
        if (startingNode == null || goalNode == null) yield break;

        PriorityQueue frontier = new PriorityQueue();
        frontier.Put(startingNode, 0);

        Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();
        cameFrom.Add(startingNode, null);

        Dictionary<Node, float> costSoFar = new Dictionary<Node, float>();
        costSoFar.Add(startingNode, 0);

        List<Node> path = new List<Node>();
        while (frontier.Count > 0)
        {
            Node current = frontier.Get();

            if (current == goalNode)
            {
                Node nodeToAdd = current;

                while (nodeToAdd != null)
                {
                    path.Add(nodeToAdd);
                    nodeToAdd = cameFrom[nodeToAdd];
                }
                path.Reverse();
            }

            foreach (var next in current.neighbors)
            {
                if (next == null) continue;
                float dist = Vector3.Distance(goalNode.transform.position, next.transform.position);

                float newCost = costSoFar[current] + next.cost;
                float priority = newCost + dist;
                if (!cameFrom.ContainsKey(next))
                {
                    frontier.Put(next, priority);
                    cameFrom.Add(next, current);
                    costSoFar.Add(next, newCost);
                }
                else
                {
                    if (newCost < costSoFar[next])
                    {
                        frontier.Put(next, priority);
                        cameFrom[next] = current;
                        costSoFar[next] = newCost;
                    }
                }

            }
        }
        callBack(path);
        yield return null;
    }
}
public class PriorityQueue
{
    Dictionary<Node, float> _allNodes = new Dictionary<Node, float>();

    public int Count { get { return _allNodes.Count; } }

    public void Put(Node node, float cost)
    {
        if (_allNodes.ContainsKey(node)) _allNodes[node] = cost;
        else _allNodes.Add(node, cost);
    }

    public Node Get()
    {
        Node node = null;
        float lowestValue = Mathf.Infinity;

        foreach (var item in _allNodes)
        {
            if (item.Value < lowestValue)
            {
                lowestValue = item.Value;
                node = item.Key;
            }
        }
        _allNodes.Remove(node);
        return node;
    }
}