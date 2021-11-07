using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Extend this for different AI types
/// </summary>
public class AIController : MonoBehaviour, IController
{
    private IPossesable _ActivePossesable;
    private IPossesable _DefaultPossesable;
    private IAIBehaviour _AIBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        _ActivePossesable = GetComponent<IPossesable>();
        _DefaultPossesable = _ActivePossesable;

        if (_DefaultPossesable == null)
        {
            throw new MissingComponentException("Possesable requird.");
        }

        _AIBehaviour = GetComponent<IAIBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        _AIBehaviour?.GetRootNode().Evaluate(_ActivePossesable);
    }

    public void SetPossessed(IPossesable possesable)
    {
        _ActivePossesable?.OnUnPossess();
        _ActivePossesable = possesable;
        _ActivePossesable?.OnPossess(this);
    }

    public void ResetPossessed()
    {
        if (_ActivePossesable != _DefaultPossesable)
        {
            _ActivePossesable?.OnUnPossess();
            _ActivePossesable = _DefaultPossesable;
            _ActivePossesable?.OnPossess(this);
        }
    }
}
