using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    private static AIManager _Instance;
    public Transform[] FleeNodes;

    private AIController[] _AIs;
    private AIController[] _ScientistAI;

    // Start is called before the first frame update
    void Start()
    {
        if (_AIs == null)
        {
            LoadAllAI();
        }
        var nodes = GameObject.FindGameObjectsWithTag("FleeNode");

        FleeNodes = new Transform[nodes.Length];
        var index = 0;
        foreach (GameObject node in nodes)
        {
            FleeNodes[index] = node.transform;
            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AlertPlayerLocation(Vector3 position)
    {
        foreach(AIController controller in _AIs)
        {
            controller?.SetAlerted(position);
        }
        GameManager.GetInstance().OnPlayerSeen();
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
        if (_AIs == null)
        {
            LoadAllAI();
        }
        return _AIs;
    }

    public AIController[] GetScientistsAI()
    {
        if (_ScientistAI == null)
        {
            LoadAllAI();
        }
        return _ScientistAI;
    }

    private void LoadAllAI()
    {
        GameObject[] ais = GameObject.FindGameObjectsWithTag("AI");
        _AIs = new AIController[ais.Length];
        var index = 0;
        List<AIController> sciAi = new List<AIController>();
        foreach (GameObject ai in ais)
        {
            _AIs[index] = ai.GetComponent<AIController>();
            ScientistAIBehaviour scientist = ai.GetComponent<ScientistAIBehaviour>();
            if (scientist)
            {
                sciAi.Add(_AIs[index]);
            }
            index++;
        }
        _ScientistAI = sciAi.ToArray();
    }
}
