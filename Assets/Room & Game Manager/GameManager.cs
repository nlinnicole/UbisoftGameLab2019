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
            GenerateRooms();
        }
        else if (Team1Room.GetComponent<Room>().door2.GetComponent<Door>().doorChosen)
        {
            GenerateRooms();
        }

        if (Team2Room.GetComponent<Room>().door1.GetComponent<Door>().doorChosen)
        {
            GenerateRooms();
        }
        else if (Team2Room.GetComponent<Room>().door2.GetComponent<Door>().doorChosen)
        {
            GenerateRooms();
        }


    }

    public void GenerateRooms()
    {
        Object.DestroyImmediate(Team1Room);
        Object.DestroyImmediate(Team2Room);



        GameObject roomToBuild = roomArray[Random.Range(0, roomArray.Length)];
        int xdim = roomToBuild.GetComponent<Room>().xDimension;
        int zdim = roomToBuild.GetComponent<Room>().zDimension;
        Team1Room = Instantiate(roomToBuild, new Vector3(50 - xdim/2, 0, 50 - zdim/2), Quaternion.identity) as GameObject;


        //team1 room option 1
        int roomOption1 = Random.Range(0, roomArray.Length-1);
        while (roomOption1 == Team1Room.GetComponent<Room>().roomNumber)
        {
            roomOption1 = Random.Range(0, roomArray.Length-1);
        }

        //team1 room option 2
        int roomOption2 = Random.Range(0, roomArray.Length-1);
        while (roomOption2 == Team1Room.GetComponent<Room>().roomNumber || roomOption2 == roomOption1)
        {
            roomOption2 = Random.Range(0, roomArray.Length-1);
        }
        Team1Room.GetComponent<Room>().door1.GetComponent<Renderer>().sharedMaterial = roomMaterials[roomOption1-1];
        Team1Room.GetComponent<Room>().door2.GetComponent<Renderer>().sharedMaterial = roomMaterials[roomOption2-1];




        roomToBuild = roomArray[Random.Range(0, roomArray.Length)];

        xdim = roomToBuild.GetComponent<Room>().xDimension;
        zdim = roomToBuild.GetComponent<Room>().zDimension;
        Team2Room = Instantiate(roomToBuild, new Vector3(-50 - xdim / 2, 0, 50 - zdim/2), Quaternion.identity) as GameObject;


        //team2 room option 1
        roomOption1 = Random.Range(0, roomArray.Length);
        while (roomOption1 == Team1Room.GetComponent<Room>().roomNumber)
        {
            roomOption1 = Random.Range(0, roomArray.Length);
        }

        //team2 room option 2
        roomOption2 = Random.Range(0, roomArray.Length);
        while (roomOption2 == Team1Room.GetComponent<Room>().roomNumber || roomOption2 == roomOption1)
        {
            roomOption2 = Random.Range(0, roomArray.Length);
        }
        Team1Room.GetComponent<Room>().door1.GetComponent<Renderer>().sharedMaterial = roomMaterials[roomOption1];
        Team1Room.GetComponent<Room>().door2.GetComponent<Renderer>().sharedMaterial = roomMaterials[roomOption2];

        team1.transform.position = new Vector3(50, 0, 50);
        team2.transform.position = new Vector3(-50, 0, 50);
    }
}
