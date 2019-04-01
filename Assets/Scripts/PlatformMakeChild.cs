using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMakeChild : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.layer == 11)
        {
            Vector3 scale = collision.transform.localScale;
            collision.transform.parent = transform;
            collision.transform.localScale = scale;
        }
    }
}
