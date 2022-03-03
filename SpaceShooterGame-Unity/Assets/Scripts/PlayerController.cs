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
    private bool canJump;
    //private bool jumping;
    private float movementSpeed;
    private Vector3 m_Velocity = Vector3.zero;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        lookingRight = true; //Set as true in begining to avoid null flag
        canJump = true;
        //jumping = false;
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
        rb.MovePosition(rb.position + (moveVector * Time.fixedDeltaTime));
        Move(moveVector);
       // Jump(jumping);
        */
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
        if (jump)
        {
            // Add a vertical force to the player.
            rb.AddForce(new Vector2(0f, jumpForce));
        }
    }

}
