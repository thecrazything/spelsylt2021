using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string Name = "Interactable";

    public void Interact()
    {
        Debug.Log("Interacted with " + transform.name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Interactable triggered");
    }
}
