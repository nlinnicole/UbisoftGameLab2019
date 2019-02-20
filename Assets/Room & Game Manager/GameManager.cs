using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int baseRoomSize = 40;
    public int amountOfRooms = 10;

    public GameObject team1;
    public GameObject team1StartRoom;
    public GameObject Team1Space;
    public int Team1RoomNumber = 0;
    
    public GameObject team2;
    public GameObject team2StartRoom;
    public GameObject Team2Space;
    public int Team2RoomNumber = 0;

    public GameObject[] roomArray;
    public GameObject[] Team1Rooms;
    public GameObject[] Team2Rooms;

    GameObject team1Rooms;
    GameObject team2Rooms;

    public GameObject team1Start;
    public GameObject team2Start;

    void Start()
    {
        GenerateRooms(10);
    }

    public void GenerateRooms(int amount)
    {
        //delete all old rooms
        for(int i = 0; i < Team1Rooms.Length; i++)
        {
            DestroyImmediate(Team1Rooms[i]);
            DestroyImmediate(Team2Rooms[i]);
        }

        DestroyImmediate(team1Rooms);
        DestroyImmediate(team2Rooms);

        team1Rooms = new GameObject();
        team2Rooms = new GameObject();
        team1Rooms.name = "Team1 Rooms";
        team2Rooms.name = "Team2 Rooms";

        team1Start = Instantiate(team1StartRoom);
        team2Start = Instantiate(team2StartRoom, new Vector3(-baseRoomSize, 0, 0), Quaternion.identity);

        Team1Rooms = new GameObject[(amount * 2)-1];
        Team2Rooms = new GameObject[(amount * 2)-1];

        for (int i = 1, k = 1; i < amount * 2; i+=2, k++)
        {
            if(i < (amount * 2) -1)
            {
                //randomly choose a room
                GameObject roomToBuild = roomArray[Random.Range(0, roomArray.Length)];
                //create room
                Team1Rooms[i] = Instantiate(roomToBuild, new Vector3(0, 0, baseRoomSize * k), Quaternion.identity, team1Rooms.transform) as GameObject;
                //change name
                Team1Rooms[i].gameObject.name += " (Option 1)";
                Team1Rooms[i].SetActive(false);


                roomToBuild = roomArray[Random.Range(0, roomArray.Length)];
                Team1Rooms[i+1] = Instantiate(roomToBuild, new Vector3(0, 0, baseRoomSize * k), Quaternion.identity, team1Rooms.transform) as GameObject;
                Team1Rooms[i+1].gameObject.name += " (Option 2)";
                Team1Rooms[i+1].SetActive(false);


                //team2
                roomToBuild = roomArray[Random.Range(0, roomArray.Length)];
                Team2Rooms[i] = Instantiate(roomToBuild, new Vector3(-baseRoomSize, 0, baseRoomSize * k), Quaternion.identity, team2Rooms.transform) as GameObject;
                Team2Rooms[i].gameObject.name += " (Option 1)";
                Team2Rooms[i].SetActive(false);

                roomToBuild = roomArray[Random.Range(0, roomArray.Length)];
                Team2Rooms[i+1] = Instantiate(roomToBuild, new Vector3(-baseRoomSize, 0, baseRoomSize * k), Quaternion.identity, team2Rooms.transform) as GameObject;
                Team2Rooms[i+1].gameObject.name += " (Option 2)";
                Team2Rooms[i+1].SetActive(false);

            }
        }
    }

    void FixedUpdate()
    {
        if (Team1RoomNumber == 0)
        {
            if (team1Start.GetComponent<Room>().door1Trigger.GetComponent<DoorTrigger>().doorChosen)
            {
                Team1Rooms[Team1RoomNumber + 1].SetActive(true);
                Team1RoomNumber++;
            }
            else if (team1Start.GetComponent<Room>().door2Trigger.GetComponent<DoorTrigger>().doorChosen)
            {
                Team1Rooms[Team1RoomNumber + 2].SetActive(true);
                Team1RoomNumber++;
            }
        }

        if (Team2RoomNumber == 0)
        {
            if (team2Start.GetComponent<Room>().door1Trigger.GetComponent<DoorTrigger>().doorChosen)
            {
                Team2Rooms[Team2RoomNumber + 1].SetActive(true);
                Team2RoomNumber++;
            }
            else if (team2Start.GetComponent<Room>().door2Trigger.GetComponent<DoorTrigger>().doorChosen)
            {
                Team2Rooms[Team2RoomNumber + 2].SetActive(true);
                Team2RoomNumber++;
            }
        }


        if (Team1RoomNumber > 0)
        {
            //check which room the players choose
            //team 1
            if (Team1Rooms[Team1RoomNumber].GetComponent<Room>().door1Trigger.GetComponent<DoorTrigger>().doorChosen)
            {
                Team1Rooms[Team1RoomNumber + 1].SetActive(true);
                Team1RoomNumber += 2;
            }
            else if (Team1Rooms[Team1RoomNumber].GetComponent<Room>().door2Trigger.GetComponent<DoorTrigger>().doorChosen)
            {
                Team1Rooms[Team1RoomNumber + 2].SetActive(true);
                Team1RoomNumber += 2;
            }
        }

        if (Team2RoomNumber > 0)
        {
            //team 2
            if (Team2Rooms[Team2RoomNumber].GetComponent<Room>().door1Trigger.GetComponent<DoorTrigger>().doorChosen)
            {
                Team2Rooms[Team2RoomNumber + 1].SetActive(true);
                Team2RoomNumber += 2;
            }
            else if (Team2Rooms[Team2RoomNumber].GetComponent<Room>().door2Trigger.GetComponent<DoorTrigger>().doorChosen)
            {
                Team2Rooms[Team2RoomNumber + 2].SetActive(true);
                Team2RoomNumber += 2;
            }
        }
    }
}
