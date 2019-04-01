using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneteredFinalPlatform : MonoBehaviour
{
    public GameObject MovingPlatform;
    public GameObject Parent;
    GameObject Player;

    public float minconstraintx;
    public float maxconstraintx;


    Transform position;

    public bool running = true;
    public bool reachedend = false;

    public int counter = 0;
    public float platformspeed = 1;
    public float acceleration = 0;
    public float factor = 0.01f;

    public GameObject player;

    Vector3 startPos;

    private void Start()
    {
        counter = 0;
        startPos = this.transform.position;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player = other.gameObject;
            counter++;
        }

        if (other.tag == "Player" && counter > 2) 
        {
            if (!reachedend)
            {
                other.gameObject.GetComponent<PlayerController>().playerCamera.GetComponentInParent<CamPlayerFollow>().viewangle = 4;
                Player = other.gameObject;
            }
            
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (running)
        {
            if (!reachedend)
            {
                if (other.tag == "Player" && counter > 2)
                {

                    other.gameObject.transform.SetParent(Parent.transform);
                    gameObject.transform.Translate(Vector3.forward * Time.deltaTime * platformspeed * acceleration);

                    acceleration += Time.deltaTime * factor;

                    Vector3 clampedPosition = transform.position;
                    // Now we can manipulte it to clamp the y element
                    clampedPosition.x = Mathf.Clamp(transform.position.x, minconstraintx, maxconstraintx);
                    // re-assigning the transform's position will clamp it
                    transform.position = clampedPosition;

                }
            }

        }

        
    }

    void SlowDown()
    {

    }

    private void Update()
    {
        if(player.GetComponent<PlayerController>().rope.isBroken)
        {
            this.transform.position = startPos;
        }


        if (reachedend)
        {
            gameObject.transform.Translate(new Vector3(0,0,0));

        }
    }


}
