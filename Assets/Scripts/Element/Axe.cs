using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField]
    private GameObject axeObject;
    [SerializeField]
    private float rotateSpeed = 0.5f;
    [SerializeField]
    private int yAngle = 90;
    [SerializeField]
    private int zAngle = 90;
    [SerializeField]
    private int offset = -90;

    private GameObject axe;

    void Start()
    {
        axe = Instantiate(axeObject, this.transform.position, Quaternion.identity);
    }

    void Update()
    {
        float angle = 90 * Mathf.Sin(Time.time) + offset; ;
        axe.transform.eulerAngles = new Vector3(angle, yAngle, zAngle);
    }
}
