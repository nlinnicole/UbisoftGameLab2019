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
        axe = Instantiate(axeObject, this.transform.position, Quaternion.identity);
    }

    void Update()
    {
        float angle = 60 * Mathf.Sin(Time.time);
        axe.transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
