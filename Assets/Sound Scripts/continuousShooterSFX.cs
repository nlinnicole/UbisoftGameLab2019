using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class continuousShooterSFX : MonoBehaviour
{
    float timer;
    float timeCounter;
    // Start is called before the first frame update
    void Start()
    {
        timeCounter = Time.time;
       
    }

    // Update is called once per frame
    void Update()
    {
        timer = gameObject.GetComponent<ContinuousProjectile>().timer;

        if (Time.time > timeCounter + timer)
        {
            AkSoundEngine.PostEvent("continuousShot", gameObject);
            timeCounter = Time.time;

        }
    }
}
