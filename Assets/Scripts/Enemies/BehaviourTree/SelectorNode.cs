using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : Node
{
    protected List<Node> _Nodes = new List<Node>();

    public SelectorNode(List<Node> nodes)
    {
        _Nodes = nodes;
    }

    public override NodeStates Evaluate(IPossesable possesable)
    {
        foreach (Node node in _Nodes)
        {
            NodeStates evaluated = node.Evaluate(possesable);
            switch(evaluated)
            {
                case NodeStates.Success:
                case NodeStates.Running:
                    _NodeState = evaluated;
                    return _NodeState;
                case NodeStates.Failure:
                default:
                    continue;
            }
        }
        _NodeState = NodeStates.Failure;
        return _NodeState;
    }
}
