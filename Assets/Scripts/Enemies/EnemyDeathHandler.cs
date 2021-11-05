using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathHandler : MonoBehaviour, IDeathHandler
{
    public void Hit()
    {
        Destroy(gameObject); 
        // TODO log enemy death
        // TODO drop a corpse
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
