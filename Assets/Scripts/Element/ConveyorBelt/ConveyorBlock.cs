using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConveyorBlock : MonoBehaviour
{
    [SerializeField]
    private static float speed = 1;

    [SerializeField]
    private bool left = true;

    void Start()
    {
        
    }

    void Update()
    {
        if (left)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
        }
        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
        }

    }

}
