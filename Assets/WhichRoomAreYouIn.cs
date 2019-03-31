using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WhichRoomAreYouIn : MonoBehaviour
{
    public float percentdoneteam1;
    public Slider Team1ProgressBar;

    public float initialdistance;

    public GameObject Team1Position;

    public GameObject FinalPosition;

    public float Team1Progress;

    public bool StartFindingDistance = false;

    public void Awake()
    {

    }


    void FindDistance()
    {
        if (StartFindingDistance)
        {
            initialdistance = Math.Abs(FinalPosition.transform.position.z - Team1Position.transform.position.z);
            StartFindingDistance = false;
        }

        Team1Progress = Mathf.Abs((FinalPosition.transform.position.z - Team1Position.transform.position.z) - initialdistance);
        percentdoneteam1 = Team1Progress / initialdistance;

    }

    private void Update()
    {
        FindDistance();
        Team1ProgressBar.value = percentdoneteam1;
    }

}
