using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteredMazeLevel : MonoBehaviour
{
    int counter = 0;

    private void Start()
    {
        counter = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            counter++;
            if(counter > 2)
            {
                other.gameObject.GetComponent<PlayerController>().playerCamera.GetComponentInParent<CamPlayerFollow>().viewangle = 2;
            }

        }

    }
}