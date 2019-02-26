using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    private GameObject player;
    private Vector3 distance;
    void Start()
    {
        
    }

    void Update()
    {
        if(player != null)
        {
            transform.position = player.transform.position + distance;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(player== null)
        {
            Debug.Log("CUBE !");
            player = collision.gameObject;
            distance = player.transform.position - transform.position;
        }

    }
}
