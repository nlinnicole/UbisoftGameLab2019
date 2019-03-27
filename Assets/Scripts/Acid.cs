using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour
{

    public GameObject acidSplash;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 11)
        {
            GameObject go = Instantiate(acidSplash);
            go.transform.position = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
        }
    }
}
