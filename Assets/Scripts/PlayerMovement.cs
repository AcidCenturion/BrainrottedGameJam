using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformerPlayerMove : MonoBehaviour
{
    //FIELDS
    public float playerSpeed;
    public float jumpPower;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private float movementInput;
    private bool jumpInput;
    private RaycastHit2D groundCheck;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocityX = movementInput * playerSpeed;

Debug.DrawRay(transform.position, Vector2.down * 0.6f, Color.red);
        if (jumpInput && isGrounded())
        {
            rb.linearVelocityY = jumpPower;
        }
    }


    // Called by the Player Input component
    /*
    Gets the direction of the input 
    based on bindings connected to the Move action in the Input System asset
    */
    private void OnMove(InputValue input)
    {
        movementInput = input.Get<float>();
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

    /*
    true when raycast detects the groundLayer
    */
    private bool isGrounded()
    {
        groundCheck = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, groundLayer);
        return groundCheck;
    }
}
