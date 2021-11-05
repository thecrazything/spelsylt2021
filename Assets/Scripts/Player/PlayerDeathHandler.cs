using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour, IDeathHandler
{
    public void Hit()
    {
        Destroy(transform.parent.gameObject); // TODO Handle death
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
