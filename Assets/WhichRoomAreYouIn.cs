using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WhichRoomAreYouIn : MonoBehaviour
{
    public int teamnumber;

    public float percentdone;

    public Slider ProgressBar;

    public float initialdistance;

    public GameObject MyTeamPos, Team2Position;

    public GameObject FinalPosition;

    public float MyTeamProgress;
    public float Team2Progress;

    private void Start()
    {


        if(teamnumber == 1)
        {
            //Team2Position = GameObject.FindGameObjectWithTag("Team2Pos");
        }else if(teamnumber == 2)
        {
            //Team2Position = GameObject.FindGameObjectWithTag("Team1Pos");
        }

        FinalPosition = GameObject.FindGameObjectWithTag("FinalPoint");

        initialdistance = Math.Abs(FinalPosition.transform.position.z - MyTeamPos.transform.position.z);

        ProgressBar.value = percentdone;

    }


    void FindDistance()
    {
        MyTeamProgress = Math.Abs(FinalPosition.transform.position.z - MyTeamPos.transform.position.z);

        percentdone = Math.Abs(MyTeamProgress - initialdistance) / initialdistance;
        //Team2Progress = Math.Abs(FinalPosition.transform.position.z - Team2Position.transform.position.z);
    }

    private void Update()
    {
        FindDistance();
        ProgressBar.value = percentdone;
    }

}
