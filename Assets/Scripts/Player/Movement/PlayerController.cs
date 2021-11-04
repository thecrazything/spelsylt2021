using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static readonly string AXIS_HORIZONTAL = "Horizontal";
    private static readonly string AXIS_VERTICAL = "Vertical";
    private static readonly string BUTTON_INTERACT = "Interact";
    private static readonly string BUTTON_PUPPETEER = "Puppeteer";

    private IPossesable _ActiveMovement;
    private IPossesable _DefaultMovement;
    // Start is called before the first frame update
    void Start()
    {
        _ActiveMovement = GetComponent<IPossesable>();
        _DefaultMovement = _ActiveMovement;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis(AXIS_HORIZONTAL);
        float y = Input.GetAxis(AXIS_VERTICAL);
        // Normalize mouse relative to screen center
        Vector2 mousePos = new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2);
        float mouseX = mousePos.x;
        float mouseY = mousePos.y;
        if (_ActiveMovement != null)
        {
            _ActiveMovement.OnMove(x, y);
            _ActiveMovement.OnLook(new Vector2(mouseX, mouseY).normalized); // TODO controler stick support?

            if (Input.GetButtonUp(BUTTON_INTERACT))
            {
                _ActiveMovement.OnAction(ActionEnum.Interact);
            }
            if (Input.GetButtonUp(BUTTON_PUPPETEER))
            {
                _ActiveMovement.OnAction(ActionEnum.Puppeteer);
            }
        }
    }

    public void SetMovement(IPossesable movement)
    {
        _ActiveMovement = movement;
    }

    public void ResetMovement()
    {
        _ActiveMovement = _DefaultMovement;
    }
}
