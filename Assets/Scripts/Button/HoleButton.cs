using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Create a hole (delete a plane) when pressed
 */
public class HoleButton : MonoBehaviour
{
    [SerializeField]
    private GameObject hole;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        Destroy(hole);
    }
}
