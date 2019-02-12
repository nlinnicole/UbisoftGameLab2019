using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularDoorButton : MonoBehaviour
{

    [SerializeField]
    private GameObject door;

    private float doorSpeed = 5f;
    private float doorDuration = 3f;

    private float doorTimer;
    private bool doorMoving = false;

    void Start()
    {

    }

    void Update()
    {
        if (doorMoving)
        {
            if(door.transform.position.y < 4f)
            {
                door.transform.Translate(Vector3.up * Time.deltaTime * doorSpeed, Space.World);
            }
            else
            {
                doorMoving = false;
            }
        }
        else if (Time.time > doorTimer + doorDuration)
        {
            if(door.transform.position.y > 1.3f)
            {
                door.transform.Translate(Vector3.down * Time.deltaTime * doorSpeed, Space.World);
            }
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        doorTimer = Time.time;
        doorMoving = true;
    }
}
