using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfFinalLevel : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "FinalPlatform")
        {
            other.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            other.gameObject.GetComponent<EneteredFinalPlatform>().enabled = false;
        }
    }


}
