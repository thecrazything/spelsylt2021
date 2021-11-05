using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puppeteering : MonoBehaviour
{
    private PlayerController _PlayerController;
    private Puppetable _CurrentPuppet;
    private GameObject _Target;
    private float _PuppetMaxtime = 10f; // TODO adjust to reasonable length
    private float _PuppetTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        _PlayerController = transform.GetComponent<PlayerController>();
        if (!_PlayerController)
        {
            throw new MissingComponentException("Missing PlayerController");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_CurrentPuppet)
        {
            _PuppetTimer += Time.deltaTime;
            if (_PuppetTimer >= _PuppetMaxtime)
            {
                DropPuppet();
            }
        }
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
        _PuppetTimer = 0;
        Puppetable puppet = target.GetComponent<Puppetable>();
        if (!puppet)
        {
            return;
        }
        _PlayerController.SetPossessed(puppet.GetMovement());
        _CurrentPuppet = puppet;
        transform.position = target.transform.position;
        transform.rotation = target.transform.rotation * Quaternion.Euler(0,0,180);
        transform.parent = target.transform; // This might be a bad idea
    }

    public void DropPuppet()
    {
        if (_CurrentPuppet)
        {
            _PlayerController.ResetPossessed();
            transform.parent = null;
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
