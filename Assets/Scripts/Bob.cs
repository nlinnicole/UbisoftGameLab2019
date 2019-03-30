using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour
{
    public float speed = 0.2f;
    public float heightOffset = 3.0f;
    public float height = 0.5f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        float newYpos = height * Mathf.Sin(Time.time * speed) + heightOffset;
        transform.position = new Vector3(pos.x, newYpos, pos.z);
    }
}
