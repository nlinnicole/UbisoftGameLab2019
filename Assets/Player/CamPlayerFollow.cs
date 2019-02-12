﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CamPlayerFollow : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public float lerpAmount;
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        target = (new Vector3(player1.transform.position.x, transform.position.y, player1.transform.position.z) + 
            new Vector3(player2.transform.position.x, transform.position.y, player2.transform.position.z) )/2;
        transform.position = Vector3.Lerp(transform.position, target, lerpAmount);
    }
}