using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public GameObject obj;
    Vector3 initPos;

    private void Start()
    {
        initPos = obj.transform.position;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "DragObject")
        {
            obj.transform.position = initPos;
            obj.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
