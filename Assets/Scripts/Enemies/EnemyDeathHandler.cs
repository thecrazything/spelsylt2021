using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathHandler : MonoBehaviour, IDeathHandler
{
    private IPossesable _Possesable;
    public GameObject body;
    public bool isScientist = false;
    public void Hit()
    {
        _Possesable.OnAction(ActionEnum.Puppeteer);

        Instantiate(body, transform.position, transform.rotation);

        Destroy(gameObject);

        GameManager.GetInstance().OnEnemyDeath(isScientist);
    }

    // Start is called before the first frame update
    void Start()
    {
        _Possesable = GetComponent<IPossesable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
