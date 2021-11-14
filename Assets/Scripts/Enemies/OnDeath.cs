using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeath : MonoBehaviour
{
    public GameObject[] splatters;
    public int AmountOfBlood;
    public float MaxBloodDistance;

    void Start()
    {
        for (int i = 0; i < AmountOfBlood; i++)
        {
            int j = Random.Range(0, splatters.Length);
            GameObject bloodSplatter = splatters[j];

            Vector3 eRot = transform.rotation.eulerAngles;
            eRot.z = Random.Range(0, 360);
            Quaternion rot = Quaternion.Euler(eRot.x, eRot.y, eRot.z);

            Vector3 pos = transform.position;
            pos += new Vector3(Random.Range(0, MaxBloodDistance), Random.Range(0, MaxBloodDistance), Random.Range(0, MaxBloodDistance));

            Instantiate(bloodSplatter, pos, rot);
        }
    }
}
