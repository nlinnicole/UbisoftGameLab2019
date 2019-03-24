using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaisePlatform : MonoBehaviour
{
    private Animator anim;
    public Animator buttonanimator;
    bool reachedend = false;
    // Start is called before the first frame update
    void Start()
    {
        bool reachedend = false;

        anim = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (reachedend)
        {
            anim.SetBool("RaisePlatform", true);
            buttonanimator.SetBool("PressButton", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Finished");
            reachedend = true;
        }
    }

}
