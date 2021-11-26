using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentSystem : MonoBehaviour
{
    public Vent[] Vents;
    void Start()
    {
        Vents = transform.GetComponentsInChildren<Vent>();
        foreach(var vent in Vents)
        {
            vent.RegisterVentSystem(this);
        }
    }

    public Vent GetNext(Vent current)
    {
        int i = new List<Vent>(Vents).IndexOf(current);

        if (i == (Vents.Length - 1))
        {
            return Vents[0];
        }

        return Vents[i + 1];
    }
}
