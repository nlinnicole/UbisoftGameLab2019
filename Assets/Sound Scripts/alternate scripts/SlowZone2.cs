using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone2 : MonoBehaviour
{
    [SerializeField]
    private float slowSpeed = 1;
    [SerializeField]
    private float regularSpeed = 5;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerController>().changePlayerSpeed(slowSpeed);
      
        AkSoundEngine.SetSwitch("ground", "drippyGround", other.gameObject);
    }

    public void OnTriggerExit(Collider other)
    {
        other.GetComponent<PlayerController>().changePlayerSpeed(regularSpeed);
        AkSoundEngine.SetSwitch("ground", "normalGround", other.gameObject);
    }
}
