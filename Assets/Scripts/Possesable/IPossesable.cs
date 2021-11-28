using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPossesable
{
    void OnMove(float x, float y);
    void OnLook(Vector2 dir);

    void OnAction(ActionEnum action);

    void OnPossess(IController controller);

    void OnUnPossess();

    bool HasFeature(FeaturesEnum features);

    bool IsCursorVisible()
    {
        return false;
    }

    GameObject GetGameObject();
}
