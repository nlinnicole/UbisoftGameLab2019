using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Script for the object been shot, used to set the speed and direction
 */

public class ProjectileWeapon : MonoBehaviour
{
    [SerializeField]
    private int shotSpeed;
    [SerializeField]
    public int direction;
    //+ve is forward -ve is other one

    void Start()
    {
        Destroy(gameObject, 10);
    }

    void Update()
    {
        if(direction == 1)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * shotSpeed);

        }
        else if(direction == -1)
        {
            transform.Translate(Vector3.back * Time.deltaTime * shotSpeed);
        }
        else
        {
            Debug.Log("Put in a proper direction");
        }
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Trigger");
        if(collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

}
