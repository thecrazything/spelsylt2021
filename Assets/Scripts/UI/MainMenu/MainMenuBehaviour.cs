using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    private bool musicStarted = false;
    private AudioClip _MapSong;
    // Start is called before the first frame update
    void Start()
    {
        _MapSong = Resources.Load<AudioClip>("Sounds/Music/main_menu");
    }

    // Update is called once per frame
    void Update()
    {
        if (!musicStarted)
        {
            MusicManager.GetInstance().PlayMusic(_MapSong);
        }
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
