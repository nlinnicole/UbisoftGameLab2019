using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmeDatRagdoll : MonoBehaviour
{
    public Transform[] bones;
    public Vector3[] originalPositions;
    public Quaternion[] originalRotations;

    private void Start()
    {

        bones = new Transform[50];

        bones = gameObject.GetComponentsInChildren<Transform>();

        originalPositions = new Vector3[bones.Length];
        originalRotations = new Quaternion[bones.Length];

        for (int i = 0; i < bones.Length; i++)
        {
            originalPositions[i] = bones[i].localPosition;
            originalRotations[i] = bones[i].localRotation;
        }



    }

    public void resetRagdoll()
    {
        for (int i = 0; i < originalPositions.Length; i++)
        {
            bones[i].localPosition = originalPositions[i];
            bones[i].localRotation = originalRotations[i];
        }
    }
}
