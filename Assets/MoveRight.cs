using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : MonoBehaviour
{
    public GameObject MovingPlatform;
    public float strafespeed = 5f;
    public bool reachedend = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (!reachedend)
        {
            if (other.tag == "Player" && gameObject.GetComponentInParent<EneteredFinalPlatform>().counter > 2)
            {
                MovingPlatform.transform.Translate(Vector3.right * Time.deltaTime * strafespeed * MovingPlatform.GetComponent<EneteredFinalPlatform>().acceleration);
            }
        }
        
    }
}

