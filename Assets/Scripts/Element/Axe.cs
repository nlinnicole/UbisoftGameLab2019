using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField]
    private GameObject axeObject;
    [SerializeField]
    private float rotateSpeed = 0.5f;
    [SerializeField]
    private int yAngle = 90;
    [SerializeField]
    private int zAngle = 90;
    [SerializeField]
    private int offset = -90;
    bool swingTriggered = true;

    private GameObject axe;

    void Start()
    {
        //axe = Instantiate(axeObject, this.transform.position, Quaternion.identity);
    }

    void Update()
    {
        // save time value since it'll be used a few more times
        float sinTime = Mathf.Sin(Time.time);

        float angle = 60 * sinTime;
        axe.transform.eulerAngles = new Vector3(0, 0, angle);


        // SOUND TRIGGER HERE  
        if (sinTime > 0 && !swingTriggered)
        {
            swingTriggered = true;
            AkSoundEngine.PostEvent("startSwingingAxe", gameObject);
        }
        if (sinTime < 0 && swingTriggered)
        {
            AkSoundEngine.PostEvent("startSwingingAxe", gameObject);
            swingTriggered = false;
        }
    }
