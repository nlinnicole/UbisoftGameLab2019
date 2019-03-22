using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaisePlatform : MonoBehaviour
{
    private Animator anim;
    bool OnlyOnce = true;
    bool ReachedEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (OnlyOnce)
        {
            if (ReachedEnd)
            {
                Debug.Log("Raiseing platform");
                anim.SetBool("RaisePlatform", true);
                ReachedEnd = false;
                OnlyOnce = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Finished");
            ReachedEnd = true;
        }
    }

}
