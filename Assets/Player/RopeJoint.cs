using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeJoint : MonoBehaviour
{

    public bool broken = false;
    void OnCollisionEnter(Collision collision)
    {
        if (!transform.parent.GetComponent<RopeGenerator>().isBroken && collision.gameObject.layer == 14)
        {
            transform.parent.GetComponent<RopeGenerator>().isBroken = true;
            broken = true;
            gameObject.SetActive(false);
        }
    }
}
