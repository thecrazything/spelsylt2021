using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementInput : MonoBehaviour
{
    private static readonly string AXIS_HORIZONTAL = "Horizontal";
    private static readonly string AXIS_VERTICAL = "Vertical";

    private IMovement _ActiveMovement;
    // Start is called before the first frame update
    void Start()
    {
        _ActiveMovement = GetComponent<IMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis(AXIS_HORIZONTAL);
        float y = Input.GetAxis(AXIS_VERTICAL);
        if (_ActiveMovement != null)
        {
            _ActiveMovement.OnMove(x, y);
        }
    }
}
