using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField]
    private float fallSpeed = 3f;

    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * fallSpeed, Space.World);
    }
}
