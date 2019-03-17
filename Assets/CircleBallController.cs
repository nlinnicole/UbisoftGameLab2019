using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBallController : MonoBehaviour
{
    //If possible, in the future lets add a controller that swaps players from each one.
    public GameObject players;
    Transform CurrentLocation;

    public GameObject exitdoor;

    int boardkillcounter = 0;

    int negativer = -1;

    float initialTimeLeft = 4f;

    float lerpvalue;

    public Rigidbody rb;

    public Transform centerPoint;

    Vector3 attackDirection;

    private float timeLeft =5f;

    bool startattacking = false;

    bool triggeredNeg = true;
    bool ReturnToCenter;
    bool directionFinder = true;
    // Movement speed in units/sec.
    public float speed = 1.0F;
    public float backspeed = 0.5f;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;
    private float journeyLengthBack;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        // Keep a note of the time the movement started.
       

        //InvokeRepeating("attackPlayer", 2, 4);

        attackPlayer();
    }
    
    void whenGoingBack()
    {
        startTime = Time.time;

        // Calculate the journey length.
        journeyLengthBack = Vector3.Distance(gameObject.transform.position, centerPoint.transform.position);
    }


    // Update is called once per frame
    void Update()
    {
        if (startattacking)
        {
            if (ReturnToCenter)
            {
                whenGoingBack();
                Debug.Log("returning");
                returnToCenter();
                timeLeft -= Time.deltaTime;
                if (timeLeft < 0)
                {
                    startTime = 0;
                    ReturnToCenter = false;
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

                if (directionFinder)
                {
                    attackDirection = transform.forward;
                    directionFinder = false;
                }
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

    void attackPlayer()
    {
        transform.position += attackDirection * negativer * Time.deltaTime * speed;


    }

    void returnToCenter()
    {
        startTime += Time.deltaTime;
        lerpvalue = (startTime / backspeed);
        transform.position = Vector3.Lerp(gameObject.transform.position,centerPoint.position, lerpvalue);

        Debug.Log("Running");        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            ReturnToCenter = true;
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
}
