using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoorControls : MonoBehaviour
{

    private Animator anim;

    bool OnlyOnce = true;


    public static bool EnteredLevel = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (OnlyOnce)
        {
            if (EnteredLevel)
            {
                Debug.Log("Lowering Door");
                anim.SetBool("LowerDoor", true);
                EnteredLevel = false;
                OnlyOnce = false;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Triggering Door");
            LevelDoorControls.EnteredLevel = true;
        }
    }
}
