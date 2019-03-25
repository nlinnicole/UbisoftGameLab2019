using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteredWallLevel : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().playerCamera.GetComponentInParent<CamPlayerFollow>().viewangle = 3;

        }

    }


}
