using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WhichRoomAreYouIn : MonoBehaviour
{
    public int teamnumber;

    public float percentdoneteam1, percentdoneteam2;

    public Slider Team1ProgressBar;
    public Slider Team2ProgressBar;

    public float initialdistance;

    public GameObject Team1Position, Team2Position;

    public GameObject FinalPosition;

    public float Team1Progress;
    public float Team2Progress;

    public bool StartFindingDistance;

    public void StartDistances()
    {
        if (StartFindingDistance)
        {
            Team1Position = GameObject.FindGameObjectWithTag("Team1Pos");

            Team2Position = GameObject.FindGameObjectWithTag("Team2Pos");

            initialdistance = Math.Abs(FinalPosition.transform.position.z - Team1Position.transform.position.z);
        }
    }


    void FindDistance()
    {
        Team1Progress = Math.Abs(FinalPosition.transform.position.z - Team1Position.transform.position.z);
        Team2Progress = Math.Abs(FinalPosition.transform.position.z - Team2Position.transform.position.z);
        percentdoneteam1 = Math.Abs(Team1Progress - initialdistance) / initialdistance;
        percentdoneteam2 = Math.Abs(Team2Progress - initialdistance) / initialdistance;
    }

    private void LateUpdate()
    {
        if (StartFindingDistance)
        {
            FindDistance();
            Team1ProgressBar.value = percentdoneteam1;
            Team2ProgressBar.value = percentdoneteam2;
        }
        
    }

}
