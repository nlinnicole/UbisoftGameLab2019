using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject team1;
    public GameObject Team1Space;
    public GameObject team2;
    public GameObject Team2Space;

    GameObject Team1Room;
    GameObject Team2Room;

    GameObject[] roomArray;

    void Start()
    {
        
    }

    void Update()
    {
        
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
