using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTrigger : MonoBehaviour
{

    GameObject Player;

    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == "MovingPlatform")
        {
            other.GetComponent<EneteredFinalPlatform>().reachedend = true;
            other.GetComponentInChildren<MoveLeft>().reachedend = true;
            other.GetComponentInChildren<MoveRight>().reachedend = true;


        }
        
    }
}
