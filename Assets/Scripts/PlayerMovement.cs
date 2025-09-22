using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlatformerPlayerMove : MonoBehaviour
{
    // FIELDS
    public float playerSpeed;
    public float dashSpeed;
    public float dashDuration;
    public float dashMaxCooldown;
    public float jumpPower;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private float movementInput;
    private bool dashInput;
    private float dashTimer;
    private float dashCooldown;
    private float faceDirection;
    private float originalGravity;
    private bool jumpInput;
    private RaycastHit2D groundCheck;
    private Animator animator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
        faceDirection = -1; // start facing left

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement();

        dash();

        jump();

        animator.SetFloat("xVelocity", Math.Abs(rb.linearVelocityX));
        animator.SetFloat("yVelocity", rb.linearVelocityY);
    }


    // FUNCTIONS
    private void movement()
    {
        //save the direction the player is facing
        if(movementInput != 0)
        {
            if(faceDirection != movementInput)
            {
Debug.Log("flip");
                Vector3 ls = transform.localScale;
                ls.x *= -1f;
                transform.localScale = ls;
            }
            faceDirection = movementInput;
        }

        //if dash is inputted the timer is started
        if(dashTimer > 0)
        {
            rb.linearVelocityY = 0;
            rb.gravityScale = 0;
            rb.linearVelocityX = faceDirection * dashSpeed;
            dashTimer -= Time.deltaTime;
        }
        //otherwise move as normal
        else
        {
            rb.gravityScale = originalGravity;
            rb.linearVelocityX = movementInput * playerSpeed;
        }
    }

    private void dash()
    {
        if(dashInput && dashCooldown <= 0)
        {
            dashTimer = dashDuration;
            dashCooldown = dashMaxCooldown;
        }
        dashCooldown -= Time.deltaTime;
    }

    private void jump()
    {
Debug.DrawRay(transform.position, Vector2.down * 1f, Color.red);
        if(jumpInput && isGrounded())
        {
            rb.linearVelocityY = jumpPower;
            animator.SetBool("isJumping", true);
        }
        else if(isGrounded())
        {
            animator.SetBool("isJumping", false);
        }
    }


    // HELPER FUNCTIONS
    /*
    true when raycast detects the groundLayer
    set animator to not jumping when on ground
    */
    private bool isGrounded()
    {
        groundCheck = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);
        return groundCheck;
    }


    // ACTION INPUT SYSTEM FUNCTIONS
    /*
    Gets the direction of the input 
    as a positive or negative value (-1, 1)
    based on bindings connected to the Move action in the Input System asset
    */
    private void OnMove(InputValue input)
    {
        movementInput = input.Get<float>();
    }

    /*
    dashInput turned true when button is pressed
    stays true until button is released
    turns false upon release
    */
    private void OnDash(InputValue input)
    {
        dashInput = input.isPressed;
Debug.Log("checked dash" + dashInput);
    }

    /*
    jumpInput turned true when button is pressed
    stays true until button is released
    turns false upon release
    */
    private void OnJump(InputValue input)
    {
        jumpInput = input.isPressed;
Debug.Log("checked jump: " + jumpInput);
    }
}
