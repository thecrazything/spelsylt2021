using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private readonly string BUTTON_RESTART = "Restart";
    public bool _PlayerIsDead = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_PlayerIsDead)
        {
            if (Input.GetButtonDown(BUTTON_RESTART))
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
        }
    }

    public void OnPlayerDeath()
    {
        _PlayerIsDead = true;
    }
}
