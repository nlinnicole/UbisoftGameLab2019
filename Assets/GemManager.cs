using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemManager : MonoBehaviour
{
    public int Team1Gems;
    public int Team2Gems;
    GameObject Team1;
    GameObject Team2;
    


    // Start is called before the first frame update
    void Start()
    {

        Team1Gems = 0;
        Team2Gems = 0;

        Team1 = GameObject.FindGameObjectWithTag("Team1");
        Team2 = GameObject.FindGameObjectWithTag("Team2");

    }

    // Update is called once per frame
    void Update()
    {
        Team1Gems = Team1.GetComponent<NetworkPlayer>().Gemzzzzzz;

        Team2Gems = Team2.GetComponent<NetworkPlayer>().Gemzzzzzz;

    }
}
