using System.Collections.Generic;
using UnityEngine;
public class Node : MonoBehaviour
{
    public List<Node> neighbors = new List<Node>();

    [SerializeField] LayerMask _nodeMask;
    private void Start()
    {
        Collider[] nodes = Physics.OverlapSphere(transform.position, 3.5f, _nodeMask);
        foreach (var item in nodes)
        {
            if (item == null) break;
            if (item.GetComponent<Node>() != this)
            {
                if (IsBlocked(item.transform.position, .1f, GameManager.Instance.WallLayer))
                    Destroy(item.gameObject);
                else if (InSight(transform.position, item.transform.position, GameManager.Instance.WallLayer)) continue;
                else neighbors.Add(item.GetComponent<Node>());
            }
        }
    }
    bool InSight(Vector3 start, Vector3 end, LayerMask mask)
    {
        return Physics.Raycast(start, end - start, Vector3.Distance(start, end), mask);
    }
    bool IsBlocked(Vector3 start, float radius, LayerMask mask)
    {
        return Physics.CheckSphere(start, radius, mask);
    }
}
