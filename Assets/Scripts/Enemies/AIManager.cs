using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    private static AIManager _Instance;
    public Transform[] FleeNodes;

    private AIController[] _AIs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static AIManager GetInstance()
    {
        if (!_Instance)
        {
            _Instance = GameObject.Find("AIManager")?.GetComponent<AIManager>();
            if (!_Instance)
            {
                throw new MissingReferenceException("No AI manager exists");
            }
        }
        return _Instance;
    }

    public AIController[] GetAIs()
    {
        return _AIs;
    }
}
