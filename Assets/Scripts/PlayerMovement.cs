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
    public LayerMask groundLayer;

    private float movementInput;
    private bool dashInput;
    private float dashTimer;
    private float dashCooldown;
    private float faceDirection;
    private float originalGravity;
    private bool jumpInput;
    private RaycastHit2D groundCheck;


    //new by charlie
    float horizontalInput;
    float moveSpeed = 5f;
    bool isFacingRight = false;
    float jumpPower = 5f;
    bool isGrounded = false;

    Rigidbody2D rb;
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
        faceDirection = -1; // start facing left

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement();

        if(dashInput && dashCooldown <= 0)
        {
            dashTimer = dashDuration;
            dashCooldown = dashMaxCooldown;
        }
        dashCooldown -= Time.deltaTime;

        //new by charlie
        horizontalInput = Input.GetAxis("Horizontal");

        FlipSprite();

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            isGrounded = false;
            animator.SetBool("isJumping", !isGrounded);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
        animator.SetFloat("xVelocity", Math.Abs(rb.linearVelocity.x));
        animator.SetFloat("yVelocity", (rb.linearVelocity.y));
    }

    void FlipSprite() //new by charlie
    {
        if(isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
        animator.SetBool("isJumping", !isGrounded);
    }


    // FUNCTIONS
    private void movement()
    {
        //save the direction the player is facing
        if(movementInput != 0)
        {
            faceDirection = movementInput;
        }

        if(dashTimer > 0)
        {
            rb.linearVelocityY = 0;
            rb.gravityScale = 0;
            rb.linearVelocityX = faceDirection * dashSpeed;
            dashTimer -= Time.deltaTime;
        }
        else
        {
            rb.gravityScale = originalGravity;
            rb.linearVelocityX = movementInput * playerSpeed;
        }
    }

    // ACTION INPUT SYSTEM FUNCTIONS
    /*
    Gets the direction of the input 
    based on bindings connected to the Move action in the Input System asset
    */
    private void OnMove(InputValue input)
    {
        movementInput = input.Get<float>();
    }

    private void OnDash(InputValue input)
    {
        dashInput = input.isPressed;
Debug.Log("dash input");
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
