using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seesawSounds : MonoBehaviour
{

    float lastRotate = 0;
    bool dragStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        lastRotate = Mathf.Abs(gameObject.GetComponent<Transform>().rotation.z);
    }

    // Update is called once per frame
    void Update()
    {
        float thisRotate = Mathf.Abs(gameObject.GetComponent<Transform>().rotation.z);

        if (Mathf.Abs(thisRotate - lastRotate) >= 0.01)
        {
            lastRotate = thisRotate;
            if (!dragStarted)
            {
                AkSoundEngine.PostEvent("startRotate", gameObject);
                dragStarted = true;
            }

        }
        else if (Mathf.Abs(thisRotate - lastRotate) < 0.004 && dragStarted)
        {
            AkSoundEngine.PostEvent("stopRotate", gameObject);
            dragStarted = false;
            lastRotate = thisRotate;
        }

    }
}