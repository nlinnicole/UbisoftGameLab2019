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

    GameObject[] roomArray;

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

        roomArray = Resources.LoadAll<GameObject>("Rooms");


        GameObject roomToBuild = roomArray[Random.Range(0, roomArray.Length)];
        int xdim = roomToBuild.GetComponent<Room>().xDimension;
        int zdim = roomToBuild.GetComponent<Room>().zDimension;
        Team1Room = Instantiate(roomToBuild, new Vector3(50 - xdim/2, 0, 50 - zdim/2), Quaternion.identity) as GameObject;

        roomToBuild = roomArray[Random.Range(0, roomArray.Length)];
        xdim = roomToBuild.GetComponent<Room>().xDimension;
        zdim = roomToBuild.GetComponent<Room>().zDimension;
        Team2Room = Instantiate(roomToBuild, new Vector3(-50 - xdim / 2, 0, 50 - zdim/2), Quaternion.identity) as GameObject;

        team1.transform.position = new Vector3(50, 0, 50);
        team2.transform.position = new Vector3(-50, 0, 50);
    }
}
