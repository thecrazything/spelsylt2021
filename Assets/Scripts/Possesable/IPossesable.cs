using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPossesable
{
    void OnMove(float x, float y);
    void OnLook(Vector2 dir);

    void OnAction(ActionEnum action);
}
