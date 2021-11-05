using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public int GetAmmo();
    public float GetFireRate();
    public void Fire();
}
