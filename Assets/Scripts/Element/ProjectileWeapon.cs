using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
