using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoomGenerator : MonoBehaviour
{
    public int baseRoomSize = 40;
    public int amountOfRooms = 10;

    [Header("Team 1")]
    public GameObject team1;
    public GameObject team1StartRoom;
    public GameObject Team1Space;

    [Header("Team 2")]
    public GameObject team2;
    public GameObject team2StartRoom;
    public GameObject Team2Space;

    [Header("Rooms")]
    GameObject[] roomArray;

    public GameObject[] Team1Rooms;
    int[,] team1RoomRefs;
    public int Team1RoomNumber = 0;

    public GameObject[] Team2Rooms;
    int[,] team2RoomRefs;
    public int Team2RoomNumber = 0;

    GameObject team1Rooms;
    GameObject team2Rooms;

    //public GameObject team1Start;
    //public GameObject team2Start;

    bool team1InFirstRoom = true;
    bool team2InFirstRoom = true;

    public GameObject baseRoom;

    void Start()
    {
        GenerateRooms(10);
    }

    public void GenerateRooms(int amount)
    {
        roomArray = Resources.LoadAll("Rooms", typeof(GameObject)).Cast<GameObject>().ToArray();
        if (roomArray.Length == 0)
        {
            roomArray = new GameObject[1];
            roomArray[0] = baseRoom;
        }

        //delete all old rooms
        for (int i = 0; i < Team1Rooms.Length; i++)
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

        //team1Start = Instantiate(team1StartRoom);
        //team2Start = Instantiate(team2StartRoom, new Vector3(-baseRoomSize, 0, 0), Quaternion.identity);

        Team1Rooms = new GameObject[(amount * 2)+1];
        Team2Rooms = new GameObject[(amount * 2)+1];

        team1RoomRefs = new int[amount, 2];
        team2RoomRefs = new int[amount, 2];

        Team1RoomNumber = 0;
        Team2RoomNumber = 0;

        for (int i = 0, k = 1; i < amount * 2; i += 2, k++)
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

    short adderTeam1 = 0;
    short adderTeam2 = 0;
    void FixedUpdate()
    {
        if(team1InFirstRoom)
        {
            if (team1StartRoom.GetComponent<Room>().door1Trigger.GetComponent<DoorTrigger>().doorChosen)
            {
                Team1Rooms[0].SetActive(true);
                team1InFirstRoom = false;
            }
            else if (team1StartRoom.GetComponent<Room>().door2Trigger.GetComponent<DoorTrigger>().doorChosen)
            {
                Team1Rooms[1].SetActive(true);
                team1InFirstRoom = false;
                adderTeam1 = 1;
            }
        }
        else
        {
            //check which room the players choose
            if (Team1Rooms[Team1RoomNumber + adderTeam1].GetComponent<Room>().door1Trigger.GetComponent<DoorTrigger>().doorChosen)
            {
                Team1RoomNumber += 2;
                adderTeam1 = 0;
                Team1Rooms[Team1RoomNumber + adderTeam1].SetActive(true);
            }
            else if (Team1Rooms[Team1RoomNumber + adderTeam1].GetComponent<Room>().door2Trigger.GetComponent<DoorTrigger>().doorChosen)
            {
                Team1RoomNumber += 2;
                adderTeam1 = 1;
                Team1Rooms[Team1RoomNumber + adderTeam1].SetActive(true);
            }

        }

        //team2
        if (team2InFirstRoom)
        {
            if (team1StartRoom.GetComponent<Room>().door1Trigger.GetComponent<DoorTrigger>().doorChosen)
            {
                Team1Rooms[0].SetActive(true);
                team2InFirstRoom = false;
            }
            else if (team1StartRoom.GetComponent<Room>().door2Trigger.GetComponent<DoorTrigger>().doorChosen)
            {
                Team1Rooms[1].SetActive(true);
                team2InFirstRoom = false;
                adderTeam2 = 1;
            }
        }
        else
        {
            //check which room the players choose
            if (Team1Rooms[Team1RoomNumber + adderTeam2].GetComponent<Room>().door1Trigger.GetComponent<DoorTrigger>().doorChosen)
            {
                Team1RoomNumber += 2;
                adderTeam2 = 0;
                Team1Rooms[Team1RoomNumber + adderTeam2].SetActive(true);
            }
            else if (Team1Rooms[Team1RoomNumber + adderTeam2].GetComponent<Room>().door2Trigger.GetComponent<DoorTrigger>().doorChosen)
            {
                Team1RoomNumber += 2;
                adderTeam2 = 1;
                Team1Rooms[Team1RoomNumber + adderTeam2].SetActive(true);
            }
        }
    }
}
