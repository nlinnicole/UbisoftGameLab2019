using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalButton : MonoBehaviour
{
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private GameObject dice_trigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DiceTrigger")
        {
            Debug.Log("hello");
        }
    }
}
