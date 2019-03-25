using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttach : MonoBehaviour
{
    public Transform MasterController;

    private void Start()
    {
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            MasterController = other.gameObject.transform.parent;
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = MasterController.transform;
            other.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}