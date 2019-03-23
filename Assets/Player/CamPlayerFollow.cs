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

    //Camera Movement Per Level
    public float speed = .2f;

    private Transform start;
    public Transform des;




    // Start is called before the first frame update
    void Start()
    {
        //changeCameraTo2D();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if (photonView.IsMine)
        
        {
            target = (new Vector3(player1.transform.position.x, player1.transform.position.y, player1.transform.position.z) + 
                new Vector3(player2.transform.position.x, player2.transform.position.y, player2.transform.position.z))/2 
                + Vector3.up * heightOffset;
            transform.position = Vector3.Lerp(transform.position, target, lerpAmount);
        }
        
    }

    void changeCameraTo2D()
    {
        start = gameObject.transform;
        des = des.transform;
    }

    private void Update()
    {
        //transform.rotation = Quaternion.Slerp(start.rotation, des.rotation, Time.time * speed);
    }

}
