using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    public GameObject DefaultCamT1, DefaultCamT2;

    private Transform start;
    public Transform Cam2D;
    public Transform CamDefault;
    public Transform CamTopDown;
    public Transform CamBehind;

    public int teamnumber;

    public float cammovespeed = 1f;

    public void Start()
    {
        start = gameObject.transform;
        if (teamnumber == 1)
        {
            CamDefault = DefaultCamT2.transform;
        }else if(teamnumber == -1)
        {
            CamDefault = DefaultCamT1.transform;
        }
    }

    public void changeTo2D()
    {
        start = gameObject.transform;
        transform.position = Vector3.Lerp(start.position, Cam2D.position, Time.deltaTime * cammovespeed);
        transform.rotation = Quaternion.Slerp(start.rotation, Cam2D.rotation, Time.deltaTime * cammovespeed);
        gameObject.GetComponent<Camera>().orthographic = true;

    }

    public void changetoDefault()
    {
        start = gameObject.transform;
        transform.position = Vector3.Slerp(start.position, new Vector3(CamDefault.position.x, CamDefault.position.y, CamDefault.position.z), Time.deltaTime * cammovespeed);
        transform.rotation = Quaternion.Slerp(start.rotation, new Quaternion(CamDefault.rotation.x, CamDefault.rotation.y * teamnumber, CamDefault.rotation.z * teamnumber, CamDefault.rotation.w),Time.deltaTime * cammovespeed);
        gameObject.GetComponent<Camera>().orthographic = false;

    }

    public void changeToTopDown()
    {
        start = gameObject.transform;
        transform.position = Vector3.Slerp(start.position, CamTopDown.position, Time.deltaTime * cammovespeed);
        transform.rotation = Quaternion.Slerp(start.rotation, CamTopDown.rotation, Time.deltaTime * cammovespeed);
        gameObject.GetComponent<Camera>().orthographic = false;

    }

    public void changeToBehind()
    {
        start = gameObject.transform;
        transform.position = Vector3.Slerp(start.position, CamBehind.position, Time.deltaTime * cammovespeed);
        transform.rotation = Quaternion.Slerp(start.rotation, CamBehind.rotation, Time.deltaTime * cammovespeed);
        gameObject.GetComponent<Camera>().orthographic = false;
    }


    

    // Update is called once per frame
    void Update()
    {
        
    }
}
