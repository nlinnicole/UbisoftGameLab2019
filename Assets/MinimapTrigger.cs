using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapTrigger : MonoBehaviour
{
    public GameObject Player;
    public int teamnumber;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Player = GameObject.FindGameObjectWithTag("Minimap");
            Player.GetComponent<WhichRoomAreYouIn>().TeamNumber = 1;
            Player.GetComponent<WhichRoomAreYouIn>().Team1Position = other.gameObject;
            Player.GetComponent<WhichRoomAreYouIn>().StartFindingDistance = true;
            Player.GetComponent<WhichRoomAreYouIn>().enabled = true;
        }
    }
}
