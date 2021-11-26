using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public GameObject RestartPanel;
    public GameObject EndPanel;
    public Text EnemiesKilled;
    public Text TimesDetected;
    public Text LossReason;

    // Start is called before the first frame update
    void Start()
    {
        
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
        EnemiesKilled.text = enemiesKilled.ToString() + "/" + totalEnemies.ToString();
        TimesDetected.text = timesDetected.ToString();
    }

    public void SetLossMessage(string text)
    {
        LossReason.text = text;
    }
}
