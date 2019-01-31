using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CamPlayerFollow : MonoBehaviour
{
    public GameObject player;
    public float lerpAmount;
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        target = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.position = Vector3.Lerp(transform.position, target, lerpAmount);
    }
}
