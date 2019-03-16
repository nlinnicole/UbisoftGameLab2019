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

    void FixedUpdate()
    {
        if(isBroken && !startedGas)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<RopeJoint>().broken)
                {
                    brokenJoint = i;
                    break;
                }
            }

            GameObject gas = Instantiate(ropeGas, ropeJointsTrans[brokenJoint].transform);
            GameObject gas2 = Instantiate(ropeGas, ropeJointsTrans[brokenJoint+2].transform);
            startedGas = true;

            transform.GetChild(brokenJoint - 1).gameObject.AddComponent<ConstantForce>();
            transform.GetChild(brokenJoint+1).gameObject.AddComponent<ConstantForce>();
        }

        if(startedGas)
        {
            transform.GetChild(brokenJoint - 1).gameObject.GetComponent<ConstantForce>().relativeForce = new Vector3(Random.Range(minGasForce, maxGasForce), Random.Range(minGasForce, maxGasForce), Random.Range(minGasForce, maxGasForce));
            transform.GetChild(brokenJoint + 1).gameObject.GetComponent<ConstantForce>().relativeForce = new Vector3(Random.Range(minGasForce, maxGasForce), Random.Range(minGasForce, maxGasForce), Random.Range(minGasForce, maxGasForce));
        }
    }

    void Start()
    {
        ropeJointsTrans = transform.GetComponentsInChildren<Transform>();
    }


    public void generate()
    {
        ropeLength = Vector3.Distance(ropeEnd.transform.position, ropeStart.transform.position);

        ropeJoints = new GameObject[ropeResolution];

        gameObject.transform.position = ropeStart.transform.position;

        for(int i = 0; i < ropeResolution; i++)
        {
            GameObject newJoint = Instantiate(RopeJointPrefab, transform);
            newJoint.transform.localScale = new Vector3(ropeRadius, (ropeLength / ropeResolution)/2, ropeRadius);
            newJoint.transform.localPosition = new Vector3(0, -(ropeLength / ropeResolution) * i, 0);
            newJoint.GetComponent<ConfigurableJoint>().projectionMode = JointProjectionMode.PositionAndRotation;

            SoftJointLimit softJointLimit = new SoftJointLimit();
            softJointLimit.limit = ropeLength / ropeResolution;
            newJoint.GetComponent<ConfigurableJoint>().linearLimit = softJointLimit;

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
