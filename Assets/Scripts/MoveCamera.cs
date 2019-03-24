using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    private Transform start;
    public Transform Cam2D;
    public Transform CamDefault;
    public Transform CamTopDown;
    public Transform CamBehind;


    float cammovespeed = 1f;
    public void changeTo2D()
    {
        start = gameObject.transform;
        transform.position = Vector3.Slerp(start.position, Cam2D.position, Time.deltaTime * cammovespeed);
        transform.rotation = Quaternion.Slerp(start.rotation, Cam2D.rotation, Time.deltaTime * cammovespeed);
        gameObject.GetComponent<Camera>().orthographic = true;
        Debug.Log("Slerping");

    }

    public void changetoDefault()
    {
        start = gameObject.transform;
        transform.position = Vector3.Slerp(start.position, CamDefault.position, Time.deltaTime * cammovespeed);
        transform.rotation = Quaternion.Slerp(start.rotation, CamDefault.rotation, Time.deltaTime * cammovespeed);
        gameObject.GetComponent<Camera>().orthographic = true;
        Debug.Log("Slerping");

    }

    public void changeToTopDown()
    {
        start = gameObject.transform;
        transform.position = Vector3.Slerp(start.position, CamTopDown.position, Time.deltaTime * cammovespeed);
        transform.rotation = Quaternion.Slerp(start.rotation, CamTopDown.rotation, Time.deltaTime * cammovespeed);
        gameObject.GetComponent<Camera>().orthographic = false;
        Debug.Log("Slerping");

    }


    // Start is called before the first frame update
    void Start()
    {
        start = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
