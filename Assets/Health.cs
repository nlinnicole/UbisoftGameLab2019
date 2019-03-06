using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Transform bar;
    private Image barFill;

    public bool alive = true;
    public bool onOxygen = false;
    public float startHealth = 100f;
    private float health;
    public float damage = 1f;

    void Start(){
      health = startHealth;

      Transform[] children = GetComponentsInChildren<Transform>();

      foreach(Transform child in children){
        if (child.name == "HealthUI")
          bar = child;
      }

      barFill = bar.GetChild(0).GetChild(0).GetComponent<Image>();
      bar.gameObject.SetActive(false);
    }

    void Update(){
      if (onOxygen && health > 0){
        if (!bar.gameObject.activeSelf)
          bar.gameObject.SetActive(true);

        bar.transform.position = Camera.main.WorldToScreenPoint(transform.position);

        health -= damage;

      } else if (health <= 0){
        alive = false;
        this.gameObject.SetActive(false);
      }

      if (transform.position.y < - 8){
        health = 0;
      }

      barFill.fillAmount = health / startHealth;
    }

    public void Reset(){
      gameObject.SetActive(true);
      alive = true;
      onOxygen = false;
      health = startHealth;
      bar.gameObject.SetActive(false);
    }

}
