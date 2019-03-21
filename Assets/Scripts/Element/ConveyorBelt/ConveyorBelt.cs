using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField]
    private List<ConveyorBlock> blocks;

    [SerializeField]
    private GameObject edge;

    private float length;
    private Vector3 lengthVec; 

    void Start()
    {
        length = GetComponent<Collider>().bounds.size.x;
        lengthVec = new Vector3(length, 0, 0);
    }

    void Update()
    {
        for(int i = 0; i< blocks.Count; i++)
        {
            if(blocks[i].transform.position.x > edge.transform.position.x)
            {
                blocks[i].transform.Translate(blocks[i].transform.position - lengthVec);
            }
        }
    }

}
