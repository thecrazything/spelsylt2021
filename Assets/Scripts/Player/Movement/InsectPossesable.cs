using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectPossesable : MonoBehaviour, IPossesable
{
    public GameObject _SubPlayer;
    private Interactor _Interactor;
    private Puppeteering _Puppeteering;
    private Rigidbody2D _Rigid;
    private CapsuleCollider2D _Collider;
    public float speed = 10f;
    public float rotSpeed = 1000f;

    public Animator Animator;

    // Start is called before the first frame update
    void Start()
    {
        _Interactor = _SubPlayer.GetComponent<Interactor>();
        _Puppeteering = GetComponent<Puppeteering>();
        _Rigid = GetComponent<Rigidbody2D>();
        _Collider = GetComponent<CapsuleCollider2D>();
    }

    public void OnMove(float x, float y)
    {
        Vector2 movement = new Vector2(x, y);
        if (movement.magnitude > 1)
        {
            movement = movement.normalized;
        }
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(angle + 90, Vector3.forward), Time.deltaTime * rotSpeed * movement.magnitude);
        }

        Animator.SetBool("IsMoving", movement != Vector2.zero);
    }

    public void OnLook(Vector2 dir)
    {
        // Do nothing
    }

    public void OnAction(ActionEnum action)
    {
        if (action == ActionEnum.Interact)
        {
            _Interactor?.Interact();
        } else if(action == ActionEnum.Puppeteer)
        {
            _Puppeteering?.Puppeteer();
        }
    }

    public void OnPossess(IController controller)
    {
        _SubPlayer.SetActive(true);
        _Collider.enabled = true;
        _Puppeteering.DropPuppet();
        _Rigid.simulated = true;
    }

    public void OnUnPossess()
    {
        _SubPlayer.SetActive(false);
        _Collider.enabled = false;
        _Rigid.simulated = false;
    }

    public bool HasFeature(FeaturesEnum features)
    {
        return false;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
