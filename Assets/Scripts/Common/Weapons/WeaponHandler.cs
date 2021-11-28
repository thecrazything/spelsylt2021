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
    public LayerMask VisibilityMask;

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

            Transform origin = posseable.GetGameObject().transform;
            Vector3 curPos = origin.position;
            Vector3 dir = (player.transform.position - curPos).normalized;

            // Aim at player
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            origin.rotation = Quaternion.Slerp(origin.rotation, q, Time.deltaTime * AimSpeed);
            float dotProd = Vector3.Dot(dir, transform.up);
            if (dotProd >= AimFuzz)
            {
                Vector3 fireDir = origin.up;
                Vector3 from = origin.position;
                from += fireDir * 2.0f; // Offset so we dont hit ourself
                RaycastHit2D hit = Physics2D.Raycast(from, fireDir, 100f, VisibilityMask);
                return !hit || PlayerDetectorBehaviour.IsPlayer(hit.collider) ? NodeStates.Success : NodeStates.Failure;
            }

            return NodeStates.Failure; // We are not, so any action depending on us aiming should be ignored
        });
    }

    public ActionNode GetHasAmmoNode()
    {
        return new ActionNode((posseable) => {
            return _TotalAmmo > 0 ? NodeStates.Success : NodeStates.Failure;
        });   
    }

    public SequenceNode GetAimFireSequenceNode()
    {
        List<Node> nodes = new List<Node> {GetHasAmmoNode(), GetAimAtPlayerNode(), GetFireNode() };
        return new SequenceNode(nodes);
    }
}
