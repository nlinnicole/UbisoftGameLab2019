using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RoomGen : MonoBehaviour
{
    public GameObject bottomLeft;
    public GameObject topRight;

    void Start()
    {
        
    }

    void Update()
    {
        Debug.DrawLine(bottomLeft.transform.position, new Vector3(topRight.transform.position.x, 0, 0), Color.green, 1f);
        Debug.DrawLine(new Vector3(topRight.transform.position.x, 0, 0), topRight.transform.position, Color.green, 1f);
        Debug.DrawLine(new Vector3(bottomLeft.transform.position.x, 0, topRight.transform.position.z), topRight.transform.position, Color.green, 1f);
        Debug.DrawLine(new Vector3(bottomLeft.transform.position.x, 0, topRight.transform.position.z), bottomLeft.transform.position, Color.green, 1f);
    }
}
