using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : Node
{
    protected List<Node> _Nodes = new List<Node>();

    public SequenceNode(List<Node> nodes)
    {
        _Nodes = nodes;
    }

    public override NodeStates Evaluate(IPossesable possesable)
    {
        bool childRunning = false;

        foreach(Node node in _Nodes)
        {
            NodeStates state = node.Evaluate(possesable);
            switch(state)
            {
                case NodeStates.Failure:
                    _NodeState = state;
                    return _NodeState;
                case NodeStates.Success:
                    continue;
                case NodeStates.Running:
                    childRunning = true;
                    continue;
                default:
                    _NodeState = NodeStates.Success;
                    return _NodeState;
            }
        }
        _NodeState = childRunning ? NodeStates.Running : NodeStates.Success;
        return _NodeState;
    }
}
