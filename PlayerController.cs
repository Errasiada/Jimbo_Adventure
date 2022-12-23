using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("InputChecks")]
    public float moveSpeed;
    public float jumpForce;
    private bool canDoubleJump;
    private float horizontal;
    private float movingInput;

    private bool canMove;

    [Header("Collision")]
    public LayerMask whatisGround;
    public float wallCheckDistance;
    public float groundCheckDistance;
    private bool isGrounded;
    private bool isWallDetected;
    private bool canWallSlide;
    private bool isWallSliding;

    [Header("Knockback")] 
    [SerializeField] private Vector2 knockbackDirection;

    [SerializeField] private float knockbackTime;
    private bool isKnocked;
    
    [Header("Flip")]
    private bool facingRight = true;
    private int facingDirection = 1;

    public int fruits;

    private Animator anim;
    public Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

   
    void Update()
    {
        AnimationControllers();
        
        if (isKnocked)
            return;
        
        FlipController();
        CollisionDetection();
        InputChecks();

        if (canWallSlide)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
        }
        
        if (isGrounded)
        {
            canDoubleJump = true;
            canMove = true;
        }

    }

    private void AnimationControllers()
    {
        bool isMoving = rb.velocity.x != 0;
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isWallSliding", isWallSliding);
        anim.SetBool("isWallDetected", isWallDetected);
        anim.SetBool("isKnocked", isKnocked);
    }

    private void JumpButton()
    {
        if (isWallSliding)
        {
            WallJump();
        }
        if (isGrounded)
        {
            Jump();
        }
        else if (canDoubleJump)
        {
            canDoubleJump = false;
            Jump();
        }

        canWallSlide = false;
    }
    private void Jump()
    {
        Debug.Log("Jump");
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void WallJump()
    {
        canMove = false;
        rb.velocity = new Vector2(6 * -facingDirection, jumpForce);
    }

   public void Knockback(int direction)
    {
        isKnocked = true;
        rb.velocity = new Vector2(knockbackDirection.x * direction, knockbackDirection.y);
        Invoke("CancelKnockback", knockbackTime);
      
        fruits--;
        if (fruits < 0)
        {
            Destroy(gameObject);
        }  
    }

    private void CancelKnockback()
    {
        isKnocked = false;
    }
    private void InputChecks()
    {
        movingInput = Input.GetAxisRaw("Horizontal"); 
        if (canMove)
            rb.velocity = new Vector2(moveSpeed * movingInput, rb.velocity.y);

        if (Input.GetAxis("Vertical") < 0)
            canWallSlide = false;
        
        if (Input.GetKeyDown(KeyCode.Space))
            JumpButton();
        
        
    }

    private void CollisionDetection()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatisGround);
        isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, whatisGround);

        if (isWallDetected && rb.velocity.y < 0)
            canWallSlide = true;
        
        if (!isWallDetected)
        {
            isWallSliding = false;
            canWallSlide = false;
        }
           
    }
    
    private void FlipController()
    {
        if (facingRight && movingInput < 0)
            Flip();
        else if (!facingRight && movingInput > 0)
            Flip();
    }
    private void Flip()
    {
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + wallCheckDistance * facingDirection , transform.position.y));
    }
}
