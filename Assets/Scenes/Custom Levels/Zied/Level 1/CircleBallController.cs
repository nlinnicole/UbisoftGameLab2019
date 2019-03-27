﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBallController : MonoBehaviour
{
    //If possible, in the future lets add a controller that swaps players from each one.
    public GameObject players;

    public Animator RaiseDoor;

    int boardkillcounter = 0;

    bool enteredroom = false;

    Transform LastPointHit;

    public Rigidbody rb;

    public Transform centerPoint;

    Vector3 attackDirection;

    private float fraction = 0;

    private float timeLeft =2f;

    bool startattacking = false;


    bool centered = true;
    bool ReturnToCenter = false;

    // Movement speed in units/sec.
    public float speed = 1.0F;
    public float backspeed = 0.5f;

    // Time when the movement started.




    // Start is called before the first frame update
    void Start()
    {
        int boardkillcounter = 0;
        players = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void returnBallToCenter()
    {
        if (fraction < 2)
        {
            Debug.Log("Returning to Center");
            fraction += Time.deltaTime * 0.2f;
            gameObject.transform.position = Vector3.Lerp(LastPointHit.position, centerPoint.transform.position, fraction);
        }
    }


    /*
    // Update is called once per frame
    void Update()
    {
        if (startattacking)
        {
            if (ReturnToCenter)
            {
                if (FirstHit)
                {
                    LastPointHit = gameObject.transform;
                    FirstHit = false;
                    Debug.Log("Hit Wall");
                }
                LastPointHit = gameObject.transform;
                whenGoingBack();
                Debug.Log("returning");
                timeLeft -= Time.deltaTime;
                if (timeLeft < 0)
                {
                    startTime = 0;
                    ReturnToCenter = false;
                    FirstHit = true;
                    directionFinder = true;
                    timeLeft = 4;
                    if (triggeredNeg)
                    {
                        negativer = negativer * -1;
                        triggeredNeg = false;
                    }
                }
            }
            else
            {


                Debug.Log("Attacking");
                attackPlayer();
            }
        }


        if (boardkillcounter == 1)
        {
            exitdoor.transform.position = exitdoor.transform.position + Vector3.up * 0.2f;

        }

        transform.LookAt(players.gameObject.transform.position);

        initialTimeLeft -= Time.deltaTime;
        if(initialTimeLeft < 0)
        {
            startattacking = true;
        }

    }
    */

    private void Update()
    {
        if (enteredroom)
        {
            Debug.Log("Centered :" + centered);
            Debug.Log("Returning to Center" + ReturnToCenter);

            if (ReturnToCenter)
            {
                returnBallToCenter();

            timeLeft -= Time.deltaTime;
            if(timeLeft < 0f)
            {
                Debug.Log(timeLeft);

                startattacking = true;
                timeLeft = 4f;
            }


            if (startattacking)
            {
                Debug.Log("Attacking");
                attackPlayer();
            }
            else
            {
                transform.LookAt(players.gameObject.transform.position);
                attackDirection = transform.forward;

            }
          }


        }

        CheckBoardKills();

    }

    void CheckBoardKills()
    {
        if(boardkillcounter > 2)
        {
            RaiseDoor.SetBool("OpenExit", true);
            Destroy(gameObject, 1);
        }
    }


    void attackPlayer()
    {
        transform.position += attackDirection * Time.deltaTime * speed;

    }



    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            Debug.Log("Hit a wall");
            ReturnToCenter = true;
            LastPointHit = gameObject.transform;
            startattacking = false;

        }

        if (collision.gameObject.tag == "Board")
        {
            boardkillcounter++;
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "Player")
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Center")
        {
            fraction = 0;
            centered = true;
            ReturnToCenter = false;
        }

        if(other.tag == "Player")
        {
            enteredroom = true;
        }

    }


}
