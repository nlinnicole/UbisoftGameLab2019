using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoRotation : MonoBehaviour
{
    private void FixedUpdate()
    {
        this.transform.rotation = Quaternion.identity;
    }
}
