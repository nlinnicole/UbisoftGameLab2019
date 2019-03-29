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

    public int gemCount;

    //Variables for health
    public Transform currentRespawnArea;
    public Health player1Health;
    public Health player2Health;
    public float cooldownBeforeRespawn = 2f;
    public bool respawnInCurrent = true;

    public GameObject cam;


    private void Start()
    {
        if (roomGen == null){
          roomGen = GameObject.Find("RoomGenerator").GetComponent<RoomGenerator>();
        }

        player1HeadTopPos = player1HeadTop.transform.localPosition;
        player2HeadTopPos = player2HeadTop.transform.localPosition;

        player1Health = player1.GetComponent<Health>();
        player2Health = player2.GetComponent<Health>();
    }

    void FixedUpdate()
    {
        if (rope.isBroken)
        {
            //if (!player1Health.onOxygen)
            //{
                player1Health.onOxygen = true;
                //player1Health.bar.gameObject.SetActive(true);
            //}

            //if (!player2Health.onOxygen)
            //{
            //    player2Health.onOxygen = true;
            //    //player1Health.bar.gameObject.SetActive(true);
            //}

        }

        if ((!player1Health.alive || !player2Health.alive) || (player1.GetComponent<PlayerController>().inDeathZone && player2.GetComponent<PlayerController>().inDeathZone))
        {
            Transform[] children = rope.GetComponentsInChildren<Transform>();
            foreach (Transform child in children)
            {
                if (child.name != "Rope")
                    GameObject.Destroy(child.gameObject);
            }

            player1Health.Reset();
            player2Health.Reset();

            respawn();
        }

    }

    public void respawn()
    {

        player1.GetComponent<PlayerController>().inDeathZone = false;
        player2.GetComponent<PlayerController>().inDeathZone = false;

        if (teamNumber == 1)
        {
            if(roomGen.team1CurrentRoom != -1)
            {
                player1.transform.parent = transform.parent;
                player2.transform.parent = transform.parent;
                player1.GetComponent<Rigidbody>().velocity = Vector3.zero;
                player2.GetComponent<Rigidbody>().velocity = Vector3.zero;
                player1HeadTop.transform.localPosition = player1HeadTopPos;
                player2HeadTop.transform.localPosition = player2HeadTopPos;
                this.transform.parent.transform.position = roomGen.Team1Rooms[roomGen.team1CurrentRoom].GetComponent<Room>().respawnPoint.transform.position;

                if (respawnInCurrent){
                  currentRespawnArea = roomGen.GetCurrentRespawn(teamNumber);

                  player1.transform.localPosition = new Vector3(currentRespawnArea.localPosition.x - 4, currentRespawnArea.localPosition.y, currentRespawnArea.localPosition.z);
                  player2.transform.localPosition = new Vector3(currentRespawnArea.localPosition.x + 4, currentRespawnArea.localPosition.y, currentRespawnArea.localPosition.z);
                } else {
                  player1.transform.localPosition = new Vector3(-4, 0, 0);
                  player2.transform.localPosition = new Vector3(4, 0, 0);
                }
            }
            else
            {
                player1.transform.parent = transform.parent;
                player2.transform.parent = transform.parent;
                player1.GetComponent<Rigidbody>().velocity = Vector3.zero;
                player2.GetComponent<Rigidbody>().velocity = Vector3.zero;
                player1HeadTop.transform.localPosition = player1HeadTopPos;
                player2HeadTop.transform.localPosition = player2HeadTopPos;
                this.transform.parent.transform.position = roomGen.team1StartRoom.GetComponent<Room>().respawnPoint.transform.position;
                player1.transform.localPosition = new Vector3(-4, 0, 0);
                player2.transform.localPosition = new Vector3(4, 0, 0);
            }
            rope.generate();

            rope.isBroken = false;
            rope.startedGas = false;
            cam.GetComponentInParent<CamPlayerFollow>().player1Dead = false;
            cam.GetComponentInParent<CamPlayerFollow>().player2Dead = false;
        }
        if (teamNumber == 2)
        {
            if(roomGen.team2CurrentRoom != -1)
            {
                player1.transform.parent = transform.parent;
                player2.transform.parent = transform.parent;
                player1.GetComponent<Rigidbody>().velocity = Vector3.zero;
                player2.GetComponent<Rigidbody>().velocity = Vector3.zero;
                player1HeadTop.transform.localPosition = player1HeadTopPos;
                player2HeadTop.transform.localPosition = player2HeadTopPos;
                this.transform.parent.transform.position = roomGen.Team2Rooms[roomGen.team2CurrentRoom].GetComponent<Room>().respawnPoint.transform.position;


                if (respawnInCurrent){
                  currentRespawnArea = roomGen.GetCurrentRespawn(teamNumber);

                  player1.transform.localPosition = new Vector3(currentRespawnArea.localPosition.x - 4, currentRespawnArea.localPosition.y, currentRespawnArea.localPosition.z);
                  player2.transform.localPosition = new Vector3(currentRespawnArea.localPosition.x + 4, currentRespawnArea.localPosition.y, currentRespawnArea.localPosition.z);
                } else {
                  this.transform.parent.transform.position = roomGen.Team2Rooms[roomGen.team2CurrentRoom].GetComponent<Room>().respawnPoint.transform.position;

                  player1.transform.localPosition = new Vector3(-4, 0, 0);
                  player2.transform.localPosition = new Vector3(4, 0, 0);
                }
            }
            else
            {
                player1.transform.parent = transform.parent;
                player2.transform.parent = transform.parent;
                player1.GetComponent<Rigidbody>().velocity = Vector3.zero;
                player2.GetComponent<Rigidbody>().velocity = Vector3.zero;
                player1HeadTop.transform.localPosition = player1HeadTopPos;
                player2HeadTop.transform.localPosition = player2HeadTopPos;
                this.transform.parent.transform.position = roomGen.team2StartRoom.GetComponent<Room>().respawnPoint.transform.position;
                player1.transform.localPosition = new Vector3(-4, 0, 0);
                player2.transform.localPosition = new Vector3(4, 0, 0);
            }
            rope.generate();

            rope.isBroken = false;
            rope.startedGas = false;

            cam.GetComponentInParent<CamPlayerFollow>().player1Dead = false;
            cam.GetComponentInParent<CamPlayerFollow>().player2Dead = false;
        }

        cam.GetComponent<Animator>().ResetTrigger("isDying");


    }
}
