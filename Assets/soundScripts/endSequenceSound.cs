using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endSequenceSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.PostEvent("startSequence", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
