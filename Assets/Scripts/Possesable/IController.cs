using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController
{
    public void SetPossessed(IPossesable possesable);

    public void ResetPossessed();
}
