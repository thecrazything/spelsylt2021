using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    SpriteRenderer r;
    
    public string Name = "Interactable";

    void Start()
    {
        r = GetComponent<SpriteRenderer>();
    }

    public void OnUnfocused()
    {
        Debug.Log(Name + " lost focus");
        r.color = Color.white;
    }

    public void OnFocused()
    {
        Debug.Log(Name + " got focus");
        r.color = Color.red;
    }

    public void Interact()
    {
        Debug.Log("Interacted with " + transform.name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Interactable triggered");
    }
}
