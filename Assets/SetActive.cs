using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    public Rigidbody polerigidbody;

    private void Start()
    {
        polerigidbody = gameObject.GetComponent<Rigidbody>();
        polerigidbody.isKinematic = true;
    }


    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Rope")
        {
            polerigidbody.isKinematic = false;
        }
    }


}
