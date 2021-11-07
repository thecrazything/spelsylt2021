using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    private float _TTL = 2f;
    private float _TTLCounter;
    private float _Speed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * _Speed;
        if (_TTLCounter >= _TTL)
        {
            Destroy(gameObject);
        }
        _TTLCounter += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
