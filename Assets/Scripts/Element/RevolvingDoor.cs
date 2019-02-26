using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolvingDoor : MonoBehaviour
{
    [SerializeField]
    private float doorLength = 5f;
    [SerializeField]
    private float doorArmsAmount = 3f;
    [SerializeField]
    private GameObject doorArm;
    [SerializeField]
    private float rotateSpeed = 0.1f;

    private List<GameObject> armList = new List<GameObject>();

    void Start()
    {
        //instatitate the door arms
        for(int x=0; x<doorArmsAmount; x++)
        {
            float angle = (360 / doorArmsAmount) * x;
            GameObject arm = Instantiate(doorArm, new Vector3(transform.position.x+(doorLength/2), transform.position.y, transform.position.z), Quaternion.identity);
            arm.transform.RotateAround(transform.position, Vector3.up, angle);
            armList.Add(arm);
        }
    }

    void Update()
    {
        //make the arms rotate
        foreach(GameObject obj in armList)
        {
            obj.transform.RotateAround(transform.position, Vector3.up, 360 * Time.deltaTime* rotateSpeed);

        }
    }
}
