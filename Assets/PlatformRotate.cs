using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRotate : MonoBehaviour
{
    public float speed = 10f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
}
