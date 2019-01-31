using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float rotationSpeed = 1f;
    public float moveDrag = 0.1f;
    public float accelerationFactor = 1;
    public float jumpForce = 5;
    public float jumpMovementReduction = 1;
    public bool isGrounded = false;
    public float groundDetectDistance = 0.5f;
    public float rotationLerpAmount;
    Vector3 velocity;
    Vector3 faceVelocity;
    Vector3 acceleration;
    Vector3 faceDirection;

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
                faceVelocity.x += 1 * rotationSpeed;
            }
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                velocity.x -= 1 * moveSpeed;
                faceVelocity.x -= 1 * rotationSpeed;
            }
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                velocity.z += 1 * moveSpeed;
                faceVelocity.z += 1 * rotationSpeed;
            }
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                velocity.z -= 1 * moveSpeed;
                faceVelocity.z -= 1 * rotationSpeed;
            }

            velocity /= moveDrag; //reduce velocity vector to look like drag
            faceVelocity /= moveDrag; //reduce velocity vector to look like drag
        }
        else {
            //reduced movement when jumping
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                velocity.x += (1 * moveSpeed) / jumpMovementReduction;
                faceVelocity.x += (1 * rotationSpeed);
            }
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                velocity.x -= (1 * moveSpeed) / jumpMovementReduction;
                faceVelocity.x -= (1 * rotationSpeed);
            }
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                velocity.z += (1 * moveSpeed) / jumpMovementReduction;
                faceVelocity.z += (1 * rotationSpeed);
            }
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                velocity.z -= (1 * moveSpeed) / jumpMovementReduction;
                faceVelocity.z -= (1 * moveSpeed);
            }
        }


        velocity = Vector3.ClampMagnitude(velocity, 1 * moveSpeed); //clamping instead of normalizing
        faceVelocity = Vector3.ClampMagnitude(faceVelocity, 1 * moveSpeed); //clamping instead of normalizing
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


        faceDirection = Vector3.Lerp(transform.position + transform.forward, transform.position + faceVelocity, rotationLerpAmount);
        transform.LookAt(faceDirection);

    }
}
