using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAIBehaviour : MonoBehaviour, IAIBehaviour
{
    Node _Root;

    // Start is called before the first frame update
    void Start()
    {
        WeaponHandler weaponHandler = GetComponent<WeaponHandler>();
        NavigationBehaviour navigationBehaviour = GetComponent<NavigationBehaviour>();
        // _Root = new SelectorNode(new List<Node> { weaponHandler.GetAimFireSequenceNode() });
        _Root = navigationBehaviour.GetPatrolNode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Node GetRootNode()
    {
        return _Root;
    }
}
