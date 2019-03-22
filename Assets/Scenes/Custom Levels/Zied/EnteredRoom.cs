using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteredRoom : MonoBehaviour
{

    public GameObject monster;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            monster.GetComponent<CircleBallController>().players = other.gameObject;
            LevelDoorControls.EnteredLevel = true;
            Destroy(gameObject);
        }

    }
}
