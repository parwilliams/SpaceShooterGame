using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float MaxSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * MaxSpeed * Time.deltaTime;

        if(transform.position.x < -20 || transform.position.x > 20 || transform.position.z <-20 || transform.position.z > 20)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        Destroy(gameObject);
    }
}
