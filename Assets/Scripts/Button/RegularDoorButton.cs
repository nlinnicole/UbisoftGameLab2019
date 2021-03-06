﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularDoorButton : MonoBehaviour
{

    [SerializeField]
    private GameObject door;
    [SerializeField]
    private float doorSpeed = 5f;
    [SerializeField]
    private float doorDuration = 3f;

    //door height
    [SerializeField]
    private float doorOpenHeight;

    //door original position 
    [SerializeField]
    private float doorOriginalHeight;

    private float doorTimer;
    private bool doorOpening = false;
    private bool doorClosing = false;


    void Start()
    {

    }

    void Update()
    {
        if (doorOpening)
        {
            if(door.transform.position.y < doorOpenHeight)
            {
                door.transform.Translate(Vector3.up * Time.deltaTime * doorSpeed, Space.World);
            }
            else
            {
                doorOpening = false;
                doorClosing = true;
            }
        }
        //OPEN DOOR
        else if (doorClosing && Time.time > doorTimer + doorDuration)
        {
            if(door.transform.position.y > doorOriginalHeight)
            {
                door.transform.Translate(Vector3.down * Time.deltaTime * doorSpeed, Space.World);
            }
            else
            {
                doorClosing = false;
            }
        }

    }
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "DiceTrigger")
        {
            doorTimer = Time.time;
            doorOpening = true;
        }
    }
}
