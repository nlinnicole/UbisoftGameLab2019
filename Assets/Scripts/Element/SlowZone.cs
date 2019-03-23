using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : MonoBehaviour
{
    [SerializeField]
    private float slowSpeed = 1;
    [SerializeField]
    private float regularSpeed = 5;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerController>().changePlayerSpeed(slowSpeed);
    }

    public void OnTriggerExit(Collider other)
    {
        other.GetComponent<PlayerController>().changePlayerSpeed(regularSpeed);
    }
}
