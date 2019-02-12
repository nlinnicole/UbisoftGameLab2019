using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockButton : MonoBehaviour
{
    [SerializeField]
    public GameObject rock;
    [SerializeField]
    public float rockTimer = 2f;

    private float timer;

    void Start()
    {
        timer = Time.time;
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(Time.time > timer + rockTimer)
        {
            GameObject rockObject = Instantiate(rock, new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z), Quaternion.identity);
            Destroy(rockObject, 3f);
            timer = Time.time;
        }
    }
}
