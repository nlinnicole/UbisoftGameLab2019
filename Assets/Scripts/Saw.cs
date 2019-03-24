using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public float speed;
    public float amount;
    public float offset;

    private void FixedUpdate()
    {
        this.transform.position += new Vector3(0, 0, Mathf.Cos(Time.time * speed) + offset) * amount;
    }
}
