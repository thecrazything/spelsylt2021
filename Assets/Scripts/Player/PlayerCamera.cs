using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float mouseScale = 0.5f;
    public float maxMouseOffset = 2f;
    private Camera _camera;
    public bool Delay;
    public float DelaySpeed = 1;
    private Vector3 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.Find("Main Camera")?.GetComponent<Camera>();
        if (!_camera)
        {
            throw new MissingComponentException("No camera named Main Camera found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2);
        float mouseX = mousePos.x * mouseScale * 0.01f;
        float mouseY = mousePos.y * mouseScale * 0.01f;
        if (mouseX > maxMouseOffset)
        {
            mouseX = maxMouseOffset;
        } 
        else if (mouseX < -maxMouseOffset)
        {
            mouseX = -maxMouseOffset;
        }
        if (mouseY > maxMouseOffset)
        {
            mouseY = maxMouseOffset;
        }
        else if (mouseY < -maxMouseOffset)
        {
            mouseY = -maxMouseOffset;
        }
        if (Delay)
        {
            Vector3 newPos = Vector3.MoveTowards(_camera.transform.position, new Vector3(transform.position.x + mouseX, transform.position.y + mouseY, _camera.transform.position.z), DelaySpeed * Time.deltaTime);
            _camera.transform.position = new Vector3(newPos.x , newPos.y, _camera.transform.position.z);
        }
        else
        {
            _camera.transform.position = new Vector3(transform.position.x + mouseX, transform.position.y + mouseY, _camera.transform.position.z);
        }
    }
}
