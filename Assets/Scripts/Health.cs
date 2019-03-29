using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Transform bar;
    public Image healthBar;
    public GameObject cam;

    public float deathzone = -10f;

    public bool alive = true;
    public bool onOxygen = false;
    private float maxHealth = 100f;
    public float health;
    public float damage = 1f;


    void Start(){
        maxHealth = health;

      Transform[] children = GetComponentsInChildren<Transform>();

      //foreach(Transform child in children){
      //  if (child.name == "HealthUI")
      //    bar = child;
      //}

      //healthBar = bar.GetChild(0).GetChild(0).GetComponent<Image>();
      //bar.gameObject.SetActive(false);
    }
    
    void FixedUpdate(){
      if (onOxygen){
        if (health <= 0)
          Die();
        else
          TakeDamage();
      }

      //if (transform.position.y < deathzone){
      //  GetComponent<PlayerController>().rope.isBroken = true;
      //  Die();
      //}
    }


    void TakeDamage(){
      //if (!bar.gameObject.activeSelf)
      //  bar.gameObject.SetActive(true);
      
        health -= /*damage * 100 **/ Time.deltaTime;

        //healthBar.fillAmount = health / maxHealth;


        cam.GetComponent<Animator>().SetTrigger("isDying");




    }

    void Die(){
      alive = false;



        this.gameObject.SetActive(false);
    }

    public void Reset(){
      gameObject.SetActive(true);
      alive = true;
      onOxygen = false;
      health = maxHealth;
      //bar.gameObject.SetActive(false);
    }

}
