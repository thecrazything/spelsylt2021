using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController
{
    public GameObject GetGameObject();
    public void SetPossessed(IPossesable possesable);

    public void ResetPossessed();
    public void ResetPossessed(Vector3 newPos);
}
