using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ropeSoundTriggers : MonoBehaviour
{
    bool ropebroken;
    bool ropeReset = true;
    

    void Start()
    {
    }

   
    void Update()
    {
        ropebroken = gameObject.GetComponent<RopeGenerator>().isBroken;

        if (ropebroken && ropeReset)
        {
            // trigger rope broken sound event 
            AkSoundEngine.PostEvent("ropeBroken", gameObject);
            ropeReset = false;
        }

        
        if (!ropeReset && !ropebroken)
        {
            // trigger rope unbroken when players reconnect or respawn

            AkSoundEngine.PostEvent("RopeNotBroken", gameObject);

            ropeReset = true;
        }
      
    }
}
