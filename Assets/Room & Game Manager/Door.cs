using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool doorChosen = false;

    private void OnTriggerStay(Collider other)
    {
        doorChosen = true;
    }
}
