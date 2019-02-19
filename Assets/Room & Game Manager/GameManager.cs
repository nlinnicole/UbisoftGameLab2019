using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject team1;
    public GameObject Team1Space;
    public GameObject Team1Room;
    public GameObject team2;
    public GameObject Team2Room;
    public GameObject Team2Space;

    public GameObject[] roomArray;
    public Material[] roomMaterials;


    void FixedUpdate()
    {
        //check if a door has been chosen via trigger for each team
        if(Team1Room.GetComponent<Room>().door1.GetComponent<Door>().doorChosen)
        {
            GenerateRoomTeam1(Team1Room.GetComponent<Room>().door1.GetComponent<Door>().correspondingRoomNum);
        }
        else if (Team1Room.GetComponent<Room>().door2.GetComponent<Door>().doorChosen)
        {
            GenerateRoomTeam1(Team1Room.GetComponent<Room>().door2.GetComponent<Door>().correspondingRoomNum);
        }

        if (Team2Room.GetComponent<Room>().door1.GetComponent<Door>().doorChosen)
        {
            GenerateRoomTeam2(Team2Room.GetComponent<Room>().door1.GetComponent<Door>().correspondingRoomNum);
        }
        else if (Team2Room.GetComponent<Room>().door2.GetComponent<Door>().doorChosen)
        {
            GenerateRoomTeam2(Team2Room.GetComponent<Room>().door2.GetComponent<Door>().correspondingRoomNum);
        }
    }

    void Start()
    {
        GenerateRoomTeam1(0);
        GenerateRoomTeam2(0);
    }

    //probably pretty bad practice to have two methods that are virtually identical, but whatcha gonna do about it?
    public void GenerateRoomTeam1(int chosenRoom)
    {
        Object.DestroyImmediate(Team1Room);

        print(chosenRoom);

        GameObject roomToBuild = roomArray[chosenRoom];
        int xdim = roomToBuild.GetComponent<Room>().xDimension;
        int zdim = roomToBuild.GetComponent<Room>().zDimension;
        Team1Room = Instantiate(roomToBuild, new Vector3(50 - xdim/2, 0, 50 - zdim/2), Quaternion.identity) as GameObject;


        //team1 room option 1
        int roomOption1 = Random.Range(0, roomArray.Length);
        while (roomOption1 == Team1Room.GetComponent<Room>().roomNumber)
        {
            roomOption1 = Random.Range(0, roomArray.Length);
        }

        //team1 room option 2
        int roomOption2 = Random.Range(0, roomArray.Length);
        while (roomOption2 == Team1Room.GetComponent<Room>().roomNumber || roomOption2 == roomOption1)
        {
            roomOption2 = Random.Range(0, roomArray.Length);
        }

        Team1Room.GetComponent<Room>().door1.GetComponent<Renderer>().sharedMaterial = roomMaterials[roomOption1];
        Team1Room.GetComponent<Room>().door1.gameObject.GetComponent<Door>().correspondingRoomNum = roomOption1;
        Team1Room.GetComponent<Room>().door2.GetComponent<Renderer>().sharedMaterial = roomMaterials[roomOption2];
        Team1Room.GetComponent<Room>().door2.gameObject.GetComponent<Door>().correspondingRoomNum = roomOption2;

        team1.transform.position = Team1Room.GetComponent<Room>().SpawnPos.transform.position;

    }
    public void GenerateRoomTeam2(int chosenRoom)
    {

        Object.DestroyImmediate(Team2Room);
        GameObject roomToBuild = roomArray[chosenRoom];

        int xdim = roomToBuild.GetComponent<Room>().xDimension;
        int zdim = roomToBuild.GetComponent<Room>().zDimension;
        Team2Room = Instantiate(roomToBuild, new Vector3(-50 - xdim / 2, 0, 50 - zdim / 2), Quaternion.identity) as GameObject;


        //team2 room option 1
        int roomOption1 = Random.Range(0, roomArray.Length);
        while (roomOption1 == Team1Room.GetComponent<Room>().roomNumber)
        {
            roomOption1 = Random.Range(0, roomArray.Length);
        }

        //team2 room option 2
        int roomOption2 = Random.Range(0, roomArray.Length);
        while (roomOption2 == Team1Room.GetComponent<Room>().roomNumber || roomOption2 == roomOption1)
        {
            roomOption2 = Random.Range(0, roomArray.Length);
        }
        Team2Room.GetComponent<Room>().door1.GetComponent<Renderer>().sharedMaterial = roomMaterials[roomOption1];
        Team2Room.GetComponent<Room>().door1.gameObject.GetComponent<Door>().correspondingRoomNum = roomOption1;
        Team2Room.GetComponent<Room>().door2.GetComponent<Renderer>().sharedMaterial = roomMaterials[roomOption2];
        Team2Room.GetComponent<Room>().door2.gameObject.GetComponent<Door>().correspondingRoomNum = roomOption2;


        team2.transform.position = Team2Room.GetComponent<Room>().SpawnPos.transform.position;
    }
    
}
