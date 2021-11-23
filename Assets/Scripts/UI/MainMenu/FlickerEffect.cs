using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlickerEffect : MonoBehaviour
{
    public float MinTimeBetweenChange = 1f;
    public int ChangeChance = 2;
    public int FlickerDice = 10;
    private Color _Dark;
    private float timer = 0;
    private Image _Image;
    private bool isLight = false;

    // Start is called before the first frame update
    void Start()
    {
        _Image = GetComponent<Image>();
        _Dark = _Image.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= MinTimeBetweenChange)
        {
            timer = 0;
            int roll = Random.Range(1, FlickerDice);
            if (roll <= ChangeChance)
            {
                if (isLight)
                {
                    _Image.color = _Dark;
                    isLight = false;
                }
                else
                {
                    _Image.color = Color.white;
                    isLight = true;
                }
            }
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
