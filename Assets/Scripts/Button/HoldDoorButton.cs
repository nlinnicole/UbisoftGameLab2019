using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldDoorButton : MonoBehaviour
{
    [SerializeField]
    private GameObject door;

    private float doorSpeed = 5f;

    private bool doorOpening = false;
    private bool doorClosing = false;

    void Start()
    {

    }

    void Update()
    {
        if (doorOpening)
        {
            if (door.transform.position.y < 4f)
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
            if (door.transform.position.y > 1.3f)
            {
                door.transform.Translate(Vector3.down * Time.deltaTime * doorSpeed, Space.World);
            }
            else
            {
                doorClosing = false;
            }
        }

    }

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
