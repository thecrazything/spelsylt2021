using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Extend this for different AI types
/// </summary>
public class AIController : MonoBehaviour, IController
{
    private IPossesable _ActivePossesable;
    private IPossesable _DefaultPossesable;
    private IAIBehaviour _AIBehaviour;
    private NavMeshAgent _NavMeshAgent;
    private NavigationBehaviour _NavBehaviour;
    public PlayerDetectorBehaviour PlayerDetectorBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        _ActivePossesable = GetComponent<IPossesable>();
        _DefaultPossesable = _ActivePossesable;
        _NavMeshAgent = GetComponent<NavMeshAgent>();
        _NavBehaviour = GetComponent<NavigationBehaviour>();

        if (_DefaultPossesable == null)
        {
            throw new MissingComponentException("Possesable requird.");
        }

        _AIBehaviour = GetComponent<IAIBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_ActivePossesable != null)
        {
            if (_NavMeshAgent)
            {
                _NavMeshAgent.enabled = true;
            }
            _AIBehaviour?.GetRootNode().Evaluate(_ActivePossesable);
        } 
        else
        {
            if (_NavMeshAgent)
            {
                _NavMeshAgent.isStopped = true;
                _NavMeshAgent.enabled = false;
            }
        }
    }

    public void SetPossessed(IPossesable possesable)
    {
        _ActivePossesable?.OnUnPossess();
        _ActivePossesable = possesable;
        _ActivePossesable?.OnPossess(this);
    }

    public void ResetPossessed()
    {
        if (_ActivePossesable != null)
        {
            _ActivePossesable.OnUnPossess();
            _ActivePossesable = null;
        }
    }

    public void SetAlerted(Vector3 playerPos)
    {
        PlayerDetectorBehaviour.SetAlerted();
        _NavBehaviour.SetLastPlayerLocation(playerPos);
    }

    public void ResetPossessed(Vector3 newPos)
    {
        ResetPossessed();
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
