using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public PlayerController controller;
    public Animator animator;

    public float runSpeed = 40f;
    float horizontalMove = 0f;
    private bool jump;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("isJumping", true);
        }
    }

    //Animation jumping needs to be handled by an event handler. 
    //Brackeys 2D Animation in Unity: https://www.youtube.com/watch?v=hkaysu1Z-N8&list=PLPV2KyIb3jR6TFcFuzI2bB7TMNIIBpKMQ&index=3
    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
        //Debug.Log("isJumping is false");
    }

    private void FixedUpdate()
    {
        /*
        controller.Jump(jump);
        jump = false;
        */
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
