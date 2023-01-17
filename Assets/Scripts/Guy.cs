using System.Collections.Generic;
using UnityEngine;
public enum PlayerActions { BUILD, CHOP_TREE, PICK_UP_AXE, REST, SAVE_LOOT }
public class Guy : MonoBehaviour
{
    [SerializeField] List<Node> _path = new List<Node>();
    [SerializeField] float _moveSpeed;

    Node _startingNode;
    Node _goalNode;
    EventFSM<PlayerActions> _fsm;
    WorldState _worldState;
    private void Start()
    {
        var build = new State<PlayerActions>("BUILD");
        var chopTree = new State<PlayerActions>("CHOP TREE");
        var pickUpAxe = new State<PlayerActions>("PICK UP AXE");
        var rest = new State<PlayerActions>("REST");
        var saveLoot = new State<PlayerActions>("SAVE LOOT");

        StateConfigurer.Create(pickUpAxe)
            .SetTransition(PlayerActions.CHOP_TREE, pickUpAxe)
            .SetTransition(PlayerActions.REST, rest)
            .Done();

        StateConfigurer.Create(chopTree)
            .SetTransition(PlayerActions.SAVE_LOOT, saveLoot)
            .SetTransition(PlayerActions.REST, rest)
            .Done();

        StateConfigurer.Create(saveLoot)
            .SetTransition(PlayerActions.CHOP_TREE, chopTree)
            .SetTransition(PlayerActions.REST, rest)
            .SetTransition(PlayerActions.BUILD, build)
            .Done();

        StateConfigurer.Create(rest)
            .SetTransition(PlayerActions.CHOP_TREE, chopTree)
            .SetTransition(PlayerActions.BUILD, build)
            .SetTransition(PlayerActions.SAVE_LOOT, saveLoot)
            .SetTransition(PlayerActions.PICK_UP_AXE, pickUpAxe)
            .Done();

        StateConfigurer.Create(build).Done();

        _worldState = WorldState.instance;

        #region PickUpAxe
        pickUpAxe.OnEnter += x =>
        {
            Debug.Log("Pick Up Axe");
            SetPath(_worldState.items[Items.Axe].transform.position);
        };

        pickUpAxe.OnUpdate += GoTo;

        #endregion

        #region ChopTree

        chopTree.OnEnter += x =>
        {
            Debug.Log("Chop Tree");
        };

        chopTree.OnUpdate += GoTo;

        #endregion

        #region SaveLoot

        saveLoot.OnEnter += x =>
        {
            Debug.Log("Save Loot");
        };

        saveLoot.OnUpdate += GoTo;

        #endregion

        #region Build

        build.OnEnter += x =>
        {
            Debug.Log("Build");
        };

        build.OnUpdate += GoTo;

        #endregion

        _fsm = new EventFSM<PlayerActions>(pickUpAxe);
    }
    void Update()
    {
        _fsm.Update();
    }
    void GoTo()
    {
        if (_path.Count <= 0) return;

        Vector3 dir = _path[0].transform.position - transform.position;
        dir.y = 0;
        if (dir.magnitude < 0.1f)
            _path.RemoveAt(0);

        transform.position += dir.normalized * _moveSpeed * Time.deltaTime;
    }
    public void SetPath(Vector3 goalPos)
    {
        if (_path == null) return;
        AStar aStar = new AStar();
        StartCoroutine(aStar.ConstructPathAStar(GetStartingNode(), GetGoalNode(goalPos), x => _path = x));
    }

    //public void SendInputToFsm(PlayerActions nextStep)
    //{
    //    _fsm.SendInput(nextStep);
    //}
    Node GetStartingNode()
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
    Node GetGoalNode(Vector3 goalPos)
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
