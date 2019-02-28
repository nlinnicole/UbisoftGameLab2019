using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public int playerNumber = 1;
    public Camera playerCamera;

    [Header("Movement")]
    public float moveSpeed = 1f;
    public float sprintMultiplier;
    [Range(1, 2)]
    public float deceleration = 1f; //lower means slower deceleration
    [Range(0.001f, 0.3f)]
    public float rotationSpeed; //how fast the model turns. higher is faster

    [Header("Jump")]
    public float jumpForce = 5;
    public float jumpMovementReduction = 1;
    public bool isGrounded = false;
    public float jumpCooldown = 0.2f;
    float jumpCooldownCount = 0f;
    public bool jumpCooldownFinished = true;
    public float groundDetectDistance = 0.6f;
    public Vector3 velocity;
    public LayerMask jumpLayerMask;


    [Header("Roll")]
    public bool isRolling = false;
    public float rollMultiplier = 1;
    public float rollDuration = 1;
    public float rollCooldown = 0.5f;
    float rollTime = 0;
    float rollMod = 1;

    [Header("Item")]
    public GameObject heldItem;
    public float itemPickupDistance;
    public GameObject inventory;
    public GameObject swapText;
    public float swapCooldown = 1;
    public float playerSwapDelay = 1;
    float playerSwapCountdown = 0;

    [Header("Rope")]
    public RopeGenerator rope;

    [Header("Ragdoll")]
    public GameObject head;


    //[Header("Ability")]
    //public Ability ability;
    //public enum Ability { anchor, ball }
    //public GameObject body;
    //public bool abilityOn = false;
    //public float ballRadius = 0.01f;

    Vector3 faceDirection;
    Vector3 CamForward;
    Vector3 CamRight;
    float sprintMod = 1;
    LayerMask itemLayerMask;
    Collider[] nearbyItems;

    [HideInInspector]
    public Vector3 feetVelocity;

    void Start()
    {
        itemLayerMask = LayerMask.GetMask("Items");

        //make ball mesh
        //if(ability == Ability.ball)
        //{
        //    Vector3[] verts = body.GetComponent<MeshFilter>().mesh.vertices;
        //    for (int i = 0; i < verts.Length; i++)
        //    {
        //        verts[i] = verts[i].normalized * ballRadius;
        //    }
        //    body.GetComponent<MeshFilter>().mesh.vertices = verts;
        //    gameObject.GetComponent<SphereCollider>().enabled = true;
        //    gameObject.GetComponent<CapsuleCollider>().enabled = false;
        //    body.transform.localPosition = Vector3.zero;
        //}
    }


    //wallsticking on jump may occur if the wall doesnt have a friction-less physics material
    void FixedUpdate()
    {

        if (jumpCooldownCount > 0)
        {
            jumpCooldownCount -= Time.deltaTime;
        }
        else
        {
            jumpCooldownFinished = true;
        }
        //check if on the ground
        if (Physics.Raycast(transform.position, Vector3.down, groundDetectDistance, jumpLayerMask))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (Input.GetButtonDown("Jump" + playerNumber) && isGrounded && jumpCooldownFinished)
        {
            jumpCooldownCount = jumpCooldown;
            jumpCooldownFinished = false;
            isGrounded = false;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

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
                    if (Input.GetButtonDown("Fire" + playerNumber))
                    {
                        playerSwapCountdown = playerSwapDelay;
                       
                        //old item
                        heldItem.layer = 10;
                        heldItem.transform.parent = null;
                        heldItem.GetComponent<Item>().swapCountDown = swapCooldown;
                        heldItem.GetComponent<Rigidbody>().isKinematic = false;
                        heldItem.GetComponent<Rigidbody>().useGravity = true;


                        //new item
                        heldItem = nearbyItems[0].gameObject;
                        heldItem.transform.position = inventory.transform.position;
                        heldItem.transform.parent = inventory.transform;
                        heldItem.layer = 0;
                        heldItem.GetComponent<Rigidbody>().isKinematic = true;
                        heldItem.GetComponent<Rigidbody>().useGravity = false;
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
                heldItem.GetComponent<Rigidbody>().useGravity = false;
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
        if (Input.GetButtonDown("Run" + playerNumber) && isGrounded && !isRolling)
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
            rollMod = 1;
        } else {
            isRolling = false;
            rollMod = 1;
        }


        faceDirection = Vector3.zero;
        CamForward = new Vector3(playerCamera.transform.forward.x, 0, playerCamera.transform.forward.z);
        CamRight = new Vector3(playerCamera.transform.right.x, 0, playerCamera.transform.right.z);
        if (isGrounded)
        {
            //movement
            if (Input.GetAxisRaw("Horizontal" + playerNumber) > 0)
            {
                velocity += CamRight * moveSpeed;
                faceDirection += CamRight;
            }
            if (Input.GetAxisRaw("Horizontal" + playerNumber) < 0)
            {
                velocity -= CamRight * moveSpeed;
                faceDirection += -CamRight;
            }
            if (Input.GetAxisRaw("Vertical" + playerNumber) > 0)
            {
                velocity += CamForward * moveSpeed;
                faceDirection += CamForward;
            }
            if (Input.GetAxisRaw("Vertical" + playerNumber) < 0)
            {
                velocity -= CamForward * moveSpeed;
                faceDirection += -CamForward;
            }

            velocity /= deceleration; //reduce velocity vector to look like drag
        }
        else {
            //reduced movement when jumping
            if (Input.GetAxisRaw("Horizontal" + playerNumber) > 0 || Input.GetAxisRaw("HorizontalJoy" + playerNumber) > 0)
            {
                velocity += (CamRight * moveSpeed) / jumpMovementReduction;
                faceDirection += CamRight;
            }
            if (Input.GetAxisRaw("Horizontal" + playerNumber) < 0)
            {
                velocity -= (CamRight * moveSpeed) / jumpMovementReduction;
                faceDirection += -CamRight;
            }
            if (Input.GetAxisRaw("Vertical" + playerNumber) > 0)
            {
                velocity += (CamForward * moveSpeed) / jumpMovementReduction;
                faceDirection += CamForward;
            }
            if (Input.GetAxisRaw("Vertical" + playerNumber) < 0)
            {
                velocity -= (CamForward * moveSpeed) / jumpMovementReduction;
                faceDirection += -CamForward;
            }
        }

        faceDirection.Normalize();
        if(faceDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(faceDirection), rotationSpeed);
        }


        velocity = Vector3.ClampMagnitude(velocity, 1 * moveSpeed) * sprintMod * rollMod; //clamping instead of normalizing
        //transform.GetComponent<Rigidbody>().velocity = new Vector3(velocity.x, transform.GetComponent<Rigidbody>().velocity.y, velocity.z); //apply velocity to rigidbody

        
        //rope limit
        //if(rope.totalDistance > rope.distanceLimit)
        //{
        //    velocity -= velocity * (rope.totalDistance - rope.distanceLimit);
        //}

        transform.localPosition += velocity/100;

        //if(Input.GetKeyDown(KeyCode.E) && !abilityOn)
        //{
        //    abilityOn = true;
        //    if(ability == Ability.anchor)
        //    {

        //    }
        //    else if(ability == Ability.ball)
        //    {
        //        turnToBall();
        //    }
        //}
        //else if(Input.GetKeyDown(KeyCode.E) && abilityOn)
        //{
        //    abilityOn = false;
        //}
        //void turnToBall()
        //{
        //    Vector3[] verts = body.GetComponent<MeshFilter>().mesh.vertices;
        //    for(int i = 0; i < verts.Length; i++)
        //    {
        //        verts[i] = verts[i].normalized * ballRadius;
        //    }
        //    body.GetComponent<MeshFilter>().mesh.vertices = verts;
        //    gameObject.GetComponent<SphereCollider>().enabled = true;
        //    gameObject.GetComponent<CapsuleCollider>().enabled = false;
        //    body.transform.localPosition = Vector3.zero;
        //}
        //void turnOutOfBall()
        //{

        //}

        feetVelocity = velocity;
    }
    
    
    public void changePlayerSpeed(float speed)
    {
        moveSpeed = speed;
    }

}
 
