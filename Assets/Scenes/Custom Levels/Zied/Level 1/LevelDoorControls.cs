using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoorControls : MonoBehaviour
{

    public Animator anim;

    bool OnlyOnce = true;

    public GameObject monster;

    public static bool EnteredLevel = false;

    // Start is called before the first frame update
    void Start()
    {

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
            CircleBallController.foundplayer = true;

            monster.GetComponent<CircleBallController>().players = other.gameObject;
            Debug.Log("Triggering Door");
            LevelDoorControls.EnteredLevel = true;
        }
    }
}
