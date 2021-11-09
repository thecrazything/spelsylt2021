using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : MonoBehaviour, IWeapon
{

    public int Ammo = 10;
    public float FireRate = 1.0f;
    public float BulletSpread = 1.0f;
    public float Range = 100f;
    public float FireOffset = 2.0f;
    public LayerMask VisibilityMask;
    private GameObject _Projectile;
    // Start is called before the first frame update
    void Start()
    {
        _Projectile = Resources.Load("Prefabs/Projectiles/Projectile") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        Vector3 fireDir = transform.up;
        Vector3 origin = transform.position;
        origin += fireDir * FireOffset; // Offset so we dont hit ourself
        GameObject instance = Instantiate(_Projectile);
        instance.transform.position = origin;
        instance.transform.rotation = transform.rotation;
        RaycastHit2D hit = Physics2D.Raycast(origin, fireDir, Range, VisibilityMask);

        if (hit)
        {
            IDeathHandler deathHandler = hit.collider.gameObject.GetComponent<IDeathHandler>();
            if (deathHandler != null)
            {
                deathHandler.Hit();
            }
        }
    }

    public int GetAmmo()
    {
        return Ammo;
    }

    public float GetFireRate()
    {
        return FireRate;
    }
}
