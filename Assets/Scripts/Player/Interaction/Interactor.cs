using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    Transform focusedInteractable = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        if (focusedInteractable != null)
        {
            Interactable interactable = focusedInteractable.GetComponent<Interactable>();
            interactable.Interact();
        } else
        {
            Debug.Log("Nothing to interact with");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger enter " + collision.transform.name);

        Interactable interactable = collision.transform.GetComponent<Interactable>();
        if (interactable != null)
        {
            Debug.Log("Is interactable!");
            //focusedInteractable = interactable;
            focusedInteractable = collision.transform;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Trigger exit " + collision.transform.name);

        if (focusedInteractable == collision.transform) {
            focusedInteractable = null;
            Debug.Log("Was focused transform");
        }
    }
}
