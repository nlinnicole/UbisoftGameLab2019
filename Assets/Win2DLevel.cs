using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win2DLevel : MonoBehaviour
{
    public Animator topcover;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "2DCube")
        {
            topcover.SetBool("OpenTop", true);
            Destroy(gameObject, 1f);
        }
    }
}
