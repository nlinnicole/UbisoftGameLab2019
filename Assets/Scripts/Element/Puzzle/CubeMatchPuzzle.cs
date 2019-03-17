using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMatchPuzzle : MonoBehaviour {

    [SerializeField]
    private List<GameObject> puzzleCubeArray = new List<GameObject>();
    [SerializeField]
    private List<GameObject> puzzleSpaceArray = new List<GameObject>();

    void Start()
    {

    }

    void Update()
    {
        
    }

    public bool checkPuzzleMatch(GameObject cube, GameObject space)
    {
        bool result = false;

        if(puzzleCubeArray.IndexOf(cube) == puzzleSpaceArray.IndexOf(space))
        {
            result = true;
        }

        return result;
    }

    public bool checkPuzzle()
    {
        for (int i = 0; i<puzzleSpaceArray.Count; i++)
        {
            if (!puzzleSpaceArray[i].GetComponent<PuzzleSpace>().getMatch())
            {
                return false;
            }
        }

        return true;
    }
}
