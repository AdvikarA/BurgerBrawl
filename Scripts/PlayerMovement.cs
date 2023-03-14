using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    private float moveX;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float acceleration = 7f;
    public float deceleration = 7f;
    public float velPower = 0.9f;

    [Header("Jump")]
    public float jumpForce = 3f;
    public float gravityScale = 1f;
    public float fallGravityMultiplier = 2f;


    [Header("Ground Check")]
    public Transform groundCheck;
    public Vector2 groundCheckSize = new Vector2(1f, 1f);
    public LayerMask groundLayer;

    private void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        // Move
        Move();

        // Jump
        if (Input.GetKey(KeyCode.Space) && Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer))
        {
            Jump();
        }

        if (rb.velocity.y < 0)
        {
            rb.gravityScale = gravityScale * fallGravityMultiplier;
        }
        else
        {
            rb.gravityScale = gravityScale;
        }
    }

    private void Move()
    {
        float targetSpeedX = moveX * moveSpeed;
        float speedDifX = targetSpeedX - rb.velocity.x;
        float accelRateX = (Mathf.Abs(targetSpeedX) > 0.01f) ? acceleration : deceleration;
        float movementX = Mathf.Pow(Mathf.Abs(speedDifX) * accelRateX, velPower) * Mathf.Sign(speedDifX);

        rb.AddForce(new Vector2(movementX, 0f));
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}