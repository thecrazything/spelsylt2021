using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puppetable : MonoBehaviour
{
    private IPossesable _PupetMovement;
    private IController _Controller;
    private AudioSource _PossesAudioSource;
    private AudioClip[] _PossesSounds;

    // Start is called before the first frame update
    void Start()
    {
        _PupetMovement = GetComponent<IPossesable>();
        _Controller = GetComponent<IController>();
        if (_PupetMovement == null)
        {
            throw new MissingComponentException("Pupet needs IMovement component, otherwise player wont know how it controls.");
        }
        _PossesAudioSource = gameObject.AddComponent<AudioSource>();
        _PossesAudioSource.volume = 0.5f;
        _PossesAudioSource.playOnAwake = false;
        _PossesSounds = new AudioClip[4];
        _PossesSounds[0] = Resources.Load<AudioClip>("Sounds/Enemy/posses_sound_1");
        _PossesSounds[1] = Resources.Load<AudioClip>("Sounds/Enemy/posses_sound_2");
        _PossesSounds[2] = Resources.Load<AudioClip>("Sounds/Enemy/posses_sound_3");
        _PossesSounds[3] = Resources.Load<AudioClip>("Sounds/Enemy/posses_sound_4");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IPossesable GetPossesable()
    {
        return _PupetMovement;
    }

    public IController GetController()
    {
        return _Controller;
    }

    public void OnMakePuppet()
    {
        _PossesAudioSource.PlayOneShot(_PossesSounds[Random.Range(0, 3)]);
    }
}
