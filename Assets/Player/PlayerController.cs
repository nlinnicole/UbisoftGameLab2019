using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;


public class PlayerController : MonoBehaviourPunCallbacks
{

    public int playerNumber = 1;
    public Camera playerCamera;
    public GameObject teamManager;
    public bool inDeathZone = false;
    public Image resetImage;

    [Header("Movement")]
    public float moveSpeed = 1f;
    public float maxSpeed = 5;
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
    float rollMod = 0;

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

    [Header("Joystick")]
    public float dead = 0.5f;

    [Header("Networking")]
    public static GameObject LocalPlayerInstance;


    //[Header("Ability")]
    //public Ability ability;
    //public enum Ability { anchor, ball }
    //public GameObject body;
    //public bool abilityOn = false;
    //public float ballRadius = 0.01f;

    Vector3 faceDirection;
    Vector3 CamForward;
    Vector3 CamRight;
    Vector3 CamTopDown;


    //Angle of Camera
    public GameObject CamParent;

    


    float sprintMod = 1;
    LayerMask itemLayerMask;
    Collider[] nearbyItems;

    [HideInInspector]
    public Vector3 feetVelocity;


    private Animator anim;

    private Vector3 joyInput;



    ////check deathzone
    //private void OnCollisionStay(Collision collision)
    //{
    //    if(collision.gameObject.layer == 15)
    //    {
    //        isInDeathZone = true;
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.layer == 15)
    //    {
    //        isInDeathZone = false;
    //    }
    //}

    void Awake()
    {
        itemLayerMask = LayerMask.GetMask("Items");
        anim = GetComponent<Animator>();
        if (photonView.IsMine)
        {
            playerCamera.enabled = true;
            PlayerController.LocalPlayerInstance = this.gameObject;
        }

    }


    void FixedUpdate()
    {


        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;

        }

        //death zones
        if (inDeathZone)
        {
            if(playerNumber == 1)
            {
                rope.ropeJoints[2].GetComponent<RopeJoint>().broken = true;
                rope.isBroken = true;
                playerCamera.GetComponentInParent<CamPlayerFollow>().player1Dead = true;
            }
            else if (playerNumber == 2)
            {
                rope.ropeJoints[rope.ropeJoints.Length-2].GetComponent<RopeJoint>().broken = true;
                rope.isBroken = true;
                playerCamera.GetComponentInParent<CamPlayerFollow>().player2Dead = true;

            }
        }


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

        if(Input.GetButton("Reset" + playerNumber) && !rope.isBroken && !GetComponent<Health>().onOxygen)
        {
            holdReset();
        }
        
        if(!Input.GetButton("Reset" + 1) && !Input.GetButton("Reset" + 2))
        {
            resetImage.fillAmount -= Time.deltaTime/3;
        }




        //if (Input.GetButtonDown("Jump" + playerNumber) && isGrounded && jumpCooldownFinished)
        //{
        //    jumpCooldownCount = jumpCooldown;
        //    jumpCooldownFinished = false;
        //    isGrounded = false;
        //    gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //    gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        //}

        ////item pickup
        //nearbyItems = new Collider[Physics.OverlapSphere(transform.position, itemPickupDistance / 2, itemLayerMask).Length];
        //nearbyItems = Physics.OverlapSphere(transform.position, itemPickupDistance / 2, itemLayerMask);
        //if (nearbyItems.Length > 0)
        //{

        //    if (nearbyItems.Length > 0 && heldItem != null)
        //    {
        //        if (nearbyItems[0].GetComponent<Item>().swapCountDown <= 0 && playerSwapCountdown <= 0)
        //        {
        //            swapText.gameObject.SetActive(true);
        //            if (Input.GetButtonDown("Fire" + playerNumber))
        //            {
        //                playerSwapCountdown = playerSwapDelay;

        //                //old item
        //                heldItem.layer = 10;
        //                heldItem.transform.parent = null;
        //                heldItem.GetComponent<Item>().swapCountDown = swapCooldown;
        //                heldItem.GetComponent<Rigidbody>().isKinematic = false;
        //                heldItem.GetComponent<Rigidbody>().useGravity = true;


        //                //new item
        //                heldItem = nearbyItems[0].gameObject;
        //                heldItem.transform.position = inventory.transform.position;
        //                heldItem.transform.parent = inventory.transform;
        //                heldItem.layer = 0;
        //                heldItem.GetComponent<Rigidbody>().isKinematic = true;
        //                heldItem.GetComponent<Rigidbody>().useGravity = false;
        //            }
        //        }
        //        else
        //        {
        //            swapText.gameObject.SetActive(false);
        //        }
        //    }
        //    else
        //    {
        //        swapText.gameObject.SetActive(false);
        //    }



        //    if (heldItem == null)
        //    {
        //        heldItem = nearbyItems[0].gameObject;
        //        heldItem.transform.position = inventory.transform.position;
        //        heldItem.transform.parent = inventory.transform;
        //        heldItem.layer = 0;
        //        heldItem.GetComponent<Rigidbody>().isKinematic = true;
        //        heldItem.GetComponent<Rigidbody>().useGravity = false;
        //    }



        //}
        //else
        //{
        //    //swapText.gameObject.SetActive(false);
        //}

        //playerSwapCountdown -= Time.deltaTime;



        //roll
        if(CamParent.GetComponent<CamPlayerFollow>().viewangle == 0 || CamParent.GetComponent<CamPlayerFollow>().viewangle == 3 || CamParent.GetComponent<CamPlayerFollow>().viewangle == 2 || CamParent.GetComponent<CamPlayerFollow>().viewangle == 4)
        {
            if (Input.GetButtonDown("Run" + playerNumber) && isGrounded && !isRolling)
            {
                isGrounded = false;
                isRolling = true;
                rollTime = 0;
            }
        }
        else
        {
            if (Input.GetButtonDown("Run" + playerNumber) && isGrounded && !isRolling)
            {
                GetComponent<Rigidbody>().AddForce(0, jumpForce, 0, ForceMode.Impulse);
            }
        }
        


        if (isRolling && rollTime < rollDuration)
        {
            rollMod = rollMultiplier;
            rollTime += Time.deltaTime;
            isGrounded = false;
            GetComponent<Rigidbody>().useGravity = false;
        }
        else if (isRolling && rollTime < rollDuration + rollCooldown)
        {
            GetComponent<Rigidbody>().useGravity = true;
            rollTime += Time.deltaTime;
            rollMod = 0;
        }
        else
        {
            isRolling = false;
            rollMod = 0;
        }

        velocity = Vector3.zero;
        faceDirection = Vector3.zero;
        CamForward = new Vector3(playerCamera.transform.forward.x, 0, playerCamera.transform.forward.z);
        CamRight = new Vector3(playerCamera.transform.right.x, 0, playerCamera.transform.right.z);
        //TopDownChanges
        CamTopDown = new Vector3(playerCamera.transform.up.x, 0, playerCamera.transform.up.z);

        //keys
        if (isGrounded)
        {
            //Default Camera State Movement
            if(CamParent.GetComponent<CamPlayerFollow>().viewangle == 0 || CamParent.GetComponent<CamPlayerFollow>().viewangle == 3 || CamParent.GetComponent<CamPlayerFollow>().viewangle == 4)
            {
                //movement
                if (Input.GetAxisRaw("Horizontal" + playerNumber) > 0)
                {
                    velocity += CamRight * moveSpeed * 100;
                    faceDirection += CamRight;
                }
                if (Input.GetAxisRaw("Horizontal" + playerNumber) < 0)
                {
                    velocity -= CamRight * moveSpeed * 100;
                    faceDirection += -CamRight;
                }
                if (Input.GetAxisRaw("Vertical" + playerNumber) > 0)
                {
                    velocity += CamForward * moveSpeed * 100;
                    faceDirection += CamForward;
                }
                if (Input.GetAxisRaw("Vertical" + playerNumber) < 0)
                {
                    velocity -= CamForward * moveSpeed * 100;
                    faceDirection += -CamForward;
                }
            }
            //TopDownCamera Movement
            if(CamParent.GetComponent<CamPlayerFollow>().viewangle == 2)
            {
                //movement
                if (Input.GetAxisRaw("Horizontal" + playerNumber) > 0)
                {
                    velocity += CamRight * moveSpeed * 100;
                    faceDirection += CamRight;
                }
                if (Input.GetAxisRaw("Horizontal" + playerNumber) < 0)
                {
                    velocity -= CamRight * moveSpeed * 100;
                    faceDirection += -CamRight;
                }
                if (Input.GetAxisRaw("Vertical" + playerNumber) > 0)
                {
                    velocity += CamTopDown * moveSpeed * 100;
                    faceDirection += CamTopDown;
                }
                if (Input.GetAxisRaw("Vertical" + playerNumber) < 0)
                {
                    velocity -= CamTopDown * moveSpeed * 100;
                    faceDirection += -CamTopDown;
                }
            }

            if(CamParent.GetComponent<CamPlayerFollow>().viewangle == 1)
            {
                if (Input.GetAxisRaw("Horizontal" + playerNumber) > 0)
                {
                    velocity += CamRight * moveSpeed * 100;
                    faceDirection += CamRight;
                }
                if (Input.GetAxisRaw("Horizontal" + playerNumber) < 0)
                {
                    velocity -= CamRight * moveSpeed * 100;
                    faceDirection += -CamRight;
                }
                
            }

        }
        else
        {
            //reduced movement when jumping
            if (Input.GetAxisRaw("Horizontal" + playerNumber) > 0)
            {
                velocity += (CamRight * moveSpeed * 100) / jumpMovementReduction;
                faceDirection += CamRight;
            }
            if (Input.GetAxisRaw("Horizontal" + playerNumber) < 0)
            {
                velocity -= (CamRight * moveSpeed * 100) / jumpMovementReduction;
                faceDirection += -CamRight;
            }
            if (Input.GetAxisRaw("Vertical" + playerNumber) > 0)
            {
                velocity += (CamForward * moveSpeed * 100) / jumpMovementReduction;
                faceDirection += CamForward;
            }
            if (Input.GetAxisRaw("Vertical" + playerNumber) < 0)
            {
                velocity -= (CamForward * moveSpeed * 100) / jumpMovementReduction;
                faceDirection += -CamForward;
            }
        }

        //joysticks
        joyInput = Vector3.zero;
        if (isGrounded)
        {

            if (Input.GetAxisRaw("HorizontalJoy" + playerNumber) != 0
                || Input.GetAxisRaw("VerticalJoy" + playerNumber) != 0)
            {
                joyInput = Camera.main.transform.TransformDirection(new Vector3(Input.GetAxisRaw("HorizontalJoy" + playerNumber), 0, Input.GetAxisRaw("VerticalJoy" + playerNumber)).normalized);
            }


            velocity += joyInput * moveSpeed * 100;
            faceDirection += joyInput;
        }
        else
        {
            ////reduced movement when jumping
            if (Input.GetAxisRaw("HorizontalJoy" + playerNumber) != 0
                || Input.GetAxisRaw("VerticalJoy" + playerNumber) != 0)
            {
                joyInput = Camera.main.transform.TransformDirection(new Vector3(Input.GetAxisRaw("HorizontalJoy" + playerNumber), 0, Input.GetAxisRaw("VerticalJoy" + playerNumber)).normalized);
            }

            velocity += (joyInput * moveSpeed * 100) / jumpMovementReduction;
            faceDirection += joyInput;
        }

        faceDirection.Normalize();
        if (faceDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(faceDirection), rotationSpeed);
        }

        //velocity = Vector3.ClampMagnitude(velocity, 1 * moveSpeed) * sprintMod * rollMod; //clamping instead of normalizing
        if ( Mathf.Abs(transform.GetComponent<Rigidbody>().velocity.x) + Mathf.Abs(transform.GetComponent<Rigidbody>().velocity.z) > maxSpeed)
        {
            transform.GetComponent<Rigidbody>().velocity = new Vector3(transform.GetComponent<Rigidbody>().velocity.x/maxSpeed, transform.GetComponent<Rigidbody>().velocity.y, transform.GetComponent<Rigidbody>().velocity.z / maxSpeed);
        }

        if(CamParent.GetComponent<CamPlayerFollow>().viewangle == 0 || CamParent.GetComponent<CamPlayerFollow>().viewangle == 3 || CamParent.GetComponent<CamPlayerFollow>().viewangle == 4)
        {

            transform.GetComponent<Rigidbody>().AddForce(new Vector3(velocity.x, 0, velocity.z)); //apply velocity to rigidbody


            transform.GetComponent<Rigidbody>().AddForce(new Vector3(faceDirection.x, 0, faceDirection.z) * rollMod * 1000); //roll velocity to rigidbody

        }
        else if(CamParent.GetComponent<CamPlayerFollow>().viewangle == 1)
        {

            transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, velocity.z));

        }else if(CamParent.GetComponent<CamPlayerFollow>().viewangle == 2)
        {
            transform.GetComponent<Rigidbody>().AddForce(new Vector3(velocity.x, 0, velocity.y*-1)); //apply velocity to rigidbody


            transform.GetComponent<Rigidbody>().AddForce(new Vector3(faceDirection.x, 0, faceDirection.y*-1) * rollMod * 1000); //roll velocity to rigidbody
        }


        //for anim
        if(isGrounded)
        {
            GetComponent<Animator>().SetFloat("PlayerVelocity", GetComponent<Rigidbody>().velocity.magnitude);

        } else
        {
            GetComponent<Animator>().SetFloat("PlayerVelocity", 0f);

        }


    }


    public void changePlayerSpeed(float speed)
    {
        moveSpeed = speed;
    }


    
    void holdReset()
    {

        resetImage.fillAmount += Time.deltaTime/3;

        if(resetImage.fillAmount >= 1)
        {
            GetComponent<Health>().onOxygen = true;
            resetImage.fillAmount = 0;
        }


    }
}
