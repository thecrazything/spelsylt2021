using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour, IDeathHandler
{
    private GameManager _GameManager;
    public void Hit()
    {
        _GameManager.OnPlayerDeath();
        Destroy(transform.parent.gameObject); // TODO Handle death
    }

    // Start is called before the first frame update
    void Start()
    {
        _GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
