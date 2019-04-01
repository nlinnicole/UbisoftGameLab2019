/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSoundTriggers : MonoBehaviour
{
    BGMchanges bgmManager;

    bool walkStarted = false;
    bool isWalking = false;
    bool rollStarted = false;
    bool currentlyRolling = false;
    float totalVelocity = 0;
    float rollMod = 0;
    int playerNumber;
    bool rollStopped = false;

    Vector3 playerVelocity;


    void Start()
    {
            playerNumber = gameObject.GetComponent<PlayerController>().playerNumber;
    }

    // Update is called once per frame
    void Update()
    {

        // -------------- ROLL ----------------------------
        currentlyRolling = gameObject.GetComponent<PlayerController>().isRolling;
        if(playerNumber==1){
           rollMod = gameObject.GetComponent<PlayerController>().rollMod;
        }
        else  if(playerNumber==2){
           rollMod = gameObject.GetComponent<PlayerController>().rollMod2;
        }
     

        if (currentlyRolling && !rollStarted)
        {
            // trigger roll start event
            AkSoundEngine.PostEvent("rollStart", gameObject);
            rollStarted = true;

            GameObject.FindWithTag("RoomGenerator").GetComponent<BGMchanges>().SetRandomVoiceState();

        }
        if (rollMod == 0 && rollStarted && !rollStopped)
        {
            // trigger roll stop event
            AkSoundEngine.PostEvent("rollStop", gameObject);
            rollStopped = true;

        }
        if (rollStarted && !currentlyRolling)
        {
            rollStarted = false;
            rollStopped = false;
        }



        playerVelocity = Vector3.zero;
        playerVelocity = gameObject.GetComponent<PlayerController>().velocity;

        totalVelocity = System.Math.Abs(playerVelocity.x) + System.Math.Abs(playerVelocity.z);
        //  Debug.Log(( totalVelocity ) / 100);


        // -------------- SEND VELOCITY TO WWISE ----------------------------
        AkSoundEngine.SetRTPCValue("velocity", (totalVelocity) / 100, gameObject);


        // -------------- STEPS ----------------------------


        //movement
        if (
        Input.GetAxisRaw("Horizontal" + playerNumber) > 0
            || Input.GetAxisRaw("Horizontal" + playerNumber) < 0
            || Input.GetAxisRaw("Vertical" + playerNumber) > 0
            || Input.GetAxisRaw("Vertical" + playerNumber) < 0
             )
        {
            if (!walkStarted)
            {
                // trigger start steps event 
                walkStarted = true;
                AkSoundEngine.PostEvent("startSteps", gameObject);
                // Debug.Log("steps started");
            }
        }

        else if (walkStarted)
        {
            // trigger stop steps event
            walkStarted = false;
            AkSoundEngine.PostEvent("stopSteps", gameObject);
        }
    }
}
*/