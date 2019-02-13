using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerController>().changePlayerSpeed(1f);
    }

    public void OnTriggerExit(Collider other)
    {
        other.GetComponent<PlayerController>().changePlayerSpeed(5f);
    }
}
