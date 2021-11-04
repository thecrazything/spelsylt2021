using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectPossesable : MonoBehaviour, IPossesable
{
    public GameObject _SubPlayer;
    private Interactor _Interactor;
    private Puppeteering _Puppeteering;
    public float speed = 10f;
    public float rotSpeed = 1000f;
    // Start is called before the first frame update
    void Start()
    {
        _Interactor = _SubPlayer.GetComponent<Interactor>();
        _Puppeteering = _SubPlayer.GetComponent<Puppeteering>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(angle + 90, Vector3.forward), Time.deltaTime * rotSpeed);
        }
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
        _Puppeteering.DropPuppet();
    }

    public void OnUnPossess()
    {
        _SubPlayer.SetActive(false);
    }
}