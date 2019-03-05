using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeHealthUI : MonoBehaviour
{
    private Quaternion originalRotation;

    void Start()
    {
        originalRotation = transform.rotation;
    }

    void Update()
    {
      transform.position = new Vector3(transform.parent.transform.position.x, transform.parent.transform.position.y+2.5f, transform.parent.transform.position.z);
      transform.rotation = originalRotation;
    }
}
