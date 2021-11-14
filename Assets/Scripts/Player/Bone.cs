using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    public float MaxRotation;
    public float BleedThrough;
    public GameObject SubBoneGO;
    Bone subBone;

    Vector3 startingRotation;

    void Start()
    {
        subBone = SubBoneGO.GetComponent<Bone>();
        startingRotation = transform.localEulerAngles;
    }

    public void Input(float amount)
    {
        //transform.Rotate(transform.forward, MaxRotation * amount);
        //transform.rotation = (transform.forward * (MaxRotation * amount));

        transform.localEulerAngles = (transform.forward * (MaxRotation * amount)) + startingRotation;

        if (subBone)
        {
            subBone.Input(amount * BleedThrough);
        }
    }
}
