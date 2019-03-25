using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorLoop : MonoBehaviour
{
    [SerializeField]
    private GameObject startEdge;
    [SerializeField]
    private GameObject endEdge;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == endEdge)
        {
            this.transform.position = new Vector3(
                startEdge.transform.position.x,
                this.transform.position.y,
                this.transform.position.z
           );
        }
    }
}
