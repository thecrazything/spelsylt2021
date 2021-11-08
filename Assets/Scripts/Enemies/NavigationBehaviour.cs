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
    public float _NodePatrolTimeout = 0;
    private int _PatrolNodeIndex = 0;
    private bool _IsPatrolling = false;

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
            return NodeStates.Success;
        });
    }

    public SequenceNode GetPatrolNode()
    {
        return new SequenceNode(new List<Node> { GetIsAtPatrolDestNode(), GetNextTargetNode() });
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
            _NavMeshAgent.destination = PatrolNodes[_PatrolNodeIndex].position;
            _PatrolNodeIndex++;
            return NodeStates.Success;
        });
    }

    public ActionNode GetIsAtPatrolDestNode()
    {
        return new ActionNode((possesable) =>
        {
            if (!_IsPatrolling)
            {
                return NodeStates.Success;
            }
            if (!_NavMeshAgent.pathPending && 
                _NavMeshAgent.remainingDistance <= _NavMeshAgent.stoppingDistance && 
                (!_NavMeshAgent.hasPath || _NavMeshAgent.velocity.sqrMagnitude == 0f))
            {
                return NodeStates.Success;
            }
            return NodeStates.Failure;
        });
    }
}
