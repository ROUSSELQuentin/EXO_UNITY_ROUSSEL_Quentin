using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    InputManager inputManager;
    GameObject gameManager;

    [Header("Walk and run")]
    [SerializeField] float playerSpeed;
    [SerializeField] float startPlayerSpeed;
    private float moveInput;
    private bool facingRight;
    private Rigidbody2D rb2D;
    [SerializeField] float playerRunSpeed;
    [SerializeField] float playerRunSpeedTimer;

    [Header("Jump")]
    private bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask whatIsGrounded;
    private int extraJumps;
    [SerializeField] int extraJumpsValue;
    [SerializeField] float jumpForce;


    [Header("Dash")]
    private int direction;
    [SerializeField] float dashSpeed; 
    private float dashTime;
    [SerializeField] float startDashTime;
    private bool asDash;


    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        extraJumps = extraJumpsValue;
        dashTime = startDashTime;
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        inputManager = gameManager.GetComponent<InputManager>();
    }      

    void Update()
    {
        Jump();
        Dash();      

    }

    private void FixedUpdate()
    {
        IsGrounded();
        MoveInput();
    }

    void IsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGrounded);

    }

    void MoveInput()
    {
        inputManager._moveInput = Input.GetAxisRaw("Horizontal");

        if(inputManager._moveInput == 0)
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            playerSpeed = startPlayerSpeed;
        }

        if (inputManager._moveInput != 0 && inputManager._runInput == false)
        {
            rb2D.velocity = new Vector2(inputManager._moveInput * playerSpeed, rb2D.velocity.y);
        }

        if (isGrounded == false)
        {
            inputManager._runInput = false;
        }

        if (inputManager._moveInput != 0 && inputManager._runInput)
        {                
            Debug.Log("in run");
            playerSpeed = playerRunSpeed;
        } 

        if (facingRight == false && inputManager._moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && inputManager._moveInput < 0)
        {
            Flip();
        }

        
    }    

    void Jump()
    {
        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
            asDash = false;
        }

        //Input.GetKeyDown(KeyCode.Joystick1Button1)

        if (inputManager._jumpInput && extraJumps > 0)
        {
            rb2D.velocity = Vector2.up * jumpForce;
            extraJumps--;

            asDash = false;
        }
        else if (inputManager._jumpInput && extraJumps <= 0 && isGrounded == true)
        {
            rb2D.velocity = Vector2.up * jumpForce;
        }
    }

    void Dash()
    {
        if (direction == 0)
        {
            if (inputManager._dashInput && inputManager._moveInput > 0)
            {
                direction = 1;
            }
            else if (inputManager._dashInput && inputManager._moveInput < 0)
            {
                direction = 2;
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                playerSpeed = startPlayerSpeed;
            }
            else
            {
                dashTime -= Time.deltaTime;

                if (direction == 1 && isGrounded == false && asDash == false)
                {
                    playerSpeed = dashSpeed * inputManager._moveInput;
                    asDash = true;
                }
                else if (direction == 2 && isGrounded == false && asDash == false)
                {
                    playerSpeed = dashSpeed * -inputManager._moveInput;
                    asDash = true;
                }
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
