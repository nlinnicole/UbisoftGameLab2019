using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneteredFinalPlatform : MonoBehaviour
{
    public GameObject MovingPlatform;
    public GameObject Parent;

    Transform position;
    public bool running = true;

    public int counter = 0;
    public float platformspeed = 1;
    public float acceleration = 0;
    public float factor = 0.01f;
    private void Start()
    {
        counter = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            counter++;
        }

        if (other.tag == "Player" && counter > 2) 
        {
            other.gameObject.GetComponent<PlayerController>().playerCamera.GetComponentInParent<CamPlayerFollow>().viewangle = 4;

        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (running)
        {
            if (other.tag == "Player" && counter > 2)
            {
                
                other.gameObject.transform.SetParent(Parent.transform);
                gameObject.transform.Translate(Vector3.forward * Time.deltaTime * platformspeed * acceleration);

                acceleration += Time.deltaTime * factor;

                Vector3 clampedPosition = transform.position;
                // Now we can manipulte it to clamp the y element
                clampedPosition.x = Mathf.Clamp(transform.position.x, -22f, 5f);
                // re-assigning the transform's position will clamp it
                transform.position = clampedPosition;

            }
        }

        
    }

    private void Update()
    {
        

    }


}
