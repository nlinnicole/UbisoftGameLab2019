using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public bool doorChosen = false;

    private void OnTriggerEnter(Collider other)
    {
        doorChosen = true;
    }
}
