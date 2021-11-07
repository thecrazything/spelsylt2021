using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverterNode : Node
{
    private Node _Node;

    public Node node
    {
        get { return _Node;  }
    }

    public InverterNode(Node node)
    {
        _Node = node;
    }

    public override NodeStates Evaluate(IPossesable possesable)
    {
        switch (_Node.Evaluate(possesable))
        {
            case NodeStates.Failure:
                _NodeState = NodeStates.Success;
                return _NodeState;
            case NodeStates.Success:
                _NodeState = NodeStates.Failure;
                return _NodeState;
            case NodeStates.Running:
                _NodeState = NodeStates.Running;
                return _NodeState;
        }
        _NodeState = NodeStates.Success;
        return _NodeState;
    }
}
