using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Team1Space;
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
        roomArray = Resources.LoadAll<GameObject>("Rooms");


        GameObject roomToBuild = roomArray[Random.Range(0, roomArray.Length)];
        GameObject Team1Room = Instantiate(roomToBuild, transform.position, Quaternion.identity) as GameObject;

        roomToBuild = roomArray[Random.Range(0, roomArray.Length)];
        GameObject Team2Room = Instantiate(roomToBuild, transform.position, Quaternion.identity) as GameObject;

    }
}
