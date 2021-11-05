using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puppeteering : MonoBehaviour
{
    private PlayerController _PlayerMovementInput;
    private Puppetable _CurrentPuppet;
    private GameObject _Target;
    public GameObject _PuppetFinder;
    // Start is called before the first frame update
    void Start()
    {
        _PlayerMovementInput = transform.parent.GetComponent<PlayerController>();
        if (!_PlayerMovementInput)
        {
            throw new MissingComponentException("Missing PlayerMovementInput");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Puppeteer()
    {
        // TODO check if puppet avalible, then run MakeTargetPuppet on that
        if (!_CurrentPuppet && _Target)
        {
            MakeTargetPuppet(_Target);
        }
        else if (_CurrentPuppet)
        {
            DropPuppet();
        }
    }

    private void MakeTargetPuppet(GameObject target)
    {
        Puppetable puppet = target.GetComponent<Puppetable>();
        if (!puppet)
        {
            return;
        }
        _PlayerMovementInput.SetPossessed(puppet.GetMovement());
        _CurrentPuppet = puppet;
        transform.parent.position = target.transform.position;
        transform.parent.parent = target.transform; // This might be a bad idea
    }

    public void DropPuppet()
    {
        if (_CurrentPuppet)
        {
            transform.parent.parent = null;
            IDeathHandler deathHandler = _CurrentPuppet.gameObject.GetComponent<IDeathHandler>();
            deathHandler?.Hit();
            _CurrentPuppet = null;
        }
    }

    public void SetTarget(GameObject target)
    {
        _Target = target;
    }

    public void NoTarget()
    {
        _Target = null;
    }
}
