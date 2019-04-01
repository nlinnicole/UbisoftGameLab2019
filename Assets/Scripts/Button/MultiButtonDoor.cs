using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;



/*
 *  Players need to press all buttons in order to activate an event
 */
public class MultiButtonDoor : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject door1;
    [SerializeField]
    private GameObject door2;

    [SerializeField]
    private float doorSpeed = 5f;

    //door height
    private float door1OpenHeight;
    //door original position 
    private float door1OriginalHeight;

    //Button
    [SerializeField]
    private List<GameObject> buttonList = new List<GameObject>();

    [SerializeField]
    private float timer = 3f;

    private float timeCounter = 0;

    private bool doorOpening = true;

    private bool setTimer = true;

    public bool yes = false;

    [SerializeField]
    private Canvas canvas;

    void Start()
    {
        door1OpenHeight = (door1.GetComponent<Collider>().bounds.size.y) * 2;
        door1OriginalHeight = (door1.GetComponent<Collider>().bounds.size.y) / 2;

    }

    void Update()
    {
        checkReady();

        if (yes)
        {
            if (setTimer)
            {
                timeCounter = Time.time;
                canvas.GetComponent<Countdown>().enabled = true;
                setTimer = false;
            }

            if (timeCounter + timer < Time.time && doorOpening)
            {
                if (door1.transform.position.y < door1OpenHeight)
                {
                    door1.transform.Translate(Vector3.up * Time.deltaTime * doorSpeed, Space.World);
                    door2.transform.Translate(Vector3.up * Time.deltaTime * doorSpeed, Space.World);
                }
                else
                {
                    doorOpening = false;

                }

            }
        }

    }

    //return true if all buttons are pressed
    public void checkReady()
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            if (!buttonList[i].GetComponent<MultiButton>().getPressed())
            {
                return;
            }
        }
        photonView.RPC("SetTrig", RpcTarget.All);
    }

    [PunRPC]
    void SetTrig()
    {
        yes = true;
    }


}
