using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Health player1;
    public Health player2;
    public RopeGenerator rope;

    public float cooldownBeforeRespawn = 2f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (rope.isBroken){
          if (!player1.onOxygen)
            player1.onOxygen = true;

          if (!player2.onOxygen)
            player2.onOxygen = true;
        }

        if (!player1.alive && !player2.alive){
          Transform[] children = rope.GetComponentsInChildren<Transform>();
          foreach(Transform child in children){
            if (child.name != "Rope")
              GameObject.Destroy(child.gameObject);
          }

          StartCoroutine(Cooldown());
          player1.Reset();
          player2.Reset();

          player1.transform.position = new Vector3(transform.position.x - 6, transform.position.y, transform.position.z);
          player2.transform.position = transform.position;
          rope.generate();
          rope.isBroken = false;
        }
    }

    IEnumerator Cooldown(){
      yield return new WaitForSeconds(cooldownBeforeRespawn);
    }
}
