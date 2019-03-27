using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField]
    private GameObject pivot;
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
        float angle = 90 *  Mathf.Sin(Time.time) - 90;
        axe.transform.eulerAngles = new Vector3(angle, 90, 90);
        //axe.transform.RotateAround(pivot.transform.position, Vector3.up, 20 * Time.deltaTime);
    }
}
