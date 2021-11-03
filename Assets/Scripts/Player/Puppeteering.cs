using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puppeteering : MonoBehaviour
{
    private PlayerMovementInput _PlayerMovementInput;
    private Puppetable _CurrentPuppet;
    public GameObject debugTarget;
    // Start is called before the first frame update
    void Start()
    {
        _PlayerMovementInput = GetComponent<PlayerMovementInput>();
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
        _PlayerMovementInput.SetMovement(puppet.GetMovement());
        _CurrentPuppet = puppet;
        transform.position = target.transform.position;
        transform.parent = target.transform; // This might be a bad idea
        // TODO hide player
        // TODO disable player collision
        // TODO disable AI things
    }

    private void DropPuppet()
    {
        _PlayerMovementInput.ResetMovement();
        Destroy(_CurrentPuppet.gameObject);
        _CurrentPuppet = null;
        transform.parent = null;
        // TODO unhide player
        // TODO drop a corpse
    }
}
