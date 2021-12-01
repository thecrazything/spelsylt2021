using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectorBehaviour : MonoBehaviour
{
    public float _AwarenessTimeout = 2.0f;
    public bool _CanSeePlayer = false;
    public bool _AwareOfPlayer = false;
    private float _AwarnessTimer = 0;
    public LayerMask VisibilityMask;
    GameObject _Player;

    public Animator animator;
    bool useAnimator = false;

    // Start is called before the first frame update
    void Start()
    {
        if (animator) useAnimator = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_CanSeePlayer && _AwareOfPlayer)
        {
            if (_AwarnessTimer >= _AwarenessTimeout)
            {
                _AwarnessTimer = 0;
                _AwareOfPlayer = false;
                _Player = null;
            }
            else
            {
                _AwarnessTimer += Time.deltaTime;
            }
        }

        if (_Player)
        {
            // Test if anything is blocking our view of the player
            Vector3 from = transform.parent.position;
            Vector3 dir = (_Player.transform.position - from).normalized;
            RaycastHit2D hit = Physics2D.Raycast(from, dir, 100f, VisibilityMask);
            _CanSeePlayer =  hit && IsPlayer(hit.collider);
            if (_CanSeePlayer)
            {
                if (!_AwareOfPlayer)
                {
                    AIManager.GetInstance().AlertPlayerLocation(_Player.transform.position);
                }
                _AwareOfPlayer = true;
            }
            else
            {
                _AwarnessTimer = 0;
            }
        }

        if (useAnimator) {
            animator.SetBool("IsAlerted", _AwareOfPlayer);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsPlayer(collision))
        {
            _Player = collision.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (IsPlayer(collision))
        {
            _Player = null;
            _CanSeePlayer = false;
            _AwarnessTimer = 0;
        }
    }

    public static bool IsPlayer(Collider2D collider)
    {
        return collider.gameObject.tag == "Player" || collider.gameObject.GetComponentInChildren<PlayerController>();
    }

    public ActionNode GetCanSeePlayerNode()
    {
        return new ActionNode((possesable =>
        {
            return _CanSeePlayer ? NodeStates.Success : NodeStates.Failure;
        }));
    }

    public ActionNode GetAwareOfPlayerNode()
    {
        return new ActionNode((possesable =>
        {
            return _AwareOfPlayer ? NodeStates.Success : NodeStates.Failure;
        }));
    }

    public void SetAlerted()
    {
        _AwareOfPlayer = true;
        _AwarnessTimer = 0;
    }
}
