using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpDown : MonoBehaviour
{

    public float speed;
    public float offset;
    public float length;
    private void FixedUpdate()
    {
        this.transform.position = new Vector3(this.transform.position.x, (Mathf.Sin(Time.time * speed) + offset) * length, this.transform.position.z);
    }
}
