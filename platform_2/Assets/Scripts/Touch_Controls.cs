using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Touch_Controls : MonoBehaviour

{
    float inputHorizontal;
    float coyoteTimeCounter;
    float jumpBufferCounter;
    float lastGroundedTime;  // Track the time when the player was last grounded

    bool isFacingRight = true;
    bool isWallsliding;
    bool jumpButtonPressed;

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

    public RectTransform jumpButtonRect; // Reference to the RectTransform of the UI Jump Button

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D coll;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform wallCheck;
    [SerializeField] LayerMask wallLayer;

    void Update()
    {
        HandleTouchInput();

        JumpInput();

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

    void HandleTouchInput()
    {
        // Horizontal movement
        inputHorizontal = SimpleInput.GetAxisRaw("Horizontal");

        // Jump button check
        jumpButtonPressed = IsTouchOverJumpButton();
    }


    void JumpInput()
    {
        if (Grounded())
        {
            coyoteTimeCounter = coyoteTime;
            lastGroundedTime = Time.time;  // Update the last grounded time
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (jumpButtonPressed)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        // Check for jump input
        if ((coyoteTimeCounter > 0f || Time.time - lastGroundedTime < coyoteTime) && jumpBufferCounter > 0f)
        {
            Jump();
            jumpBufferCounter = 0f;
        }

        // Reset coyote time if the player is no longer grounded
        if (!Grounded())
        {
            coyoteTimeCounter = 0f;
        }

        // Check for jump button release
        if (!jumpButtonPressed && rb.velocity.y > 0f)
        {
            coyoteTimeCounter = 0f;
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

    bool IsTouchOverJumpButton()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Ended)
            {
                // Check if the touch position is over the JumpButton
                Vector2 touchPos = touch.position;
                if (RectTransformUtility.RectangleContainsScreenPoint(jumpButtonRect, touchPos))
                {
                    return touch.phase == TouchPhase.Began;  // Return true only on touch start
                }
            }
        }
        return false;
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

        if (jumpButtonPressed && wallJumpingCounter > 0f)
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

    void Flip()
    {
        if (inputHorizontal > 0f && !isFacingRight || inputHorizontal < 0f && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
