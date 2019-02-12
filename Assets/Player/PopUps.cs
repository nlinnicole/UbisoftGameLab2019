using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUps : MonoBehaviour
{
    public GameObject player;
    public float displayHeight = 2;

    void FixedUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, displayHeight, player.transform.position.z);
    }
}
