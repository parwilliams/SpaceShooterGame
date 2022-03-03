using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public float Damage = 100f;
    public float LifeTime = 2f;
    public Rigidbody2D rb;
    
    void OnEnable(){
        CancelInvoke();
        Invoke("Die", LifeTime);
        //Debug.Log("Instantiated");
    }

    /*
    void OnTriggerEnter2D(Collider2D Col)
    {
        Debug.Log("Entered Collision");
        Health H = Col.gameObject.GetComponent<Health>();

        if (H == null)
        {
            return;
        }

        H.HealthPoints -= Damage;
        Die();
    }
    */
    void OnCollisionEnter2D(Collision2D collision) //Needed to change to collision, unsure of why
    {
        Health H = collision.gameObject.GetComponent<Health>();

        if (H == null)
        {
            return;
        }

        H.HealthPoints -= Damage;
        Die();
    }

    void Die(){
        gameObject.SetActive(false);
    }
}
