using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteredMazeLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("EnteredTopDown");
            other.gameObject.GetComponent<PlayerController>().playerCamera.GetComponentInParent<CamPlayerFollow>().viewangle = 2;

        }

    }
}