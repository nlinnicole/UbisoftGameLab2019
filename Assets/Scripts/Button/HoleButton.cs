using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
