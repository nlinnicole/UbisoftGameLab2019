using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * A block that the player can pick up and drag along
 */
public class MovingBlock : MonoBehaviour
{
    //the player which picked up the block
    private GameObject player;
    private Vector3 distance;

    //the height of the block 
    private float height; 

    void Start()
    {
        height = (GetComponent<Collider>().bounds.size.y)/2;
    }

    void Update()
    {
        if(player != null)
        {
            transform.position = new Vector3(player.transform.position.x + distance.x, height, player.transform.position.z + distance.z);
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
