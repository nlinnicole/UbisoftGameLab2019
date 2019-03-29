using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDangle : MonoBehaviour
{

    public float startFrame;

    void Start()
    {
        GetComponent<Animator>().Play("GOGOGO", 0, startFrame);
    }
}
