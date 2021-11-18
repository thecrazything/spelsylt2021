using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puppeteering : MonoBehaviour
{
    private PlayerController _PlayerController;
    private Puppetable _CurrentPuppet;
    private GameObject _Target;
    private float _PuppetMaxtime = 10f; // TODO adjust to reasonable length
    private float _PuppetTimer = 0f;
    private AudioSource _AudioSource;
    private AudioClip[] _DropSounds;

    // Start is called before the first frame update
    void Start()
    {
        _PlayerController = transform.GetComponent<PlayerController>();
        if (!_PlayerController)
        {
            throw new MissingComponentException("Missing PlayerController");
        }
        _AudioSource = gameObject.AddComponent<AudioSource>();
        _AudioSource.volume = 0.5f;
        _AudioSource.playOnAwake = false;
        _DropSounds = new AudioClip[3];
        _DropSounds[0] = Resources.Load<AudioClip>("Sounds/Player/drop_puppet_1");
        _DropSounds[1] = Resources.Load<AudioClip>("Sounds/Player/drop_puppet_2");
        _DropSounds[2] = Resources.Load<AudioClip>("Sounds/Player/drop_puppet_3");
    }

    // Update is called once per frame
    void Update()
    {
        if (_CurrentPuppet)
        {
            _PuppetTimer += Time.deltaTime;
            if (_PuppetTimer >= _PuppetMaxtime)
            {
                DropPuppet();
            }
        }
    }

    public void Puppeteer()
    {
        // TODO check if puppet avalible, then run MakeTargetPuppet on that
        if (!_CurrentPuppet && _Target)
        {
            MakeTargetPuppet(_Target);
        }
        else if (_CurrentPuppet)
        {
            DropPuppet();
        }
    }

    private void MakeTargetPuppet(GameObject target)
    {
        _PuppetTimer = 0;
        Puppetable puppet = target.GetComponent<Puppetable>();
        if (!puppet)
        {
            return;
        }
        puppet.OnMakePuppet();
        puppet.GetController().ResetPossessed();
        _PlayerController.SetPossessed(puppet.GetPossesable());
        _CurrentPuppet = puppet;
        transform.position = target.transform.position;
        transform.rotation = target.transform.rotation * Quaternion.Euler(0,0,180);
        transform.parent = target.transform; // This might be a bad idea
    }

    public void DropPuppet()
    {
        if (_CurrentPuppet)
        {
            _PlayerController.ResetPossessed();
            transform.parent = null;
            IDeathHandler deathHandler = _CurrentPuppet?.gameObject?.GetComponent<IDeathHandler>();
            deathHandler?.Hit();
            _CurrentPuppet = null;
            _AudioSource.PlayOneShot(_DropSounds[Random.Range(0, 2)]);
        }
    }

    public void SetTarget(GameObject target)
    {
        _Target = target;
    }

    public void NoTarget()
    {
        _Target = null;
    }
}
