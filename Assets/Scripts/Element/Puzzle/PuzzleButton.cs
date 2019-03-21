using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    private bool pressed = false;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public bool getPress()
    {
        return pressed;
    }

    public void OnTriggerStay(Collider other)
    {
        pressed = true;
    }

    public void OnTriggerExit(Collider other)
    {
        pressed = false;
    }
}
