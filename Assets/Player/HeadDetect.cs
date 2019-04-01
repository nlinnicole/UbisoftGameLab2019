using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadDetect : MonoBehaviour
{
    public PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 15)
        {
            player.inDeathZone = true;
            AkSoundEngine.PostEvent("splash", GameObject.FindGameObjectWithTag("Player"));
        }
    }
}
