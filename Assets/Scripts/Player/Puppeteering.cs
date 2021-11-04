using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puppeteering : MonoBehaviour
{
    private PlayerController _PlayerMovementInput;
    private Puppetable _CurrentPuppet;
    public GameObject debugTarget;
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
        if (!_CurrentPuppet)
        {
            MakeTargetPuppet(debugTarget);
        }
        else
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
            Destroy(_CurrentPuppet.gameObject);
            _CurrentPuppet = null;
            // TODO drop a corpse
        }
    }
}
