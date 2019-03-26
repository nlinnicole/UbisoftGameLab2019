using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragCubeSounds : MonoBehaviour
{

    float lastXPos = 0;
    float lastYPos = 0;
    bool dragStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        lastXPos = Mathf.Abs(gameObject.GetComponent<Transform>().position.x);
        lastYPos = Mathf.Abs(gameObject.GetComponent<Transform>().position.z);
    }

    // Update is called once per frame
    void Update()
    {
        float thisXPos = Mathf.Abs(gameObject.GetComponent<Transform>().position.x);
        float thisYPos = Mathf.Abs(gameObject.GetComponent<Transform>().position.z);

        if( ( Mathf.Abs(thisXPos - lastXPos) >= 0.05 || Mathf.Abs(thisYPos - lastYPos) >= 0.05) )
        {
            
            lastXPos = thisXPos;
            lastYPos = thisYPos;
            if (!dragStarted)
            {
                AkSoundEngine.PostEvent("startDrag", gameObject);
                dragStarted = true;
            }
            
        }
        else if ((Mathf.Abs(thisXPos - lastXPos) < 0.04 || Mathf.Abs(thisYPos - lastYPos) < 0.04) &&  dragStarted)
        {
            AkSoundEngine.PostEvent("stopDrag", gameObject);
            dragStarted = false;
            lastXPos = thisXPos;
            lastYPos = thisYPos;
        }

    }
}
