using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistAIBehaviour : MonoBehaviour, IAIBehaviour
{
    public PlayerDetectorBehaviour PlayerDetectorBehaviour;
    Node _Root;
    public Node GetRootNode()
    {
        return _Root;
    }

    // Start is called before the first frame update
    void Start()
    {
        NavigationBehaviour navigationBehaviour = GetComponent<NavigationBehaviour>();

        Node patrol = new SequenceNode( new List<Node> { new InverterNode(PlayerDetectorBehaviour.GetAwareOfPlayerNode()), navigationBehaviour.GetPatrolNode() });

        _Root = new SelectorNode(new List<Node> {  patrol, navigationBehaviour.GetFleeSequenceNode() });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
