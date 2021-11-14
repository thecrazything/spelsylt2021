using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour, IPossesable, IInteractable
{
    public Transform SpawnLocation;
    public GameObject VentSystem;
    VentSystem ventSystem;
    private IController _Controller;

    void Start()
    {
        ventSystem = VentSystem.GetComponent<VentSystem>();
        ventSystem.Register(this);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public bool HasFeature(FeaturesEnum features)
    {
        return false;
    }

    public void OnAction(ActionEnum action)
    {
        if (action == ActionEnum.Puppeteer)
        {
            _Controller?.ResetPossessed(transform.position);
        }
        else if (action == ActionEnum.Interact)
        {
            CycleVent();
        }
    }

    public void OnLook(Vector2 dir) { }

    public void OnMove(float x, float y) { }

    public void OnPossess(IController controller)
    {
        _Controller = controller;

        SpriteRenderer r = GetComponent<SpriteRenderer>();
        r.color = Color.green;

        _Controller.GetGameObject().transform.position = transform.position;
    }

    public void OnUnPossess()
    {
        _Controller = null;

        SpriteRenderer r = GetComponent<SpriteRenderer>();
        r.color = Color.white;
    }

    private void CycleVent()
    {
        Vent newVent = ventSystem.GetNext(this);
        _Controller.SetPossessed(newVent);
    }

    public void OnUnfocused()
    {
        SpriteRenderer r = GetComponent<SpriteRenderer>();
        r.color = Color.white;
    }

    public void OnFocused()
    {
        SpriteRenderer r = GetComponent<SpriteRenderer>();
        r.color = Color.red;
    }

    public void Interact(GameObject player)
    {
        PlayerController pController = player.GetComponent<PlayerController>();
        pController.SetPossessed(this);
    }
}
