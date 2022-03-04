using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    //Parts of this controler taken from Robert Well's Unity 2020 by Example Player Controler and Brackey's CharacterControler2D
    //https://github.com/Brackeys/2D-Character-Controller/blob/master/CharacterController2D.cs

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
    public int maxJumps = 2;

    public UnityEvent OnLandEvent; //Needed to make the jump animation end

    //Internal private variables
    private Rigidbody2D rb;
    private Vector2 moveVector;
    private bool onGround;
    private bool lookingRight;
    //private bool canJump;
    //private bool jumping;
    [SerializeField] private int jumpsAvailable;
    private float movementSpeed;
    private Vector3 m_Velocity = Vector3.zero;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f; //variable to create smoother movement than previous

    //variables to end Flying animation
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        lookingRight = true; //Set as true in begining to avoid null flag
        //canJump = true;
        //jumping = false;
        jumpsAvailable = maxJumps;
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
        //Vector2 movementInput = new Vector2(Input.GetAxisRaw(horzAxis), 0);
        //moveVector = movementInput * speed;

        if(Input.GetButtonDown("Fire1") && canFire)
        {
            canFire = false;
            //Play Sound for ammo
            FindObjectOfType<AudioManager>().Play("Blaster");
            Invoke("Shoot", reloadDelay); //Go to Shoot function after you wait for the reload delay
        }
        
        /*
        if (Input.GetButtonDown(jumpAxis) && canJump)
        {
            jumping = true;
        }
        */

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
        /* 
         * Moved to the new Movement script for better movement and implementation of animations
         * 
        rb.MovePosition(rb.position + (moveVector * Time.fixedDeltaTime));
        Move(moveVector);
       // Jump(jumping);
        */

        //Code to end 
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        bool wasGrounded = m_Grounded;
        m_Grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                    jumpsAvailable = maxJumps; //reset jumps when on ground
                }
            }
        }

    }

    void Shoot()
    {
        //Shooting logic from Brackey's 2D Shooting in Unity
        //https://www.youtube.com/watch?v=wkKsl1Mfp5M
        Instantiate(ammoPrefab, firePoint.position, firePoint.rotation);
        canFire = true;
    }
    /*
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
    */


    //From Brackey's 2D-Character-Controller 
    //https://github.com/Brackeys/2D-Character-Controller/blob/master/CharacterController2D.cs
    void Flip()
    {
        lookingRight = !lookingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    /*
    void Jump(bool isJumping)
    {
        if (isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
        }
        isJumping = false;
    }
    */

    /*
    public void Jump(bool isJumping)
    {
        if (isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
        }
    }
    */

    //From Brackey's 2D-Character-Controller 
    //https://github.com/Brackeys/2D-Character-Controller/blob/master/CharacterController2D.cs
    public void Move(float move, bool crouch, bool jump)
    {

        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
        // And then smoothing it out and applying it to the character
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        // If the input is moving the player right and the player is facing left...
        if (move > 0 && !lookingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move < 0 && lookingRight)
        {
            // ... flip the player.
            Flip();
        }
        // If the player should jump...
        if (jump && jumpsAvailable > 0)
        {
            // Add a vertical force to the player.
            rb.AddForce(new Vector2(0f, jumpForce));
            jumpsAvailable--;
        }
    }

}
