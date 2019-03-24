using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToSpinner : MonoBehaviour
{
    public GameObject MasterController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.parent = gameObject.transform.parent;
        }
        
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.parent = MasterController.transform.parent;
        }
    }
}
