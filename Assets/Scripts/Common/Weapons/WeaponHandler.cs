using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    private IWeapon _Weapon;
    private bool _CanFire = true;
    private float _Timer = 0;
    private int _TotalAmmo;
    public float AimFuzz = 0.9f; // How "on target" must we be to fire?
    public float AimSpeed = 2.0f; // How fast can we aim?

    // Start is called before the first frame update
    void Start()
    {
        _Weapon = GetComponent<IWeapon>();
        if (_Weapon == null)
        {
            throw new MissingComponentException("Component of type IWeapon required.");
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

    public ActionNode GetFireNode()
    {
        return new ActionNode((posseable) => { 
            if (_CanFire)
            {
                Fire();
                return NodeStates.Success;
            }
            return NodeStates.Failure;
        });
    }

    public ActionNode GetAimAtPlayerNode()
    {
        return new ActionNode((posseable) => {
            GameObject player = GameManager.GetInstance().GetPlayer();
            if (!player)
            {
                return NodeStates.Failure;
            }


            Vector3 curPos = transform.position;
            Vector3 dir = (player.transform.position - curPos).normalized;

            // Aim at player
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * AimSpeed);

            // check if we are done aiming
            // TODO Try to aim infront of the player? Might make it too accurate.
            float dotProd = Vector3.Dot(dir, transform.up);
            // TODO make sure nothing is blocking (simple raytrace, if we dont hit anything or hit the player, we fire)
            if (dotProd >= AimFuzz)
            {
                return NodeStates.Success; // We are aiming at the player
            }

            return NodeStates.Failure; // We are not, so any action depending on us aiming should be ignored
        });
    }

    public SequenceNode GetAimFireSequenceNode()
    {
        List<Node> nodes = new List<Node> { GetAimAtPlayerNode(), GetFireNode() };
        return new SequenceNode(nodes);
    }
}
