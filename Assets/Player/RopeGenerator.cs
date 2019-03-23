using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeGenerator : MonoBehaviour
{
    public GameObject ropeStart;
    public GameObject ropeEnd;
    public GameObject RopeJointPrefab;

    public float ropeRadius;
    public int ropeResolution;
    float ropeLength;

    public bool isBroken = false;
    public GameObject ropeGas;

    public GameObject[] ropeJoints;
    public Transform[] ropeJointsTrans;
    public int brokenJoint;
    public bool startedGas = false;

    public float minGasForce;
    public float maxGasForce;

    public float totalDistance = 0;
    public float distanceLimit = 10;

    void FixedUpdate()
    {

        if (isBroken && !startedGas)
        {

            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<RopeJoint>().broken)
                {
                    brokenJoint = i;
                    break;
                }
            }
            
            if(transform.GetChild(brokenJoint + 1) != null)
            {

                Destroy(transform.GetChild(brokenJoint + 1).GetChild(0).GetComponent<LineRenderer>());
                transform.GetChild(brokenJoint + 1).GetComponent<RopeJoint>().lineDeleted = true;
            }


            if (transform.GetChild(brokenJoint-1) != null && transform.GetChild(brokenJoint+1) != null )
            {
              GameObject gas = Instantiate(ropeGas, transform.GetChild(brokenJoint-1).transform);
              GameObject gas2 = Instantiate(ropeGas, transform.GetChild(brokenJoint+1).transform);
              startedGas = true;

              transform.GetChild(brokenJoint-1).gameObject.AddComponent<ConstantForce>();
              transform.GetChild(brokenJoint+1).gameObject.AddComponent<ConstantForce>();
            }

            transform.GetChild(brokenJoint).gameObject.SetActive(false);

        }

        if(startedGas)
        {
            transform.GetChild(brokenJoint-1).gameObject.GetComponent<ConstantForce>().relativeForce = new Vector3(Random.Range(minGasForce, maxGasForce), Random.Range(minGasForce, maxGasForce), Random.Range(minGasForce, maxGasForce));
            transform.GetChild(brokenJoint+1).gameObject.GetComponent<ConstantForce>().relativeForce = new Vector3(Random.Range(minGasForce, maxGasForce), Random.Range(minGasForce, maxGasForce), Random.Range(minGasForce, maxGasForce));
        }


        totalDistance = 0;
        for(int i = 0; i < ropeJoints.Length; i++)
        {
            if(i < ropeJoints.Length -1)
            {
                totalDistance += Vector3.Distance(ropeJoints[i].transform.position, ropeJoints[i + 1].transform.position);
            }
        }

    }

    void Start()
    {
        generate();
        ropeJointsTrans = transform.GetComponentsInChildren<Transform>();
    }


    public void generate()
    {

        //destroy all
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }


        ropeLength = Vector3.Distance(ropeEnd.transform.position, ropeStart.transform.position);

        ropeJoints = new GameObject[ropeResolution];

        gameObject.transform.position = ropeStart.transform.position;

        for(int i = 0; i < ropeResolution; i++)
        {
            GameObject newJoint = Instantiate(RopeJointPrefab, transform);
            newJoint.transform.localScale = new Vector3(ropeRadius, (ropeLength / ropeResolution)/2, ropeRadius);
            newJoint.transform.localPosition = new Vector3(0, -(ropeLength / ropeResolution) * i, 0);
            newJoint.GetComponent<ConfigurableJoint>().projectionMode = JointProjectionMode.PositionAndRotation;

            if (i > 0)
            {
                newJoint.GetComponent<ConfigurableJoint>().connectedBody = ropeJoints[i - 1].GetComponent<Rigidbody>();
            } else {
                newJoint.GetComponent<ConfigurableJoint>().connectedBody = ropeStart.GetComponent<Rigidbody>();
            }

            ropeJoints[i] = newJoint;
        }

        ropeEnd.GetComponent<ConfigurableJoint>().connectedBody = ropeJoints[ropeJoints.Length-1].GetComponent<Rigidbody>();
    }

}
