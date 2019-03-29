using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSawsRoomManager : MonoBehaviour
{
    public GameObject SawPrefab;
    public Transform[] SawSpawns;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnSaw", 1f, 1f);
    }

    // Update is called once per frame
    void SpawnSaw()
    {
        Instantiate(SawPrefab, SawSpawns[Random.Range(0,5)].transform.position, SawSpawns[0].transform.rotation);
    }
}
