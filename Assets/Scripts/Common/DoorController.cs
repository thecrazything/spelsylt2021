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
        ScientistAIBehaviour behavesLikeScientist = player.GetComponent<ScientistAIBehaviour>();
        if (behavesLikeScientist != null) {
            Door.SetActive(false);
        }
    }

    public void OnFocused(GameObject focuser)
    {
        return;
    }

    public void OnUnfocused()
    {
        return;
    }
}
