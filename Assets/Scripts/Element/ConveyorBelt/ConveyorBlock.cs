using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBlock : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
    }

}
