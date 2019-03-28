using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WhichRoomAreYouIn : MonoBehaviour
{
    public GameObject Team1Position, Team2Position;

    public Transform FinalPosition;

    float Team1Progress;
    float Team2Progress;


    void FindDistance()
    {
        Team1Progress = Math.Abs(FinalPosition.transform.position.z - Team1Position.transform.position.z);

        Team2Progress = Math.Abs(FinalPosition.transform.position.z - Team2Position.transform.position.z);
    }

}
