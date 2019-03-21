using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullObjZone : MonoBehaviour
{
    public GameObject door;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "LevelTrigger")
        {
            door.transform.position += new Vector3(0, 6, 0);
            Destroy(other.GetComponent<BoxCollider>());
        }
    }
}
