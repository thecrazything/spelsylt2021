using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    public GameObject Door;

    public void Interact(GameObject player)
    {
        Door.SetActive(false);
    }

    public void OnFocused()
    {
        return;
    }

    public void OnUnfocused()
    {
        return;
    }
}
