using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoliderAIBehaviour : MonoBehaviour, IAIBehaviour
{
    public PlayerDetectorBehaviour PlayerDetectorBehaviour;
    Node _Root;

    // Start is called before the first frame update
    void Start()
    {
        WeaponHandler weaponHandler = GetComponent<WeaponHandler>();
        NavigationBehaviour navigationBehaviour = GetComponent<NavigationBehaviour>();

        Node shouldFlee = new SequenceNode(new List<Node> { new InverterNode(weaponHandler.GetHasAmmoNode()), navigationBehaviour.GetFleeSequenceNode() });

        Node findAndKill = new SequenceNode(new List<Node> { PlayerDetectorBehaviour.GetCanSeePlayerNode(), navigationBehaviour.GetFollowPlayerNode(), weaponHandler.GetAimFireSequenceNode() });

        Node patrol = navigationBehaviour.GetPatrolNode();

        Node searchForPlayer = new SequenceNode(new List<Node> { PlayerDetectorBehaviour.GetAwareOfPlayerNode(), navigationBehaviour.GetGoToLastPlayerLocationNode(), navigationBehaviour.GetAdjustRotation() });

        // If we can see the player, find and kill, else search for them if we are aware, else patrol
        _Root = new SelectorNode(new List<Node> { shouldFlee, findAndKill, searchForPlayer, patrol });
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
