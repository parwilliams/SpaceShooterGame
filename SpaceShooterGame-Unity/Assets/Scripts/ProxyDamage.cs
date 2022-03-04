using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxyDamage : MonoBehaviour
{
    public HealthBar healthBar;
    //Damage per second
    public float DamageRate = 10f;
    private void OnTriggerStay2D(Collider2D Col){
        Health H = Col.gameObject.GetComponent<Health>();
        //Debug.Log("Triggered");
        if(H == null){
            return;
        }

        H.HealthPoints -= DamageRate * Time.deltaTime;
        healthBar.SetSize(H.HealthPoints / 100);
    }
    
}
