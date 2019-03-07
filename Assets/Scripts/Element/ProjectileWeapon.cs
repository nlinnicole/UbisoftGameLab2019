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
    private Vector3 direction;

    void Start()
    {

    }

    void Update()
    {
        transform.Translate(direction * Time.deltaTime * shotSpeed, Space.World);
    }

    public void SetDirection(Vector3 _direction){
      direction = new Vector3(_direction.x, _direction.y, _direction.z);
    }

}
