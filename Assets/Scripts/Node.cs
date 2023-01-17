using System.Collections.Generic;
using UnityEngine;
public class Node : MonoBehaviour
{
    public List<Node> neighbors = new List<Node>();
    public int cost = 1;

    [SerializeField] LayerMask _nodeMask;
    [SerializeField] float _radius;
    private void Start()
    {
        Collider[] nodes = Physics.OverlapSphere(transform.position, _radius, _nodeMask);
        foreach (var item in nodes)
        {
            if (item == null) break;
            if (item.GetComponent<Node>() != this)
            {
                if (!IsBlocked(item.transform.position, .1f, GameManager.Instance.GroundMask))
                    Destroy(item.gameObject);
                if (InSight(transform.position, item.transform.position, GameManager.Instance.ItemMask)) continue;

                neighbors.Add(item.GetComponent<Node>());
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
