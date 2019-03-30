using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CircleBallController : MonoBehaviour
{
    //If possible, in the future lets add a controller that swaps players from each one.
    public List<GameObject> players;

    public Animator RaiseDoor;

    int boardKillCounter = 0;

    bool playersInRoom = false;

    Transform LastPointHit;

    public Rigidbody rb;

    public Transform centerPoint;

    Vector3 attackDirection;
    public Quaternion targetDirection = Quaternion.identity;

    private float fraction = 0;

    private float cooldown =1f;

    // sound trigger boolz
    bool monsterCollided = false;
    bool startAttacking = false;

    //bool centered = true;
    //bool ReturnToCenter = false;

    // Movement speed in units/sec.
    public float speed = 1.0F;
    public float backspeed = 0.5f;
    private float rotationLerp = 0.0f;
    public float rotationSpeed = 1f;

    public Animator  myAnims;

    public GameObject invisibleWall;


    // Time when the movement started.


    public enum State{rest, target, attack, retreat};
    public State myState = State.rest;


    // ---------------------- sound bool 
    bool chargeStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        int boardKillCounter = 0;
        players = new List<GameObject>();
        rb = gameObject.GetComponent<Rigidbody>();

        myAnims = GetComponentInChildren<Animator>();

        if (invisibleWall == null){
          invisibleWall = GameObject.Find("InvisibleWall");
          invisibleWall.SetActive(false);
        } else {
          if (invisibleWall.activeSelf){
            invisibleWall.SetActive(false);
          }
        }

    }

    private void FixedUpdate()
    {

      switch(myState){
        case State.rest:
          //Debug.Log("Monster state: resting");
          if (playersInRoom){
            cooldown -= Time.deltaTime;
            if(cooldown < 0f)
            {
                cooldown = 1f;
                myState = State.target;
            }
          }
          break;
        case State.target:
          //Debug.Log("Monster state: finding target");
          if (targetDirection == Quaternion.identity){
            targetDirection = Quaternion.LookRotation(players[0].transform.position - transform.position);
            //Debug.Log("Turning towards target: " + targetDirection);
          }

          transform.rotation = Quaternion.Slerp(transform.rotation, targetDirection, rotationLerp * rotationSpeed);
          rotationLerp += Time.deltaTime;

          if (rotationLerp >= 1f){
            if (myAnims.GetBool("isDead"))
              break;

            myState = State.attack;
            attackDirection = transform.forward;
            rotationLerp = 0f;

            myAnims.SetBool("hasHitWall", false);
            myAnims.SetInteger("currentAttack", Random.Range(1, 4));
          }
          break;
        case State.attack:
          //Debug.Log("Monster state: attacking");
          attackPlayer();


          break;
        case State.retreat:
          //Debug.Log("Monster state: recentering");

          if (myAnims.GetCurrentAnimatorStateInfo(0).IsName("Retreat")){
            returnBallToCenter();
            targetDirection = Quaternion.identity;
          }

          break;
      }

      //Debug.Log("players in room :" + playersInRoom);

      CheckBoardKills();
      //Debug.Log(boardKillCounter);

    }

    void attackPlayer()
    {
        transform.position += attackDirection * Time.deltaTime * speed;

        if (!chargeStarted)
        {
            chargeStarted = true;
            AkSoundEngine.PostEvent("startCharge", gameObject);
        }

    }

    void returnBallToCenter()
    {
        if (fraction < 2)
        {
            //Debug.Log("Returning to Center");
            fraction += Time.deltaTime * backspeed;
            gameObject.transform.position = Vector3.Lerp(LastPointHit.position, centerPoint.transform.position, Time.deltaTime * backspeed);
        } else {
          myState = State.rest;
          fraction = 0;

            if (chargeStarted)
            {
                monsterCollided = false;
                chargeStarted = false;
                AkSoundEngine.PostEvent("roar", gameObject);
            }
            
        }
    }

    void CheckBoardKills()
    {
        if(boardKillCounter > 2)
        {
            RaiseDoor.SetBool("OpenExit", true);
            if (!myAnims.GetBool("isDead")){
              myAnims.SetBool("isDead", true);
              Destroy(gameObject, 7);
            }

        }
    }

    void PlayersLeftRoom(){
      Debug.Log("Players not in room");
      players.Clear();
      myState = State.rest;
      invisibleWall.SetActive(false);
      if (playersInRoom){
        playersInRoom = false;
        myAnims.SetBool("playersInRoom", false);
      }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            //Debug.Log("Hit a wall");

            myState = State.retreat;

            LastPointHit = gameObject.transform;

            myAnims.SetBool("hasHitWall", true);
        }

        if (collision.gameObject.tag == "Board")
        {
            boardKillCounter++;
            Destroy(collision.gameObject);
        }

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Rope")){
          Debug.Log("Colliding with rope.");
          PlayersLeftRoom();
        }


        // trigger thud sound when monster hits something
        if (chargeStarted && !monsterCollided)
        {
            monsterCollided = true;
            AkSoundEngine.PostEvent("stopCharge", gameObject);
            AkSoundEngine.PostEvent("roar", gameObject);
        }
    
    }

    private void OnTriggerEnter(Collider other)
    {
        // if(other.tag == "Center")
        // {

            //centered = true;
            //ReturnToCenter = false;


            //startAttacking = false;
        // }

        if(other.tag == "Player")
        {
            if (players.Count > 0){
              if (!players.Contains(other.gameObject)){
                players.Add(other.transform.gameObject);
              }
            } else {
              players.Add(other.transform.gameObject);
            }

            if (players.Count == 2){
              playersInRoom = true;
              myState = State.target;
              targetDirection = Quaternion.LookRotation(players[0].transform.position - transform.position);
              invisibleWall.SetActive(true);

              myAnims.SetBool("playersInRoom", true);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {


      if (players.Count == 0){
        PlayersLeftRoom();
      }
    }

}
