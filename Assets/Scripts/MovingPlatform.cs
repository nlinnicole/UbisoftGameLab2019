using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public float speed;
    public float amount;
    public float offset;

    private void FixedUpdate()
    {
        this.transform.position += new Vector3(Mathf.Cos(Time.time * speed) + offset, 0, 0) * amount;
    }
}
