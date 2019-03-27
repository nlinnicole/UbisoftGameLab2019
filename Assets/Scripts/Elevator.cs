using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public GameObject elevator;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 11)
        {
            elevator.GetComponent<Animator>().SetBool("IsActive", true);
        }
    }


}
