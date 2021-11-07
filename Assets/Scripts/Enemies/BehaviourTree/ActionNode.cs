using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : Node
{
    public delegate NodeStates ActionNodeDelegate(IPossesable possesable);

    private ActionNodeDelegate _Action;

    public ActionNode(ActionNodeDelegate action)
    {
        _Action = action;
    }

    public override NodeStates Evaluate(IPossesable possesable)
    {
        return _Action(possesable);
    }
}
