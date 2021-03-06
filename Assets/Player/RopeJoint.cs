﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeJoint : MonoBehaviour
{

    public bool broken = false;
    public bool lineDeleted = false;
    public bool canBreak = true;

    void OnCollisionEnter(Collision collision)
    {
        if (!transform.parent.GetComponent<RopeGenerator>().isBroken && collision.gameObject.layer == 14 && canBreak)
        {
            transform.parent.GetComponent<RopeGenerator>().isBroken = true;
            broken = true;
        }
    }

    private void FixedUpdate()
    {
        if(!lineDeleted)
        {
            transform.GetChild(0).GetComponent<LineRenderer>().SetPosition(0, this.transform.position);
            transform.GetChild(0).GetComponent<LineRenderer>().SetPosition(1, GetComponent<ConfigurableJoint>().connectedBody.transform.position);
        }
    }
}
