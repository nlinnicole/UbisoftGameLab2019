using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[ExecuteInEditMode]
public class CamPlayerFollow : MonoBehaviour
{
    
    public GameObject player1;
    public GameObject player2;
    public float lerpAmount;
    Vector3 target;
    public float heightOffset;


    Transform camobject;

    //Camera Movement Per Level
    public float speed = .2f;

    private Transform start;
    public Transform des;

    public float cammovespeed;

    // 0 = default, 1 = 2D, 2 = Top down, 3 =behind view

    public int viewangle = 0;


    // Start is called before the first frame update
    void Start()
    {
        camobject = this.gameObject.transform.GetChild(0);
        changeCameraTo2D();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if (photonView.IsMine)

        {
            target = (new Vector3(player1.transform.position.x, player1.transform.position.y, player1.transform.position.z) +
                new Vector3(player2.transform.position.x, player2.transform.position.y, player2.transform.position.z)) / 2
                + Vector3.up * heightOffset;
            transform.position = Vector3.Lerp(transform.position, target, lerpAmount);
        }

        if(viewangle == 0)
        {
            GetComponentInChildren<MoveCamera>().changetoDefault();
        }

        if(viewangle == 1)
        {
            GetComponentInChildren<MoveCamera>().changeTo2D();
        }
        if(viewangle == 2)
        {
            GetComponentInChildren<MoveCamera>().changeToTopDown();
        }
        if(viewangle == 3)
        {
            GetComponentInChildren<MoveCamera>().changeToBehind();
        }

    }

    public void changeCameraTo2D()
    {
        start = gameObject.transform;
        
        
    }

    private void Update()
    {
       
    }

}