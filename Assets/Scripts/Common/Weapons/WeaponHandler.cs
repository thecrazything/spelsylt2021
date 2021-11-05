using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    private IWeapon _Weapon;
    private bool _CanFire = true;
    private float _Timer = 0;
    private int _TotalAmmo;
    // Start is called before the first frame update
    void Start()
    {
        _Weapon = GetComponent<IWeapon>();
        if (_Weapon == null)
        {
            throw new MissingComponentException("Combonent of type IWeapon required.");
        }
        _TotalAmmo = _Weapon.GetAmmo();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_CanFire)
        {
            _Timer += Time.deltaTime;
            if (_Timer >= _Weapon.GetFireRate())
            {
                _CanFire = true;
            }
        }
    }

    public void Fire()
    {
        if (_CanFire && _TotalAmmo > 0)
        {
            _Weapon.Fire();
            _CanFire = false;
            _Timer = 0;
            _TotalAmmo -= 1;
        }
    }


}
