using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSet : MonoBehaviour
{
    [SerializeField]
    private float doorSpeed = 5f;
    [SerializeField]
    private float doorDuration = 3f;

    //door height
    private float doorOpenHeight;
    //door original position 
    private float doorOriginalHeight;

    private float doorTimer;
    private bool doorOpening = false;
    private bool doorClosing = false;

    //Button
    [SerializeField]
    private List<GameObject> buttonList = new List<GameObject>();

    //puzzle
    [SerializeField]
    private GameObject puzzle; 

    void Start()
    {
        //get the height of door to set the height the door will open
        doorOpenHeight = (GetComponent<Collider>().bounds.size.y) * 2;
        doorOriginalHeight = (GetComponent<Collider>().bounds.size.y) / 2;
    }

    void Update()
    {
        //open door until certain height, then close door after certain time
        if (doorOpening)
        {
            if (transform.position.y < doorOpenHeight)
            {
                transform.Translate(Vector3.up * Time.deltaTime * doorSpeed, Space.World);
            }
            else
            {
                doorOpening = false;
                doorClosing = true;
            }
        }
        else if (doorClosing && Time.time > doorTimer + doorDuration)
        {
            if (transform.position.y > doorOriginalHeight)
            {
                transform.Translate(Vector3.down * Time.deltaTime * doorSpeed, Space.World);
            }
            else
            {
                doorClosing = false;
            }
        }


        if (activated())
        {
            if (puzzle.GetComponent<CubeMatchPuzzle>().checkPuzzle())
            {
                Debug.Log("CORRECT!");
                doorTimer = Time.time;
                doorOpening = true;
            }
            else
            {
                setTrap();
            }
        }
    }

    public bool activated()
    {
        for(int i = 0; i <buttonList.Count; i++)
        {
            if (!buttonList[i].GetComponent<PuzzleButton>().getPress())
            {
                return false;
            }
        }
        return true;
    }

    public void setTrap()
    {
        Debug.Log("WRONG!");
    }

}
