using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;
using XInputDotNetPure; // Required in C#



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

    bool playerIndexSet = false;
    PlayerIndex playerIndex1;
    PlayerIndex playerIndex2;
    GamePadState state1;
    GamePadState state2;
    GamePadState prevState;


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

    private void Start()
    {
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    playerIndex1 = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        playerIndexSet = false;

        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected && testPlayerIndex != playerIndex1)
                {
                    playerIndex1 = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        state1 = GamePad.GetState(playerIndex1);
        state2 = GamePad.GetState(playerIndex2);

    }

    private void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        state1 = GamePad.GetState(playerIndex1);
        state2 = GamePad.GetState(playerIndex2);

        if (playerNumber == 1)
        {
            if (state1.Buttons.B == ButtonState.Pressed && !rope.isBroken && !GetComponent<Health>().onOxygen)
            {
                holdReset();
            }
        }

        if (playerNumber == 2)
        {
            if (state2.Buttons.B == ButtonState.Pressed && !rope.isBroken && !GetComponent<Health>().onOxygen)
            {
                holdReset();
            }
        }

        if(Input.GetKey(KeyCode.R) && !rope.isBroken && !GetComponent<Health>().onOxygen)
        {
            holdReset();
        }

        if (state2.Buttons.B != ButtonState.Pressed && state1.Buttons.B != ButtonState.Pressed && !Input.GetKey(KeyCode.R))
        {
            resetImage.fillAmount -= Time.deltaTime / 3;
        }
    }

    void FixedUpdate()
    {


        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        //death zones
        if (inDeathZone && !rope.isBroken)
        {
            if(playerNumber == 1)
            {
                rope.ropeJoints[5].GetComponent<RopeJoint>().broken = true;
                rope.isBroken = true;
                playerCamera.GetComponentInParent<CamPlayerFollow>().player1Dead = true;
            }
            else if (playerNumber == 2)
            {
                rope.ropeJoints[rope.ropeJoints.Length-5].GetComponent<RopeJoint>().broken = true;
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

 

        //if(Input.GetButton("Reset" + playerNumber) && !rope.isBroken && !GetComponent<Health>().onOxygen)
        //{
        //    holdReset();
        //}

 


        //roll
        if(CamParent.GetComponent<CamPlayerFollow>().viewangle == 0 || CamParent.GetComponent<CamPlayerFollow>().viewangle == 3 || CamParent.GetComponent<CamPlayerFollow>().viewangle == 2 || CamParent.GetComponent<CamPlayerFollow>().viewangle == 4)
        {
            if(playerNumber == 1)
            {
                if (state1.Buttons.A == ButtonState.Pressed && isGrounded && !isRolling)
                {
                    isGrounded = false;
                    isRolling = true;
                    rollTime = 0;
                }
            }

            if (playerNumber == 2)
            {
                if (state2.Buttons.A ==ButtonState.Pressed && isGrounded && !isRolling)
                {
                    isGrounded = false;
                    isRolling = true;
                    rollTime = 0;
                }
            }

            //if (Input.GetButtonDown("Run" + playerNumber) && isGrounded && !isRolling)
            //{
            //    isGrounded = false;
            //    isRolling = true;
            //    rollTime = 0;
            //}
        }
        else
        {
            if(playerNumber == 1)
            {
                if (state1.Buttons.A == ButtonState.Pressed && isGrounded && !isRolling)
                {
                    GetComponent<Rigidbody>().AddForce(0, jumpForce, 0, ForceMode.Impulse);
                }
            }

            if (playerNumber == 2)
            {
                if (state2.Buttons.A == ButtonState.Pressed && isGrounded && !isRolling)
                {
                    GetComponent<Rigidbody>().AddForce(0, jumpForce, 0, ForceMode.Impulse);
                }
            }

            //if (Input.GetButtonDown("Run" + playerNumber) && isGrounded && !isRolling)
            //{
            //    GetComponent<Rigidbody>().AddForce(0, jumpForce, 0, ForceMode.Impulse);
            //}
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
            if(playerNumber == 1)
            {
                //movement
                if (state1.ThumbSticks.Left.X > 0)
                {
                    velocity += CamRight * moveSpeed * 100;
                    faceDirection += CamRight;
                }
                if (state1.ThumbSticks.Left.X < 0)
                {
                    velocity -= CamRight * moveSpeed * 100;
                    faceDirection += -CamRight;
                }
                if (state1.ThumbSticks.Left.Y > 0)
                {
                    velocity += CamForward * moveSpeed * 100;
                    faceDirection += CamForward;
                }
                if (state1.ThumbSticks.Left.Y < 0)
                {
                    velocity -= CamForward * moveSpeed * 100;
                    faceDirection += -CamForward;
                }
            }

            if (playerNumber == 2)
            {
                //movement
                if (state2.ThumbSticks.Left.X > 0)
                {
                    velocity += CamRight * moveSpeed * 100;
                    faceDirection += CamRight;
                }
                if (state2.ThumbSticks.Left.X < 0)
                {
                    velocity -= CamRight * moveSpeed * 100;
                    faceDirection += -CamRight;
                }
                if (state2.ThumbSticks.Left.Y > 0)
                {
                    velocity += CamForward * moveSpeed * 100;
                    faceDirection += CamForward;
                }
                if (state2.ThumbSticks.Left.Y < 0)
                {
                    velocity -= CamForward * moveSpeed * 100;
                    faceDirection += -CamForward;
                }
            }


        }
        else
        {
            ////reduced movement when jumping
            if (playerNumber == 1)
            {
                //movement
                if (state1.ThumbSticks.Left.X > 0)
                {
                    velocity += CamRight * moveSpeed * 100 / jumpMovementReduction;
                    faceDirection += CamRight;
                }
                if (state1.ThumbSticks.Left.X < 0)
                {
                    velocity -= CamRight * moveSpeed * 100 / jumpMovementReduction;
                    faceDirection += -CamRight;
                }
                if (state1.ThumbSticks.Left.Y > 0)
                {
                    velocity += CamForward * moveSpeed * 100 / jumpMovementReduction;
                    faceDirection += CamForward;
                }
                if (state1.ThumbSticks.Left.Y < 0)
                {
                    velocity -= CamForward * moveSpeed * 100 / jumpMovementReduction;
                    faceDirection += -CamForward;
                }
            }

            if (playerNumber == 2)
            {
                //movement
                if (state2.ThumbSticks.Left.X > 0)
                {
                    velocity += CamRight * moveSpeed * 100 / jumpMovementReduction;
                    faceDirection += CamRight;
                }
                if (state2.ThumbSticks.Left.X < 0)
                {
                    velocity -= CamRight * moveSpeed * 100 / jumpMovementReduction;
                    faceDirection += -CamRight;
                }
                if (state2.ThumbSticks.Left.Y > 0)
                {
                    velocity += CamForward * moveSpeed * 100 / jumpMovementReduction;
                    faceDirection += CamForward;
                }
                if (state2.ThumbSticks.Left.Y < 0)
                {
                    velocity -= CamForward * moveSpeed * 100 / jumpMovementReduction;
                    faceDirection += -CamForward;
                }
            }

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

            //roll velocity to rigidbody
        }


        //for anim
        if (isGrounded)
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

        if (resetImage.fillAmount >= 1)
        {
            GetComponent<Health>().onOxygen = true;
            resetImage.fillAmount = 0;
        }


    }
}
