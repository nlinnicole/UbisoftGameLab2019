using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSpace : MonoBehaviour
{
    [SerializeField]
    private CubeMatchPuzzle puzzle; 

    private bool match = false;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public bool getMatch()
    {
        return match;
    }

    public void OnTriggerStay(Collider other)
    {
        if(puzzle.GetComponent<CubeMatchPuzzle>().checkPuzzleMatch(other.gameObject, gameObject))
        {
            Debug.Log("GOT MATCH");
            match = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        match = false;
    }
}
