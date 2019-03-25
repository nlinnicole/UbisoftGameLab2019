using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenCamera : MonoBehaviour
{
    public Transform target;
    public int speed = 5;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        transform.RotateAround(target.position, Vector3.up, -speed * Time.deltaTime);

        //transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
}
