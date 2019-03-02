using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour
{
    public PlayerController player;

    public float feetMoveSpeed = 10;
    public float movingDistBetweenFeet = 1;
    public float stoppedDistBetweenFeet = 0.3f;
    public float walkingSpeed = 0.1f;

    public GameObject targetFootR;
    public GameObject targetFootL;

    public GameObject nextL;
    public GameObject nextR;

    public GameObject rightFoot;
    public GameObject leftFoot;

    public float stepSize;
    public float maxFootDistFromCenter;

    float stepDelay;
    public float stepDelayLength;

    public float feetHeight;

    bool targetFootRsTurn = true;

    Vector3 thisFeetHeight;

    void Start()
    {

    }

    void FixedUpdate()
    {


        if (player.isGrounded)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, player.groundDetectDistance, player.jumpLayerMask))
            {
               // feetHeight = hit.point.y;
            }
        }
        else
        {
        }


        stepDelay -= Time.deltaTime;


        thisFeetHeight = new Vector3(this.transform.position.x, feetHeight, this.transform.position.z);
        if(player.playerNumber ==2)
        {
            print(player.GetComponent<Rigidbody>().velocity.magnitude);
        }

        //if walking
        //if (player.GetComponent<Rigidbody>().velocity.magnitude > walkingSpeed)
        //{

        if(Vector3.Distance(rightFoot.transform.position, leftFoot.transform.position) < 0.1)
        {
            nextR.transform.position = transform.position + player.GetComponent<Rigidbody>().velocity.normalized * stepSize;
            targetFootR.transform.position = new Vector3(nextR.transform.position.x, feetHeight, nextR.transform.position.z);
        }
        else
        {
            //right foot
            nextR.transform.position = transform.position + player.GetComponent<Rigidbody>().velocity.normalized * stepSize;
            if (Vector3.Distance(targetFootR.transform.position, thisFeetHeight) > maxFootDistFromCenter
                || Vector3.Distance(leftFoot.transform.position, thisFeetHeight) < 0.1)
            {
                targetFootR.transform.position = new Vector3(nextR.transform.position.x, feetHeight, nextR.transform.position.z);
            }

            //left foot
            nextL.transform.position = transform.position + player.GetComponent<Rigidbody>().velocity.normalized * stepSize;
            if (Vector3.Distance(targetFootL.transform.position, thisFeetHeight) > maxFootDistFromCenter
                 || Vector3.Distance(rightFoot.transform.position, thisFeetHeight) < 0.1)
            {
                targetFootL.transform.position = new Vector3(nextL.transform.position.x, feetHeight, nextL.transform.position.z);
            }
        }



        //}
        //if stationary
        // else if (player.GetComponent<Rigidbody>().velocity.magnitude <= walkingSpeed)
        //{
        ////right foot
        //nextR.transform.position = transform.position + player.GetComponent<Rigidbody>().velocity;
        //if (Vector3.Distance(targetFootR.transform.position, transform.position) > maxFootDistFromCenter)
        //{
        //    targetFootR.transform.position = new Vector3(nextR.transform.position.x, feetHeight, nextR.transform.position.z);
        //}

        ////left foot
        //nextL.transform.position = transform.position + player.GetComponent<Rigidbody>().velocity;
        //if (Vector3.Distance(targetFootL.transform.position, transform.position) > maxFootDistFromCenter)
        //{
        //    targetFootL.transform.position = new Vector3(nextL.transform.position.x, feetHeight, nextL.transform.position.z);
        //}
        // }

        ////draw lines to closest
        targetFootR.GetComponent<LineRenderer>().SetPosition(0, targetFootR.transform.position);
        targetFootR.GetComponent<LineRenderer>().SetPosition(1, this.transform.position);

        targetFootL.GetComponent<LineRenderer>().SetPosition(0, targetFootL.transform.position);
        targetFootL.GetComponent<LineRenderer>().SetPosition(1, this.transform.position);

        rightFoot.transform.position = Vector3.MoveTowards(rightFoot.transform.position, targetFootR.transform.position, Time.deltaTime * feetMoveSpeed);
        leftFoot.transform.position = Vector3.MoveTowards(leftFoot.transform.position, targetFootL.transform.position, Time.deltaTime * feetMoveSpeed);

    }
    
}

