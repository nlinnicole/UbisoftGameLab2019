using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * regular projectile attack (continuous)
 */

public class ContinuousProjectile : MonoBehaviour
{
    //whatever it's shooting
    [SerializeField]
    private GameObject weapon;
    //duration between each shot
    [SerializeField]
    private float timer;

    private float timeCounter;

    void Start()
    {
        timeCounter = Time.time;
    }

    void Update()
    {
        //keep shooting continuously between interval
        if(Time.time>timeCounter + timer)
        {
            GameObject weaponObj = Instantiate(weapon, transform.position, Quaternion.identity);
            timeCounter = Time.time;
            //object will destroy in 10 seconds
            Destroy(weaponObj, 10);

        }
    }
}
