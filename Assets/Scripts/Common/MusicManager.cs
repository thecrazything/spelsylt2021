using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource _AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        _AudioSource = GetComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            _AudioSource?.Stop();
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopMusic()
    {
        _AudioSource.Stop();
    }

    public void PlayMusic(AudioClip mapSong)
    {
        if (_AudioSource.clip != mapSong || !_AudioSource.isPlaying)
        {
            _AudioSource.Stop();
            _AudioSource.clip = mapSong;
            _AudioSource.Play();
        }
    }

    public static MusicManager GetInstance()
    {
        return instance;
    }
}
