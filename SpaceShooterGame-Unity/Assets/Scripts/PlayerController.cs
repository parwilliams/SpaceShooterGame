using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string horzAxis = "Horizontal";
    public string vertAxis = "Vertical";
    public string fireKey = "Fire1";
    public string jumpAxis = "Jump";
    
    //Editable fields for gameplay
    public float reloadDelay = 0.3f;
    public bool canFire = true;
    public float speed = 5f;
    public float jumpForce = 400f;
    public GameObject ammoPrefab;
    public GameObject hero;
    public Transform firePoint;
    

    //Internal private variables
    private Rigidbody2D rb;
    private Vector2 moveVector;
    private bool onGround;
    private bool lookingRight;
    private float movementSpeed; 
    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        lookingRight = true; //Set as true in begining to avoid null flag
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Movement tutorial from Blackthornprod
        //https://www.youtube.com/watch?v=CeXAiaQOzmY
        Vector2 movementInput = new Vector2(Input.GetAxisRaw(horzAxis), 0);
        moveVector = movementInput * speed;

        if(Input.GetButtonDown("Fire1") && canFire)
        {
            canFire = false;
            Invoke("Shoot", reloadDelay); //Go to Shoot function after you wait for the reload delay
        }

        if (Input.GetButtonDown(jumpAxis))
        {
            Jump();
        }

        if(firePoint.position.x >= hero.transform.position.x)
        {
            lookingRight = true;
        }
        else
        {
            lookingRight = false;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (moveVector * Time.fixedDeltaTime));
        Move(moveVector);
    }

    void Shoot()
    {
        //Shooting logic from Brackey's 2D Shooting in Unity
        //https://www.youtube.com/watch?v=wkKsl1Mfp5M
        Instantiate(ammoPrefab, firePoint.position, firePoint.rotation);
        canFire = true;
    }

    void Move(Vector2 movingVector)
    {
        //From Brackey's 2D-Character-Controller 
        //https://github.com/Brackeys/2D-Character-Controller/blob/master/CharacterController2D.cs

        movementSpeed = movingVector.x;

        // If the input is moving the player right and the player is facing left...
        if (movementSpeed > 0 && !lookingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (movementSpeed < 0 && lookingRight)
        {
            // ... flip the player.
            Flip();
        }
    }

    //From Brackey's 2D-Character-Controller 
    //https://github.com/Brackeys/2D-Character-Controller/blob/master/CharacterController2D.cs
    void Flip()
    {
        lookingRight = !lookingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void Jump()
    {

    }

}
