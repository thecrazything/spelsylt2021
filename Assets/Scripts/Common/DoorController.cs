using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    public GameObject Door;
    public bool isOpen {
        get {
            return !Door.activeSelf;
        }
    }

    public void Interact(GameObject player)
    {
        if (player.name == "Scientist") {
            Door.SetActive(false);
        }
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
