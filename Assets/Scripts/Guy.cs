using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum PlayerActions { RUN, REST, SUCCESS }
public class Guy : MonoBehaviour
{
    [SerializeField] List<Node> _path = new List<Node>();
    [SerializeField] float _moveSpeed;
    [SerializeField] Transform _successPos;
    [SerializeField] Text _successTxt;

    Node _startingNode;
    Node _goalNode;
    EventFSM<PlayerActions> _fsm;
    private void Start()
    {
        var run = new State<PlayerActions>("RUN");
        var rest = new State<PlayerActions>("REST");
        var success = new State<PlayerActions>("SUCCESS");

        StateConfigurer.Create(run)
            .SetTransition(PlayerActions.REST, rest)
            .SetTransition(PlayerActions.SUCCESS, success)
            .Done();

        StateConfigurer.Create(rest)
            .SetTransition(PlayerActions.RUN, run)
            .Done();

        StateConfigurer.Create(success)
            .Done();

        #region PickUpAxe
        run.OnEnter += x =>
        {
            //Debug.Log("Run");
        };

        run.OnUpdate += GoTo;

        #endregion

        #region ChopTree

        rest.OnEnter += x =>
        {
            //Debug.Log("Rest");
        };

        rest.OnUpdate += GoTo;

        #endregion

        #region Success

        success.OnEnter += x =>
        {
            //Debug.Log("Success");
            SetPath(_successPos.position);
        };

        success.OnUpdate += () =>
        {
            GoTo();
            Success();
        };

        #endregion

        _fsm = new EventFSM<PlayerActions>(run);
    }

    private void Success()
    {
        if (_path.Count <= 0)
            _successTxt.text = "SUCCESS";
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
    public void SendInputToFsm(PlayerActions nextStep)
    {
        _fsm.SendInput(nextStep);
    }
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
