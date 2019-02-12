using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [HideInInspector]
    public float swapCountDown = 0;

    void Start()
    {
        //Physics.IgnoreLayerCollision(11,11);
    }

    void FixedUpdate()
    {
        if(swapCountDown > 0) swapCountDown -= Time.deltaTime;
    }
}
