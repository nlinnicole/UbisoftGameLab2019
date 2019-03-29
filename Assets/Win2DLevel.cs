using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win2DLevel : MonoBehaviour
{
    public Animator topcover1;
    public Animator topcover2;
    public Animator topcover3;
    public Animator topcover4;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "2DCube")
        {
            topcover1.SetBool("Win2DLevel", true);
            topcover2.SetBool("Win2DLevel", true);

            topcover3.SetBool("Win2DLevel", true);

            Destroy(gameObject, 1f);
        }
    }
}
