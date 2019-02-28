using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour
{
    public PlayerController player;

    public GameObject foot;
    public GameObject rightFoot;
    public GameObject leftFoot;

    public float stepSize;
    float stepDelay;
    public float stepDelayLength;

    public float feetHeight;

    bool rightFootsTurn = true;

    Vector3 rightLastPos = Vector3.zero;
    Vector3 leftLastPos = Vector3.zero;

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

        //rounding feet positions for steps
        //if(stepDelay <= 0 && player.velocity.magnitude > 0.001 )
        //{
        //    if (rightFootsTurn)
        //    {
        //        rightFoot.transform.position = new Vector3(Mathf.Round((this.transform.position.x + player.feetVelocity.z/2)), feetHeight, Mathf.Round((this.transform.position.z + player.feetVelocity.z/2)));
        //    }
        //    else
        //    {
        //        leftFoot.transform.position = new Vector3(Mathf.Round((this.transform.position.x + player.feetVelocity.x/2)), feetHeight, Mathf.Round((this.transform.position.z + player.feetVelocity.z/2)));
        //    }

        //    rightFootsTurn = !rightFootsTurn;
        //    stepDelay = stepDelayLength;
        //}


        rightFoot.transform.position = (new Vector3(
            Mathf.Round(this.transform.position.x), 
            feetHeight, 
            Mathf.Round(this.transform.position.z)));

        leftFoot.transform.position = (new Vector3(
            Mathf.Round(this.transform.position.x) + (0.2f * player.transform.right.x) + (0.5f * player.transform.forward.x), 
            feetHeight, 
            Mathf.Round(this.transform.position.z) + (0.2f * player.transform.right.z) + (0.5f * player.transform.forward.z)));

        ////draw lines to closest
        rightFoot.GetComponent<LineRenderer>().SetPosition(0, rightFoot.transform.position);
        rightFoot.GetComponent<LineRenderer>().SetPosition(1, this.transform.position);

        leftFoot.GetComponent<LineRenderer>().SetPosition(0, leftFoot.transform.position);
        leftFoot.GetComponent<LineRenderer>().SetPosition(1, this.transform.position);




    }
}

