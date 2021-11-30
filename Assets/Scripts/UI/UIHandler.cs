using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class UIHandler : MonoBehaviour
{
    private static UIHandler _Instance;
    public GameObject RestartPanel;
    public GameObject EndPanel;
    public Text EnemiesKilled;
    public Text TimesDetected;
    public Text LossReason;

    private Volume _Volume;
    private Vignette _Vignette;

    // Start is called before the first frame update
    void Start()
    {
        if (_Instance && _Instance != this)
        {
            Destroy(gameObject);
        } 
        _Volume = GameObject.Find("Main Camera")?.GetComponent<Volume>();
        if (!_Volume)
        {
            throw new MissingReferenceException("Cant find Main Camera with volume");
        }
        _Volume.profile.TryGet(out _Vignette);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowRestartMessage()
    {
        RestartPanel.SetActive(true);
    }

    public void ShowEndMenu(int enemiesKilled, int totalEnemies, int timesDetected)
    {
        EndPanel.SetActive(true);
        EnemiesKilled.text = enemiesKilled.ToString() + " of " + totalEnemies.ToString();
        TimesDetected.text = timesDetected.ToString();
    }

    public void SetLossMessage(string text)
    {
        LossReason.text = text;
    }

    public void SetVignette(float val)
    {
        _Vignette.intensity.value = val;
    }

    public void ResetVignette()
    {
        _Vignette.intensity.value = 0;
    }

    public static UIHandler GetInstance()
    {
        if (_Instance)
        {
            return _Instance;
        }
        _Instance = GameObject.Find("PlayerUI").GetComponent<UIHandler>();
        return _Instance;
    }
}
