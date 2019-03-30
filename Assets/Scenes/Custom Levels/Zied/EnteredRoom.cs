using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteredRoom : MonoBehaviour
{
    int PlayerCount = 0;

    public Animator entrancedoor;
    public Animator exitdoor;

    public Rigidbody Monster;

    public void Start()
    {
        PlayerCount = 0;
    }


    public GameObject monster;

    private void OnTriggerEnter(Collider other)
    {
      if (other != null) {
        if (other.tag == "Player")
        {
            PlayerCount++;
            if(PlayerCount == 1)
            {
                //monster.GetComponent<CircleBallController>().players = other.gameObject;
                //entrancedoor.SetBool("CloseEntrance", true);

            }

        }
      }

    }
}
