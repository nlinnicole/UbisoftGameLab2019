using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockButton : MonoBehaviour
{
    [SerializeField]
    public GameObject rock;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {

        GameObject rockObject = Instantiate(rock, new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z) , Quaternion.identity);
        Destroy(rockObject, 3f);
    }
}
