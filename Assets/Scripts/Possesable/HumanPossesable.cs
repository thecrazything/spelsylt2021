using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPossesable : MonoBehaviour, IPossesable
{
    public float speed = 1f;
    private IController _Controller;

    public void OnMove(float x, float y)
    {
        Vector2 movement = new Vector2(x, y);
        if (movement.magnitude > 1)
        {
            movement = movement.normalized;
        }
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

    public void OnLook(Vector2 dir)
    {
        transform.up = new Vector3(dir.x, dir.y, 0) - transform.position;
    }

    public void OnAction(ActionEnum action)
    {
        if (action == ActionEnum.Puppeteer)
        {
            _Controller.ResetPossessed();
        }
    }

    public void OnPossess(IController controller)
    {
        _Controller = controller;
    }

    public void OnUnPossess()
    {
        _Controller = null;
    }
}
