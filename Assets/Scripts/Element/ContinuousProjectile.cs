using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousProjectile : MonoBehaviour
{
    [SerializeField]
    private GameObject weapon;
    [SerializeField]
    private float timer;

    private float timeCounter;

    void Start()
    {
        timeCounter = Time.time;
    }

    void Update()
    {
        if(Time.time>timeCounter + timer)
        {
            Instantiate(weapon, transform.position, Quaternion.identity);
            timeCounter = Time.time;

        }
    }
}
