using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _Instance;
    private readonly string BUTTON_RESTART = "Restart";
    public bool _PlayerIsDead = false;
    public int NextMapIndex = 0;
    public string MapSong = "";
    private GameObject _Player;
    private UIHandler _UIHandler;
    private int _TotalEnemyCount;
    private int _TotalEnemiesKilledCount = 0;
    private int _TimesPlayerWasDetected = 0;
    public bool IsGameOver = false;
    public int _TotalScientists = 0;

    public GameObject ExitDoorObject;
    private DoorController _ExitDoor;

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
        AudioClip mapSong = Resources.Load<AudioClip>("Sounds/Music/" + MapSong);
        MusicManager.GetInstance()?.PlayMusic(mapSong);
        _TotalEnemyCount = AIManager.GetInstance().GetAIs().Length;
        _TotalScientists = AIManager.GetInstance().GetScientistsAI().Length;

        _ExitDoor = ExitDoorObject.GetComponent<DoorController>();
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
        if (IsGameOver)
        {
            if (Input.GetButtonDown(BUTTON_RESTART))
            {
                SceneManager.LoadScene(NextMapIndex);
            }
        }
    }

    public void OnPlayerDeath()
    {
        _PlayerIsDead = true;
        CheckIfLost();
    }

    /// <summary>
    /// Call this to end the game
    /// </summary>
    public void OnGameEnd()
    {
        _UIHandler.ShowEndMenu(_TotalEnemiesKilledCount, _TotalEnemyCount, _TimesPlayerWasDetected);
        IsGameOver = true;
    }

    public void OnEnemyDeath(bool isScientist)
    {
        _TotalEnemiesKilledCount += 1;
        CheckIfLost();
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

    void CheckIfLost()
    {
        // If number of scientist = 0
        // and the exit door has not been opened
        // OR the player is dead
        int scientistsLeft = GameObject.FindGameObjectsWithTag("Scientist").Length;

        if (scientistsLeft == 0 && !_ExitDoor.isOpen) {
            HandlePlayerLost("Locked in");
            _PlayerIsDead = true;
            return;
        }

        if (_PlayerIsDead) {
            HandlePlayerLost("You died");
            _PlayerIsDead = true;
            return;
        }
    }

    void HandlePlayerLost(string msg = "No reason specified, just git gud")
    {
        _UIHandler.SetLossMessage("Lost reason: " + msg);
        _UIHandler.ShowRestartMessage();
    }
}
