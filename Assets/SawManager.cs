using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawManager : MonoBehaviour
{
    public float timer = 0f;

    public float sawspeed = 2f;



    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 7f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if(timer > 1.5f)
        {
            gameObject.transform.Translate(new Vector3(-1,0,0) * Time.deltaTime * sawspeed);
        }
        else
        {
            gameObject.transform.Translate(new Vector3(0, 0, -1f) * Time.deltaTime);
        }
    }


}
