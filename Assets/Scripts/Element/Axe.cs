using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField]
    private float axeLength = 5f;
    [SerializeField]
    private GameObject axeObject;
    [SerializeField]
    private float rotateSpeed = 0.5f;

    private GameObject axe;

    void Start()
    {
        axe = Instantiate(axeObject, new Vector3(transform.position.x, transform.position.y + (axeLength / 2), transform.position.z), Quaternion.identity);
    }

    void Update()
    {
        axe.transform.RotateAround(transform.position, Vector3.forward, 180*Time.deltaTime * rotateSpeed);
    }
}
