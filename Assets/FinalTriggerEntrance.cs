using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTriggerEntrance : MonoBehaviour
{

    GameObject CamParent;

    private void OnTriggerEnter(Collider other)
    {
        CamParent = other.GetComponent<PlayerController>().playerCamera.gameObject;
    }
}
