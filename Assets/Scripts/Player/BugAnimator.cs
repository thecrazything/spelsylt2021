using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugAnimator : MonoBehaviour
{
    Bone rootBone;
    public GameObject rootBoneGO;

    public float turn = 0f;

    void Start()
    {
        rootBone = rootBoneGO.GetComponent<Bone>();    
    }

    void Update()
    {
        Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float goingAngle = FindDegree(dir.x, dir.y);
        float currentAngle = transform.parent.parent.rotation.eulerAngles.z;
        float diff = (goingAngle - currentAngle) / goingAngle;
        // TODO this is not working lol
        rootBone.Input(0);
    }

    public static float FindDegree(float x, float y)
    {
        float value = ((Mathf.Atan2(x, y) / Mathf.PI) * 180f);
        value -= 360;
        value = Mathf.Abs(value);
        return value - 180;
    }
}
