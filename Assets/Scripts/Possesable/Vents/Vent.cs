using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Vent : MonoBehaviour, IPossesable, IInteractable
{
    public Light2D _Light;
    public Transform SpawnLocation;
    public GameObject VentSystem;
    private VentSystem ventSystem;
    private IController _Controller;
    private Color _OGColor;
    public Color SelectedColor;
    private SpriteRenderer _Rendered;
    public AudioSource EnterSound;
    public AudioSource ExitSound;

    void Start()
    {
        _Rendered = GetComponent<SpriteRenderer>();
        if (_Light)
        {
            _OGColor = _Light.color;
        }
    }

    public void RegisterVentSystem(VentSystem system)
    {
        ventSystem = system;
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
        if (action == ActionEnum.Interact)
        {
            EnterSound.Play();
            _Controller?.ResetPossessed(transform.position);
        }
        else if (action == ActionEnum.Iterate)
        {
            CycleVent();
        }
    }

    public void OnLook(Vector2 dir) { }

    public void OnMove(float x, float y) { }

    public void OnPossess(IController controller)
    {
        _Controller = controller;
        if (_Light)
        {
            _Light.color = SelectedColor;
        }

        _Controller.GetGameObject().transform.position = transform.position;
    }

    public void OnUnPossess()
    {
        _Controller = null;
        if (_Light)
        {
            _Light.color = _OGColor;
        }
    }

    private void CycleVent()
    {
        Vent newVent = ventSystem.GetNext(this);
        _Controller.SetPossessed(newVent);
    }

    public void OnUnfocused()
    {
        _Rendered.color = Color.white;
    }

    public void OnFocused()
    {
        _Rendered.color = Color.red;
    }

    public void Interact(GameObject player)
    {
        PlayerController pController = player.GetComponent<PlayerController>();
        ExitSound.Play();
        pController.SetPossessed(this);
    }
}
