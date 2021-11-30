using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Interactor : MonoBehaviour
{
    public LayerMask LayerMask;
    Transform focusedTransform = null;
    private ISet<Transform> _tracked = new HashSet<Transform>();

    IInteractable focusedInteractable {
        get {
            if (focusedTransform == null) return null;
            return focusedTransform.GetComponent<IInteractable>() ?? null;
        }
    }

    void Update()
    {
        foreach(var t in _tracked)
        {
            if (CanSee(t.gameObject))
            {
                if (focusedTransform != null)
                {
                    focusedInteractable.OnUnfocused();
                }
                focusedTransform = t;
                focusedInteractable.OnFocused();
            }
            else
            {
                focusedInteractable?.OnUnfocused();
                focusedTransform = null;
            }
        }
    }

    void OnDestroy()
    {
        focusedInteractable?.OnUnfocused();
    }

    void SetFocused(Transform transform)
    {
        _tracked.Add(transform);
    }

    public void Interact()
    {
        if (focusedInteractable != null)
        {
            focusedInteractable.Interact(transform.parent.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable interactable = collision.transform.GetComponent<IInteractable>();
        if (interactable != null) {
            SetFocused(collision.transform);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        _tracked.Remove(collision.transform);
    }

    private bool CanSee(GameObject target)
    {
        Vector3 origin = transform.parent.position;
        Vector3 targetPos = transform.position;
        Vector3 dir = origin - targetPos;
        RaycastHit2D hit = Physics2D.Raycast(origin + (dir * 1f), dir, 10f, LayerMask);
        return hit && hit.collider.name == target.name;
    }
}
