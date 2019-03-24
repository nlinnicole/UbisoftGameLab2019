using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class twodlevelenetered : MonoBehaviour
{

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Hit Player");
            anim.SetBool("StartLava", true);
            other.gameObject.GetComponent<PlayerController>().playerCamera.GetComponentInParent<CamPlayerFollow>().viewangle = 1;
            
        }

    }

}
