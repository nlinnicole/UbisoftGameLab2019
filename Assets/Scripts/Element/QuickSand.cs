using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Suppose to sink the player very slowly, need to fix 
 */

public class QuickSand : MonoBehaviour
{
    [SerializeField]
    private float sinkingSpeed = 0.2f;

    private bool sinking = false;
    private GameObject target;

    void Start()
    {
        
    }

    void Update()
    {
        if (sinking)
        {
            target.transform.Translate(Vector3.down*sinkingSpeed* Time.deltaTime);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        target = other.gameObject;
        sinking = true;
    }
}
