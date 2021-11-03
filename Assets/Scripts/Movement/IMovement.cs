using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    void OnMove(float x, float y);
    void OnLook(Vector2 dir);
}
