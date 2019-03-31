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
        float sinTime = Mathf.Sin(Time.time);

        float angle = 90 * sinTime + offset; ;
        axeObject.transform.eulerAngles = new Vector3(angle, yAngle, zAngle);


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
}
