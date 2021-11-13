using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentSystem : MonoBehaviour
{
    List<Vent> vents = new List<Vent>();

    public void Register(Vent vent)
    {
        vents.Add(vent);
    }

    public Vent GetNext(Vent current)
    {
        int i = vents.IndexOf(current);

        if (i == (vents.Count - 1))
        {
            return vents[0];
        }

        return vents[i + 1];
    }
}
