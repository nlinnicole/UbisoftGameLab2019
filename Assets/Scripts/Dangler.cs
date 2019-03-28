using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dangler : MonoBehaviour
{

    public float startFrame;

    void Start()
    {
        GetComponent<Animator>().Play("Dangler", 0, startFrame);
    }


}
