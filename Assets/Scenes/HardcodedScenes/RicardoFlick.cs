using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicardoFlick : MonoBehaviour
{
    public GameObject Minimap;

    // Start is called before the first frame update
    private void Awake()
    {

        Minimap = GameObject.FindGameObjectWithTag("Minimap");
        Minimap.GetComponent<WhichRoomAreYouIn>().StartFindingDistance = true;
        Minimap.GetComponent<WhichRoomAreYouIn>().StartDistances();
    }
}