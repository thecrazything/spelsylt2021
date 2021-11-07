using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Node
{
    public delegate NodeStates NodeReturn();

    protected NodeStates _NodeState;

    public NodeStates GetNodeState()
    {
        return _NodeState;
    }

    public Node() { }

    public abstract NodeStates Evaluate(IPossesable possesable);
}
