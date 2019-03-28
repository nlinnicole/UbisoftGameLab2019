using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveColliderOnCollide : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Board")
        {
            gameObject.layer = 11;
        }
    }
}
