using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public int teamNumber;
    public RoomGenerator roomGen;
    public RopeGenerator rope;
    public GameObject player1;
    public GameObject player2;
    public GameObject player1HeadTop;
    Vector3 player1HeadTopPos;
    public GameObject player2HeadTop;
    Vector3 player2HeadTopPos;


    private void Start()
    {
        player1HeadTopPos = player1HeadTop.transform.localPosition;
        player2HeadTopPos = player2HeadTop.transform.localPosition;
    }

    void FixedUpdate()
    {
        if(rope.isBroken)
        {

        }
    }

    public void respawn()
    {
        if(teamNumber == 1)
        {
            if(roomGen.team1CurrentRoom != -1)
            {
                player1.GetComponent<Rigidbody>().velocity = Vector3.zero;
                player2.GetComponent<Rigidbody>().velocity = Vector3.zero;
                player1HeadTop.transform.localPosition = player1HeadTopPos;
                player2HeadTop.transform.localPosition = player2HeadTopPos;
                this.transform.parent.transform.position = roomGen.Team1Rooms[roomGen.team1CurrentRoom].GetComponent<Room>().respawnPoint.transform.position;
                player1.transform.localPosition = new Vector3(-4, 0, 0);
                player2.transform.localPosition = new Vector3(4, 0, 0);
            }
            else
            {
                player1.GetComponent<Rigidbody>().velocity = Vector3.zero;
                player2.GetComponent<Rigidbody>().velocity = Vector3.zero;
                player1HeadTop.transform.localPosition = player1HeadTopPos;
                player2HeadTop.transform.localPosition = player2HeadTopPos;
                this.transform.parent.transform.position = roomGen.team1StartRoom.GetComponent<Room>().respawnPoint.transform.position;
                player1.transform.localPosition = new Vector3(-4, 0, 0);
                player2.transform.localPosition = new Vector3(4, 0, 0);
            }
            rope.generate();
        }
        if (teamNumber == 2)
        {
            if(roomGen.team2CurrentRoom != -1)
            {
                player1.GetComponent<Rigidbody>().velocity = Vector3.zero;
                player2.GetComponent<Rigidbody>().velocity = Vector3.zero;
                player1HeadTop.transform.localPosition = player1HeadTopPos;
                player2HeadTop.transform.localPosition = player2HeadTopPos;
                this.transform.parent.transform.position = roomGen.Team2Rooms[roomGen.team2CurrentRoom].GetComponent<Room>().respawnPoint.transform.position;
                player1.transform.localPosition = new Vector3(-4, 0, 0);
                player2.transform.localPosition = new Vector3(4, 0, 0);
            }
            else
            {
                player1.GetComponent<Rigidbody>().velocity = Vector3.zero;
                player2.GetComponent<Rigidbody>().velocity = Vector3.zero;
                player1HeadTop.transform.localPosition = player1HeadTopPos;
                player2HeadTop.transform.localPosition = player2HeadTopPos;
                this.transform.parent.transform.position = roomGen.team2StartRoom.GetComponent<Room>().respawnPoint.transform.position;
                player1.transform.localPosition = new Vector3(-4, 0, 0);
                player2.transform.localPosition = new Vector3(4, 0, 0);
            }
            rope.generate();
        }
    }
}
