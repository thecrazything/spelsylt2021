using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavigationBehaviour : MonoBehaviour
{
    NavMeshAgent _NavMeshAgent;
    public Transform[] PatrolNodes;
    public float NodePauseTime = 1.0f;
    private float _NodePatrolTimeout = 0;
    public float RotationSpeed = 2.0f;
    private int _PatrolNodeIndex = 0;
    private bool _IsPatrolling = false;
    private bool _IsFleeing = false;
    private Vector3 _LastPos;
    private Vector3 _LastLastPos;
    private Vector3 _PlayerLastPos;

    // Start is called before the first frame update
    void Start()
    {
        _NavMeshAgent = GetComponent<NavMeshAgent>();
        if (!_NavMeshAgent)
        {
            throw new MissingComponentException("Missing NavMeshAgent");
        }
        _NavMeshAgent.updateRotation = false;
        _NavMeshAgent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public ActionNode GetFollowPlayerNode()
    {
        return new ActionNode((possesable) =>
        {
            GameObject player = GameManager.GetInstance().GetPlayer();
            if (!player)
            {
                return NodeStates.Failure;
            }
            _IsPatrolling = false;
            _NavMeshAgent.SetDestination(player.transform.position);
            _PlayerLastPos = player.transform.position;
            return NodeStates.Success;
        });
    }

    public ActionNode GetGoToLastPlayerLocationNode()
    {
        return new ActionNode((possesable) =>
        {
            if (_PlayerLastPos == null)
            {
                return NodeStates.Failure;
            }
            _IsPatrolling = false;
            _NavMeshAgent.SetDestination(_PlayerLastPos);
            return NodeStates.Success;
        });
    }

    public SequenceNode GetPatrolNode()
    {
        Node shouldGetNextTarget = new SelectorNode(new List<Node> { new InverterNode(GetIsPatrollingNode()), GetIsAtDestNode() });
        return new SequenceNode(new List<Node> { GetAdjustRotation(), shouldGetNextTarget, GetNextTargetNode() });
    }

    public void SetLastPlayerLocation(Vector3 playerPos)
    {
        _PlayerLastPos = playerPos;
    }

    public SequenceNode GetFleeSequenceNode()
    {
        Node shouldGetNewFleeNode = new SelectorNode(new List<Node> { new InverterNode(GetIsFleeingNode()), GetIsAtDestNode() });
        return new SequenceNode(new List<Node> { GetAdjustRotation(), shouldGetNewFleeNode, GetFleeNode() });
    }

    public ActionNode GetNextTargetNode()
    {
        return new ActionNode((posseable) =>
        {
            if (PatrolNodes == null || PatrolNodes.Length == 0)
            {
                return NodeStates.Failure;
            }
            _IsPatrolling = true;
            if (_PatrolNodeIndex > PatrolNodes.Length - 1)
            {
                _PatrolNodeIndex = 0;
            }
            _NavMeshAgent.SetDestination(PatrolNodes[_PatrolNodeIndex].position);
            _PatrolNodeIndex++;
            return NodeStates.Success;
        });
    }

    public ActionNode GetAdjustRotation()
    {
        return new ActionNode((possesable) =>
        {
            Transform origin = possesable.GetGameObject().transform;
            Vector3 curPos = origin.position;
            if (_LastPos == null)
            {
                _LastPos = curPos;
            }
            Vector3 dir = (_LastLastPos == null ? _LastPos : _LastLastPos - curPos).normalized * -1;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            origin.rotation = Quaternion.Slerp(origin.rotation, q, Time.deltaTime * RotationSpeed);

            _LastLastPos = _LastPos;
            _LastPos = curPos;
            return NodeStates.Success;
        });
    }

    public ActionNode GetIsPatrollingNode()
    {
        return new ActionNode((possesable) =>
        {
            return _IsPatrolling ? NodeStates.Success : NodeStates.Failure;
        });
    }

    public ActionNode GetIsFleeingNode()
    {
        return new ActionNode((possesable) =>
        {
            return _IsFleeing ? NodeStates.Success : NodeStates.Failure;
        });
    }

    public ActionNode GetIsAtDestNode()
    {
        return new ActionNode((possesable) =>
        {
            if (!_NavMeshAgent.pathPending && 
                _NavMeshAgent.remainingDistance <= _NavMeshAgent.stoppingDistance && 
                (!_NavMeshAgent.hasPath || _NavMeshAgent.velocity.sqrMagnitude == 0f))
            {
                return NodeStates.Success;
            }
            return NodeStates.Failure;
        });
    }

    public ActionNode GetFleeNode()
    {
        return new ActionNode((possesable) =>
        {
            _IsPatrolling = false;
            _IsFleeing = true;
            Transform[] nodesToFleeTo = AIManager.GetInstance().FleeNodes;
            if (nodesToFleeTo.Length == 0)
            {
                return NodeStates.Failure;
            }
            Transform furthest = null;
            float furthestDistance = 0;
            GameObject player = GameManager.GetInstance().GetPlayer();
            if (!player)
            {
                return NodeStates.Failure;
            }
            Vector3 origin = player.transform.position;
            foreach(Transform node in nodesToFleeTo)
            {
                float pos = Vector3.Distance(origin, node.position);
                if (pos > furthestDistance)
                {
                    furthest = node;
                    furthestDistance = pos;
                }
            }

            _NavMeshAgent.SetDestination(furthest.position);
            return NodeStates.Success;
        });
    }
}
