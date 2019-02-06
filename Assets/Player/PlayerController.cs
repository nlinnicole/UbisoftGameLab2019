using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float sprintMultiplier;
    public float rotationSpeed = 1f;
    public float moveDrag = 0.1f;
    public float accelerationFactor = 1;
    public float jumpForce = 5;
    public float jumpMovementReduction = 1;
    public bool isGrounded = false;
    public bool isRolling = false;
    public float rollMultiplier = 1;
    public float rollDuration = 1;
    public float rollCooldown = 0.5f;
    float rollTime = 0;
    float rollMod = 1;
    public float groundDetectDistance = 0.5f;
    public float rotationLerpAmount;
    public GameObject heldItem;
    public float itemPickupDistance;
    public GameObject inventory;
    public GameObject swapText;
    public float swapCooldown = 1;
    public float playerSwapDelay = 1;
    float playerSwapCountdown;

    Vector3 velocity;
    Vector3 faceDirection;
    float sprintMod = 1;
    LayerMask itemLayerMask;
    Collider[] nearbyItems;

    void Start()
    {
        itemLayerMask = LayerMask.GetMask("Items");
    }


    //wallsticking on jump may occur if the wall doesnt have a friction-less physics material
    void FixedUpdate()
    {

        //item pickup
        nearbyItems = new Collider[Physics.OverlapSphere(transform.position, itemPickupDistance / 2, itemLayerMask).Length];
        nearbyItems = Physics.OverlapSphere(transform.position, itemPickupDistance / 2, itemLayerMask);
        if (nearbyItems.Length > 0)
        {

            if (nearbyItems.Length > 0 && heldItem != null)
            {
                if (nearbyItems[0].GetComponent<Item>().swapCountDown <= 0 && playerSwapCountdown <= 0)
                {
                    swapText.gameObject.SetActive(true);
                    if (Input.GetButtonDown("Fire1"))
                    {
                        playerSwapCountdown = playerSwapDelay;
                       
                        //old item
                        heldItem.layer = 10;
                        heldItem.transform.parent = null;
                        heldItem.GetComponent<Item>().swapCountDown = swapCooldown;
                        heldItem.GetComponent<Rigidbody>().isKinematic = false;

                        //new item
                        heldItem = nearbyItems[0].gameObject;
                        heldItem.transform.position = inventory.transform.position;
                        heldItem.transform.parent = inventory.transform;
                        heldItem.layer = 0;
                        heldItem.GetComponent<Rigidbody>().isKinematic = true;
                    }
                } else {
                    swapText.gameObject.SetActive(false);
                }
            }
            else
            {
                swapText.gameObject.SetActive(false);
            }



            if (heldItem == null)
            {
                heldItem = nearbyItems[0].gameObject;
                heldItem.transform.position = inventory.transform.position;
                heldItem.transform.parent = inventory.transform;
                heldItem.layer = 0;
                heldItem.GetComponent<Rigidbody>().isKinematic = true;
            }



        } else {
            swapText.gameObject.SetActive(false);
        }

        playerSwapCountdown -= Time.deltaTime;


        //sprint
        //if(Input.GetAxisRaw("Run") > 0 && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && isGrounded)
        //{
        //    sprintMod = sprintMultiplier;
        //} else {
        //    sprintMod = 1;
        //}

        //roll
        if (Input.GetButtonDown("Run") && isGrounded && !isRolling)
        {
            isGrounded = false;
            isRolling = true;

            rollTime = 0;
        } 

        if(isRolling && rollTime < rollDuration)
        {
            rollMod = rollMultiplier;
            rollTime += Time.deltaTime;
            isGrounded = false;
        } else if(isRolling && rollTime < rollDuration + rollCooldown) {
            rollTime += Time.deltaTime;
            isGrounded = true;
            rollMod = 1;
        } else {
            isRolling = false;
            rollMod = 1;
        }


        faceDirection = Vector3.zero;
        if (isGrounded)
        {
            //movement
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                velocity.x += 1 * moveSpeed;
                faceDirection.x = 1;
            }
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                velocity.x -= 1 * moveSpeed;
                faceDirection.x = -1;
            }
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                velocity.z += 1 * moveSpeed;
                faceDirection.z = 1;
            }
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                velocity.z -= 1 * moveSpeed;
                faceDirection.z = -1;
            }

            velocity /= moveDrag; //reduce velocity vector to look like drag
        }
        else {
            //reduced movement when jumping
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                velocity.x += (1 * moveSpeed) / jumpMovementReduction;
                faceDirection.x = 1;
            }
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                velocity.x -= (1 * moveSpeed) / jumpMovementReduction;
                faceDirection.x = -1;
            }
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                velocity.z += (1 * moveSpeed) / jumpMovementReduction;
                faceDirection.z = 1;
            }
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                velocity.z -= (1 * moveSpeed) / jumpMovementReduction;
                faceDirection.z = -1;
            }
        }

        faceDirection.Normalize();
        if(faceDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(faceDirection), rotationLerpAmount);
        }

        velocity = Vector3.ClampMagnitude(velocity, 1 * moveSpeed) * sprintMod * rollMod; //clamping instead of normalizing
        transform.GetComponent<Rigidbody>().velocity = new Vector3(velocity.x, transform.GetComponent<Rigidbody>().velocity.y, velocity.z); //apply velocity to rigidbody
        //transform.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.Impulse);



        //check if on the ground
        if(Physics.Raycast(transform.position, Vector3.down, groundDetectDistance))
        {
            isGrounded = true;
        }
        if(Input.GetAxisRaw("Jump") > 0 && isGrounded)
        {
            isGrounded = false;
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
