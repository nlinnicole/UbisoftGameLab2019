using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUps : MonoBehaviour
{
    public GameObject player;
    public float displayHeight = 2;
    public GameObject headJoint;
    public float forceForSwearing = 200;
    public float currentForce = 0;

    public bool readyToSwear = true;
    float swearTime = 0;
    public float swearCountdown = 0;
    bool swearing = false;

    public GameObject text;
    public GameObject bg;

    void FixedUpdate()
    {
        currentForce = headJoint.GetComponent<SpringJoint>().currentForce.magnitude;
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + displayHeight, player.transform.position.z);
        if (headJoint.GetComponent<SpringJoint>().currentForce.magnitude > forceForSwearing && readyToSwear)
        {
            readyToSwear = false;
            swearing = true;
            swearCountdown = 10;
            swearTime = 0;
            
            // post the appropriate grunt
            AkSoundEngine.PostEvent("gruntP"+ player.GetComponent<PlayerController>().playerNumber, player);
        }

        if(swearing)
        {
            swearTime += Time.deltaTime;
            text.SetActive(true);
            bg.SetActive(true);
        }
        if (swearing && swearTime > 0.7f)
        {
            text.SetActive(false);
            bg.SetActive(false);
            swearCountdown -= Time.deltaTime;
        }
        if(swearCountdown < 0 && swearing)
        {
            swearing = false;
            readyToSwear = true;
        }
    }

    public Camera m_Camera;

    //Orient the camera after all movement is completed this frame to avoid jittering
    void LateUpdate()
    {
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
            m_Camera.transform.rotation * Vector3.up);
    }

    void swear()
    {
 

    }

}
