using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float speed;
    public float amount;
    public float offset;

    private void FixedUpdate()
    {
        this.transform.position += new Vector3(0, Mathf.Sin(Time.time * speed) + offset, 0) * amount;
    }
}
