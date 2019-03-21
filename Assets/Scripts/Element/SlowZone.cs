using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Area that will change the speed of the player on enter and go back to the original speed on exit
 */

public class SlowZone : MonoBehaviour
{
    [SerializeField]
    private float slowSpeed = 1;
    [SerializeField]
    private float regularSpeed = 5;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerController>().changePlayerSpeed(slowSpeed);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().changePlayerSpeed(regularSpeed);
        }
    }
}
