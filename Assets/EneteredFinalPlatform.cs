using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneteredFinalPlatform : MonoBehaviour
{
    int counter = 0;
    public float platformspeed = 1;

    private void OnTriggerEnter(Collider other)
    {
        counter++;
        if (other.tag == "Player" && counter > 1) 
        {
            other.gameObject.GetComponent<PlayerController>().playerCamera.GetComponentInParent<CamPlayerFollow>().viewangle = 4;

        }

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.transform.SetParent(gameObject.transform, true);
            gameObject.transform.Translate(Vector3.forward * Time.deltaTime * platformspeed);
        }
    }


}
