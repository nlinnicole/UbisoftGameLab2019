using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Regular button that will open the door when pressed
 */

public class RegularDoorButton : MonoBehaviour
{

    [SerializeField]
    private GameObject door;
    [SerializeField]
    private float doorSpeed = 5f;
    [SerializeField]
    private float doorDuration = 3f;

    //door height
    private float doorOpenHeight;
    //door original position 
    private float doorOriginalHeight;

    private float doorTimer;
    private bool doorOpening = false;
    private bool doorClosing = false;


    void Start()
    {
        //get the height of door to set the height the door will open
        doorOpenHeight = (door.GetComponent<Collider>().bounds.size.y)*2;
        doorOriginalHeight = (door.GetComponent<Collider>().bounds.size.y)/2;
    }

    void Update()
    {
        //open door until certain height, then close door after certain time
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

}
