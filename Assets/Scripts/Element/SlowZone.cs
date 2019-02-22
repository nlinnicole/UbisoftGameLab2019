using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : MonoBehaviour
{
    [SerializeField]
    private float slowSpeed;
    [SerializeField]
    private float regularSpeed;

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
