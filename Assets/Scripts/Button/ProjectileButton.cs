using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileButton : MonoBehaviour
{

    [SerializeField]
    private int amountShot = 3;
    [SerializeField]
    private int durationBetweenShot =2;
    [SerializeField]
    private GameObject weapon;
    [SerializeField]
    private GameObject location;

    private float timer;
    private bool shooting = false;
    private int counter; 

    void Start()
    {
        timer = Time.time;
    }

    void Update()
    {
        //Will shoot when button is pressed
        if (shooting && Time.time > timer + durationBetweenShot)
        {
            if(counter != amountShot)
            {
                GameObject weaponObject = Instantiate(weapon, location.transform.position, Quaternion.identity);
                counter++;
                timer = Time.time;
            }
            else
            {
                //no more shots require
                shooting = false;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        timer = Time.time;
        counter = 0;
        shooting = true;
    }
}
