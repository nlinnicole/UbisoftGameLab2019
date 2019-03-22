using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ropeSOund : MonoBehaviour
{
    bool ropebroken;
    bool ropeReset = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ropebroken = gameObject.GetComponent<RopeGenerator>().isBroken;

        if (ropebroken && ropeReset)
        {
            // trigger rope broken sound event 
            AkSoundEngine.PostEvent("ropeBroken", gameObject);
            ropeReset = false;
        }

        /*
        if (???)
        {
            // trigger rope unbroken when players reconnect or respawn

            AkSoundEngine.PostEvent("something", gameObject);

            ropeReset = true;
        }
        */
    }
}
