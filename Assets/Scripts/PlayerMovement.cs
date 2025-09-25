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
    public float coyoteLeniency;

    private Rigidbody2D rb;
    private float movementInput;
    private bool dashInput;
    private float dashTimer;
    private float dashCooldown;
    private float faceDirection;
    private float originalGravity;
    private bool jumpInput;
    private bool jumpRelease;
    private RaycastHit2D groundCheck;
    private float coyoteTime;
    private Animator animator;
    public PlayerHealth PlayerHealth;

    private bool isFalling = false;

    [SerializeField] private AudioClip dashSoundClip;

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
        if (PlayerHealth.hasDied == false)
        {
            movement();
            dash();
            jump();
        }
    }

    // FUNCTIONS
    private void movement()
    {
        //save the direction the player is facing
        if (movementInput != 0)
        {
            if (faceDirection != movementInput)
            {
                Vector3 ls = transform.localScale;
                ls.x *= -1f;
                transform.localScale = ls;
            }
            faceDirection = movementInput;
        }

        //if dash is inputted the timer is started
        if (dashTimer > 0)
        {
            rb.linearVelocity = new Vector2(faceDirection * dashSpeed, 0);
            rb.gravityScale = 0;
            dashTimer -= Time.fixedDeltaTime;
        }
        //otherwise move as normal
        else
        {
            rb.gravityScale = originalGravity;
            rb.linearVelocity = new Vector2(movementInput * playerSpeed, rb.linearVelocity.y);
            animator.SetFloat("xVelocity", Math.Abs(rb.linearVelocity.x));
        }
    }

    private void dash()
    {
        if (dashInput && dashCooldown <= 0)
        {
            dashTimer = dashDuration;
            dashCooldown = dashMaxCooldown;
        }
        dashCooldown -= Time.fixedDeltaTime;
    }

    private void jump()
    {
        Debug.DrawRay(transform.position, Vector2.down * 1f, Color.red);

        // coyote time
        //timer reset
        if (isGrounded())
        {
            coyoteTime = coyoteLeniency;
        }
        //falling after ledge
        else if (!isGrounded() && rb.linearVelocity.y <= 0)
        {
            coyoteTime -= Time.fixedDeltaTime;
        }
        //no coyote time to a regular jump
        else
        {
            coyoteTime = -1;
        }

        // main jump functionality
        //any input during coyote time
        //no jumping during a dash
        if (jumpInput && coyoteTime >= 0 && !(dashTimer > 0))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            animator.SetBool("isJumping", true);
            animator.SetBool("isFalling", false); // Stop falling animation when jumping
        }
        else if (isGrounded())
        {
            // Reset jump/fall state when grounded
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
        else if (rb.linearVelocity.y < 0)
        {
            // NEW: Check for falling
            animator.SetBool("isJumping", false); // No longer jumping
            animator.SetBool("isFalling", true);
        }

        // short hop
        if (!jumpInput && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
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
        if (dashCooldown <= 0)
        {
            SoundFXManager.instance.PlaySoundFXClip(dashSoundClip, transform, 0.33f);
        }
    }

    /*
    jumpInput turned true when button is pressed
    stays true until button is released
    turns false upon release
    */
    private void OnJump(InputValue input)
    {
        jumpInput = input.isPressed;
    }
}
