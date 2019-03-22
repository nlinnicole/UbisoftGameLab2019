using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooterTrapSFX : MonoBehaviour
{

    int counter = 0;
    int shotsFired = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counter = gameObject.GetComponent<ProjectileButton>().counter;
        if (shotsFired != counter)
        {
            shotsFired = counter;
            AkSoundEngine.PostEvent("fireShot", gameObject);
        }
    }
}
