using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    float inputHorizontal;
    float coyoteTimeCounter;
    float jumpBufferCounter;

    public bool isFacingRight = true;
    bool isWallsliding;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    public Vector2 wallJumpingPower = new Vector2(8f, 16f);


    public float speed = 5f;
    public float jump = 10f;
    public float coyoteTime = 0.2f;
    public float jumpBufferTime = 0.2f;
    public float wallslidingspeed = 2f;

    BooDetector boodetectorFlip;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D coll;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform wallCheck;
    [SerializeField] LayerMask wallLayer;

    void Start()
    {
        boodetectorFlip = GetComponent<BooDetector>();
    }

    // update for easy sht
    public void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        JumpInputs();

        Wallslide();

        WallJump();

        if (!isWallJumping)
        {
            Flip();
        }   
    }

    // update for harder sht
    void FixedUpdate()
    {
        if (!isWallJumping)
        {
            Move();
        }
    }

    bool Grounded()
    {
        return coll.IsTouchingLayers(groundLayer);
    }

    bool isWalled()
    { 
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    void JumpInputs()
    {
        if (Grounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            Jump();

            jumpBufferCounter = 0f;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            weak_jump();

            coyoteTimeCounter = 0f;
        }
    }

    void Wallslide()
    {
        if (isWalled() && !Grounded())
        {
            isWallsliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallslidingspeed, float.MaxValue));
        }
        else
        {
            isWallsliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallsliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    void Move()
    {
        rb.velocity = new Vector2(inputHorizontal * speed, rb.velocity.y);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jump);
    }

    void weak_jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
    }

    public void Flip()
    {
        if (inputHorizontal > 0f && !isFacingRight || inputHorizontal < 0f && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
            boodetectorFlip.Flip();
        }
    }
}