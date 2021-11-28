using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map5IntroText : MonoBehaviour
{
    public Text Line1;
    public Text Line2;
    public Text Line3;

    public float AppearTime;
    public float AppearTime2;
    public float AppearTime3;
    public float RemoveTime;
    private float _timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        Line1.enabled = false;
        Line2.enabled = false;
        Line3.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= AppearTime)
        {
            Line1.enabled = true;
        }
        if (_timer >= AppearTime2)
        {
            Line2.enabled = true;
        }
        if (_timer >= AppearTime3)
        {
            Line3.enabled = true;
        }
        if (_timer >= RemoveTime)
        {
            Line1.enabled = false;
            Line2.enabled = false;
            Line3.enabled = false;
            Destroy(gameObject);
        }
    }
}
