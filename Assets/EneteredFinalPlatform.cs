using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneteredFinalPlatform : MonoBehaviour
{
    public GameObject MovingPlatform;

    public int counter = 0;
    public float platformspeed = 1;

    private void Start()
    {
        counter = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            counter++;
        }

        if (other.tag == "Player" && counter > 2) 
        {
            other.gameObject.GetComponent<PlayerController>().playerCamera.GetComponentInParent<CamPlayerFollow>().viewangle = 4;

        }

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && counter >2)
        {
            other.gameObject.transform.SetParent(MovingPlatform.transform);
            gameObject.transform.Translate(Vector3.forward * Time.deltaTime * platformspeed);
        }
    }


}
