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
    public float RotatetionSpeed = 2.0f;
    private int _PatrolNodeIndex = 0;
    private bool _IsPatrolling = false;
    private Vector3 _LastPos;
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
        return new SequenceNode(new List<Node> { GetAdjustRotation(), GetIsAtPatrolDestNode(), GetNextTargetNode() });
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

    public ActionNode GetAdjustRotation()
    {
        return new ActionNode((possesable) =>
        {
            Transform origin = possesable.GetGameObject().transform;
            Vector3 curPos = origin.position;
            if (_LastPos == null)
            {
                _LastPos = possesable.GetGameObject().transform.position;
            }
            Vector3 dir = (_LastPos - curPos).normalized * -1;

            // Aim at player
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            origin.rotation = Quaternion.Slerp(origin.rotation, q, Time.deltaTime * RotatetionSpeed);

            _LastPos = curPos;
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
