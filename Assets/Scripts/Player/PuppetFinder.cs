using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetFinder : MonoBehaviour
{
    public LayerMask Mask;
    private HashSet<GameObject> _Targets = new HashSet<GameObject>();
    private Puppeteering _Puppeteering;

    // Start is called before the first frame update
    void Start()
    {
        _Puppeteering = transform.parent.parent.GetComponent<Puppeteering>();
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
            foreach (GameObject target in _Targets)
            {
                target.GetComponent<SpriteRenderer>().enabled = false;
                Vector3 targetPos = target.transform.position;
                float distance = Vector3.Distance(ourPos, targetPos);
                if (!closest || distance < closestDist)
                {
                    Vector3 dir = targetPos - ourPos;
                    Vector3 origin = ourPos + (dir * 0.5f);
                    RaycastHit2D hit = Physics2D.Raycast(origin, targetPos, distance, Mask);
                    if (hit && hit.collider.name == target.name)
                    {
                        closest = target;
                        closestDist = distance;
                    }
                }
            }
            if (closest)
            {
                _Puppeteering.SetTarget(closest);
                closest.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                _Puppeteering.NoTarget();
            }
        }
    }

    private bool IsPuppet(GameObject target)
    {
        return target.GetComponent<Puppetable>();
    }
}
