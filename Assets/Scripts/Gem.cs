using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{

    public ParticleSystem particle;
    public GameObject diamond;

    public float floatAmount = 0.005f;
    public float spinSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            Destroy(GetComponent<SphereCollider>());
            Destroy(diamond);
            other.gameObject.GetComponent<PlayerController>().teamManager.GetComponent<TeamManager>().gemCount++;
            particle.Play();
        }
    }


    private void Update()
    {
        transform.position += new Vector3(0, Mathf.Cos(Time.time) * floatAmount, 0);
        transform.Rotate(new Vector3(0, Time.deltaTime * spinSpeed, 0));
    }
}
