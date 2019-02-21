using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldDoorButton : MonoBehaviour
{
    [SerializeField]
    private GameObject door;

    //door open speed
    [SerializeField]
    private float doorSpeed = 5f;

    //door height
    [SerializeField]
    private float doorOpenHeight;

    //door original position 
    [SerializeField]
    private float doorOriginalHeight;

    private bool doorOpening = false;
    private bool doorClosing = false;

    void Start()
    {

    }

    void Update()
    {

        if (doorOpening)
        {
            //open door until certain height
            if (door.transform.position.y < doorOpenHeight)
            {
                door.transform.Translate(Vector3.up * Time.deltaTime * doorSpeed, Space.World);
            }
            else
            {
                doorOpening = false;
            }
        }

        if (doorClosing)
        {
            //close door until certain height
            if (door.transform.position.y > doorOriginalHeight)
            {
                door.transform.Translate(Vector3.down * Time.deltaTime * doorSpeed, Space.World);
            }
            else
            {
                doorClosing = false;
            }
        }

    }

    //door open only on enter
    public void OnTriggerEnter(Collider other)
    {
        doorOpening = true;
        doorClosing = false;
    }
    public void OnTriggerExit(Collider other)
    {
        doorClosing = true;
        doorOpening = false;
    }
}
