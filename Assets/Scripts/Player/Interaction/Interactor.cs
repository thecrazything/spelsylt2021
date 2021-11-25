using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Interactor : MonoBehaviour
{
    Transform focusedTransform = null;
    IInteractable focusedInteractable {
        get {
            if (focusedTransform == null) return null;
            return focusedTransform.GetComponent<IInteractable>() ?? null;
        }
    }

    void OnDestroy()
    {
        UnFocus();
    }

    void SetFocused(Transform transform)
    {
        if (focusedTransform != null) {
            focusedInteractable.OnUnfocused();
        }

        focusedTransform = transform;
        focusedInteractable.OnFocused();
    }

    void UnFocus()
    {
        if (focusedTransform == null) {
            return;
        }

        focusedInteractable.OnUnfocused();
        focusedTransform = null;
    }

    public void Interact()
    {
        if (focusedInteractable != null)
        {
            focusedInteractable.Interact(transform.parent.gameObject);
        } else
        {
            Debug.Log("Nothing to interact with");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with " + collision);
        IInteractable interactable = collision.transform.GetComponent<IInteractable>();
        if (interactable != null) {
            SetFocused(collision.transform);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (focusedTransform == collision.transform) {
            UnFocus();
        }
    }
}
