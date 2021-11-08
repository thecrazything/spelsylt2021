using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puppetable : MonoBehaviour
{
    private IPossesable _PupetMovement;
    private IController _Controller;
    // Start is called before the first frame update
    void Start()
    {
        _PupetMovement = GetComponent<IPossesable>();
        _Controller = GetComponent<IController>();
        if (_PupetMovement == null)
        {
            throw new MissingComponentException("Pupet needs IMovement component, otherwise player wont know how it controls.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IPossesable GetPossesable()
    {
        return _PupetMovement;
    }

    public IController GetController()
    {
        return _Controller;
    }
}
