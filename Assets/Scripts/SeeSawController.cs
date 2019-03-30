using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeSawController : MonoBehaviour
{
    Rigidbody rb;
    bool inDefaultAngle = false;
    bool noCollisions = true;

    public float interpolation = 0f;
    public float speed = 0.05f;

    private int playerCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
      if (noCollisions){
        if (rb.velocity != Vector3.zero){
          Brake();
        } else {
          Debug.Log("Finished breaking");
          if (interpolation < 1f){
            if (transform.rotation.z > 0f){
              transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, - (interpolation * speed));
            } else if (transform.rotation.z < 0f){
              transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, interpolation * speed);
            }

            interpolation += Time.deltaTime;
          } else {
            inDefaultAngle = true;
            interpolation = 0f;
          }
        }
      }
    }

    void Brake(){
      rb.angularVelocity = new Vector3(rb.angularVelocity.x, rb.angularVelocity.y, rb.angularVelocity.z - Time.deltaTime);
      if (rb.angularVelocity.z < 0f)
        rb.angularVelocity = new Vector3(rb.angularVelocity.x, rb.angularVelocity.y, 0f);
    }

    void OnCollisionEnter(Collision other){
      if (other.transform.tag == "Player"){
        playerCount++;

        noCollisions = false;
      }
    }

    void OnCollisionExit(Collision other){
      if (other.transform.tag == "Player"){
        playerCount--;

        if (playerCount < 1)
          noCollisions = true;
      }
    }
}
