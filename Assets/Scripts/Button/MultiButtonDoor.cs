using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Players need to press all buttons in order to activate an event
 */
public class MultiButtonDoor : MonoBehaviour
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

    void Start()
    {
        door1OpenHeight = (door1.GetComponent<Collider>().bounds.size.y) * 2;
        door1OriginalHeight = (door1.GetComponent<Collider>().bounds.size.y) / 2;

    }

    void Update()
    {

        if (checkReady())
        {
            if (setTimer)
            {
                timeCounter = Time.time;
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
    public bool checkReady()
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            if (!buttonList[i].GetComponent<MultiButton>().getPressed())
            {
               
                return false;
            }
        }
        return true;
    }
}
