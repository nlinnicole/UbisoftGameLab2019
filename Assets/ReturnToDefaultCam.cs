using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToDefaultCam : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().playerCamera.GetComponentInParent<CamPlayerFollow>().viewangle = 0;

        }

    }
}
