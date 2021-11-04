using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetFinder : MonoBehaviour
{
    private HashSet<GameObject> _Targets = new HashSet<GameObject>();
    private Puppeteering _Puppeteering;

    // Start is called before the first frame update
    void Start()
    {
        _Puppeteering = transform.parent.GetComponent<Puppeteering>();
    }

    // Update is called once per frame
    void Update()
    {
        SetClosest();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;
        if (IsPuppet(target))
        {
            _Targets.Add(target);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;
        if (IsPuppet(target))
        {
            target.GetComponent<SpriteRenderer>().enabled = false;
            _Targets.Remove(target);
        }
    }

    private void SetClosest()
    {
        if (_Targets.Count == 0)
        {
            _Puppeteering.NoTarget();
        }
        else
        {
            GameObject closest = null;
            float closestDist = 0;
            Vector3 ourPos = transform.parent.parent.position;
            foreach(GameObject target in _Targets)
            {
                target.GetComponent<SpriteRenderer>().enabled = false;
                Vector3 targetPos = target.transform.position;
                float distance = Vector3.Distance(ourPos, targetPos);
                if (!closest || distance < closestDist)
                {
                    closest = target;
                    closestDist = distance;
                }
            }
            _Puppeteering.SetTarget(closest);
            closest.GetComponent<SpriteRenderer>().enabled = true;
            // Probably move the 'show target' bits to Puppetable
        }
    }

    private bool IsPuppet(GameObject target)
    {
        return target.GetComponent<Puppetable>();
    }
}
