using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IController
{
    private static readonly string AXIS_HORIZONTAL = "Horizontal";
    private static readonly string AXIS_VERTICAL = "Vertical";
    private static readonly string BUTTON_INTERACT = "Interact";
    private static readonly string BUTTON_PUPPETEER = "Puppeteer";
    private static readonly string BUTTON_FIRE = "Fire1";
    private static readonly string BUTTON_CHANGE_VENT = "ChangeVent";

    private IPossesable _ActivePossesable;
    private IPossesable _DefaultPossesable;
    // Start is called before the first frame update
    void Start()
    {
        _ActivePossesable = GetComponent<IPossesable>();
        _DefaultPossesable = _ActivePossesable;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GetInstance().IsGameOver)
        {
            return;
        }
        float x = Input.GetAxis(AXIS_HORIZONTAL);
        float y = Input.GetAxis(AXIS_VERTICAL);
        // Normalize mouse relative to screen center
        Vector2 mousePos = new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2);
        float mouseX = mousePos.x;
        float mouseY = mousePos.y;
        if (_ActivePossesable != null)
        {
            _ActivePossesable.OnMove(x, y);
            _ActivePossesable.OnLook(new Vector2(mouseX, mouseY));

            if (Input.GetButtonUp(BUTTON_INTERACT))
            {
                _ActivePossesable.OnAction(ActionEnum.Interact);
            }
            if (Input.GetButtonUp(BUTTON_PUPPETEER))
            {
                _ActivePossesable.OnAction(ActionEnum.Puppeteer);
            }
            if (Input.GetButton(BUTTON_FIRE))
            {
                _ActivePossesable.OnAction(ActionEnum.FireWeapon);
            }
            if (Input.GetButton(BUTTON_CHANGE_VENT))
            {
                _ActivePossesable.OnAction(ActionEnum.Iterate);
            }
        }
    }

    public void SetPossessed(IPossesable possesable)
    {
        _ActivePossesable?.OnUnPossess();
        _ActivePossesable = possesable;
        _ActivePossesable?.OnPossess(this);
    }

    public void ResetPossessed()
    {
        if (_ActivePossesable != _DefaultPossesable)
        {
            _ActivePossesable?.OnUnPossess();
            _ActivePossesable = _DefaultPossesable;
            _ActivePossesable?.OnPossess(this);
        }
    }

    public void ResetPossessed(Vector3 newPos)
    {
        if (_ActivePossesable != _DefaultPossesable)
        {
            transform.position = newPos;

            _ActivePossesable?.OnUnPossess();
            _ActivePossesable = _DefaultPossesable;
            _ActivePossesable?.OnPossess(this);
        }
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
