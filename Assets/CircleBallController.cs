using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBallController : MonoBehaviour
{
    //If possible, in the future lets add a controller that swaps players from each one.
    public GameObject players;
    Transform CurrentLocation;

    int negativer = -1;

    public Rigidbody rb;

    public Transform centerPoint;

    Vector3 attackDirection;

    private float timeLeft =5f;

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
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(gameObject.transform.position, gameObject.transform.position + transform.forward * 5);
        //InvokeRepeating("attackPlayer", 2, 4);

        attackPlayer();
    }

    // Update is called once per frame
    void Update()
    {

        if (ReturnToCenter)
        {
            Debug.Log("returning");
            returnToCenter();
            timeLeft -= Time.deltaTime;
            if(timeLeft < 0)
            {
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



        transform.LookAt(players.gameObject.transform.position);


        
        
            

        


    }

    void attackPlayer()
    {
        transform.position += attackDirection * negativer * Time.deltaTime * speed;


    }

    void returnToCenter()
    {


        journeyLengthBack = Vector3.Distance(gameObject.transform.position, centerPoint.position);


        // Distance moved = time * speed.
        float distCovered = (Time.time - startTime) * backspeed;

        // Fraction of journey completed = current distance divided by total distance.
        float fracJourney = distCovered / journeyLengthBack;

        // Set our position as a fraction of the distance between the markers.
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, centerPoint.position, fracJourney);

        

        

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            ReturnToCenter = true;

        }

        if (collision.gameObject.tag == "Board")
        {
            Destroy(collision.gameObject);
        }
    }
}
