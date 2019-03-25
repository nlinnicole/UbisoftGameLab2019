using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSlinger : MonoBehaviour
{

    public GameObject slingerPrefab;
    public GameObject loadedSlinger;
    public GameObject button;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            loadedSlinger.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.None;
            loadedSlinger.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        }
    }
}
