using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaisingBar : MonoBehaviour
{
    [SerializeField]
    private float height = 10f;
    [SerializeField]
    private float duration = 5f;
    [SerializeField]
    private float speed = 3f;

    private float timer; 

    void Start()
    {
        timer = Time.time;
    }

    void Update()
    {
        if(timer + duration < Time.time)
        {
            Destroy(gameObject);
        }
        else
        {
            if(transform.position.y < height)
            {
                transform.Translate(Vector3.up * Time.deltaTime * speed, Space.World);
            }
        }


    }
}
