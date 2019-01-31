using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float moveDrag = 0.1f;
    public float accelerationFactor = 1;
    public float jumpForce = 5;
    public float jumpMovementReduction = 1;
    public bool isGrounded = false;
    public float groundDetectDistance = 0.5f;
    Vector3 velocity;
    Vector3 acceleration;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (isGrounded)
        {
            //movement
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                velocity.x += 1 * moveSpeed;
            }
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                velocity.x -= 1 * moveSpeed;
            }
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                velocity.z += 1 * moveSpeed;
            }
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                velocity.z -= 1 * moveSpeed;
            }

            velocity /= moveDrag; //reduce velocity vector to look like drag
        }
        else {
            //reduced movement
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                velocity.x += (1 * moveSpeed) / jumpMovementReduction;
            }
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                velocity.x -= (1 * moveSpeed) / jumpMovementReduction;
            }
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                velocity.z += (1 * moveSpeed) / jumpMovementReduction;
            }
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                velocity.z -= (1 * moveSpeed) / jumpMovementReduction;
            }
        }


        velocity = Vector3.ClampMagnitude(velocity, 1 * moveSpeed); //clamping instead of normalizing
        transform.position += velocity; //apply velocity to transform


        if(Physics.Raycast(transform.position, Vector3.down, groundDetectDistance))
        {
            isGrounded = true;
        }
        if(Input.GetAxisRaw("Jump") > 0 && isGrounded)
        {
            isGrounded = false;
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);
        }

    }
}
