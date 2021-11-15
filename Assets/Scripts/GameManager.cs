using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _Instance;
    private readonly string BUTTON_RESTART = "Restart";
    public bool _PlayerIsDead = false;
    private GameObject _Player;
    private UIHandler _UIHandler;
    private int _TotalEnemyCount;
    private int _TotalEnemiesKilledCount = 0;
    private int _TimesPlayerWasDetected = 0;

    // Start is called before the first frame update
    void Start()
    {
        _Player = GameObject.Find("Player");
        if (!_Player)
        {
            throw new MissingReferenceException("No gameobject named Player");
        }
        _UIHandler = GameObject.Find("PlayerUI")?.GetComponent<UIHandler>();
        if (!_UIHandler)
        {
            throw new MissingReferenceException("No gameobject named PlayerUI with a UIHandler component.");
        }
        MusicManager.GetInstance()?.PlayMusic();
        _TotalEnemyCount = AIManager.GetInstance().GetAIs().Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (_PlayerIsDead)
        {
            _Player = null;
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
        _UIHandler.ShowRestartMessage();
    }

    public void OnEnemyDeath()
    {
        _TotalEnemiesKilledCount += 1;
        if (_TotalEnemiesKilledCount >= _TotalEnemyCount)
        {
            Debug.Log("All enemies killed!");
        }
    }

    public void OnPlayerSeen()
    {
        _TimesPlayerWasDetected += 1; // For fun statistics
    }

    /// <summary>
    /// Return the player. CAN BE NULL IF PLAYER DIES!
    /// </summary>
    /// <returns>GameObject of the Player if still alive</returns>
    public GameObject GetPlayer()
    {
        return _Player;
    }

    public static GameManager GetInstance()
    {
        if (_Instance)
        {
            return _Instance;
        }
        _Instance = GameObject.Find("GameManager")?.GetComponent<GameManager>();
        if (!_Instance)
        {
            throw new MissingReferenceException("No GameManager found");
        }
        return _Instance;
    }
}
