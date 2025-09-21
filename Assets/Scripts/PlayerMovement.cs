using UnityEngine;
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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
        faceDirection = -1; // start facing left
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement();

        if(dashInput && dashCooldown <= 0)
        {
            dashTimer = dashDuration;
            dashCooldown = dashMaxCooldown;
        }
        dashCooldown -= Time.deltaTime;

        jump();
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

    private void jump()
    {
 Debug.DrawRay(transform.position, Vector2.down * 0.6f, Color.red);
        if (jumpInput && isGrounded())
        {
            rb.linearVelocityY = jumpPower;
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


    // HELPER FUNCTIONS
    /*
    true when raycast detects the groundLayer
    */
    private bool isGrounded()
    {
        groundCheck = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, groundLayer);
        return groundCheck;
    }
}
