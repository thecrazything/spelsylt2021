using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugAnimator : MonoBehaviour
{
    Bone rootBone;
    public GameObject rootBoneGO;

    void Start()
    {
        rootBone = rootBoneGO.GetComponent<Bone>();    
    }

    void Update()
    {
        float rot = Input.GetAxis("Horizontal");

        rootBone.Input(rot);
    }
}
