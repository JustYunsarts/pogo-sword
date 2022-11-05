using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer gatorSprite;
    [SerializeField]
    private float walkVelocity;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Collider2D gator_Collider;

    private Vector2 ApplyMove;
    private float horizontalSpeed;

    private bool JumpDown;
    private bool isGrounded;
    private bool JumpUp;

    [SerializeField]
    private Transform feetpos;
    [SerializeField]
    private float feetRadius;
    public LayerMask groundChecker;
    [SerializeField]
    private float fallVelocity;


    private void Update()
    {
        //all the movement logic. getMovement collects all the inputs, calculateJump handes jump logic, and Flip flips the sprite when turning.
        getMovement();
        calculateJump();
        Flip();
        //constant updates to the player state.
        if(isGrounded)
        {
            PlayerStateManager.state = PlayerStateManager.CurrentState.Idle;
        }
        if(!isGrounded)
        {
            PlayerStateManager.state = PlayerStateManager.CurrentState.Jumping;
        }
    }

    void getMovement()
    {
        ApplyMove.x = Input.GetAxisRaw("Horizontal");
        JumpDown = Input.GetButtonDown("Jump");
        JumpUp = Input.GetButtonUp("Jump");
    }

    private void calculateJump()
    {
        //if the Transform feetpos is touching something with in "Ground" layer, we set isGround to true
        isGrounded = Physics2D.OverlapCircle(feetpos.position, feetRadius, groundChecker);

        //if JumpDown is true, then this is the frame that you pressed jump. if isgrounded is true, then you can really jump.
        if(JumpDown && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // If you release the jump button early, you should halve velocity to naturally fall
        if(JumpUp && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * fallVelocity);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = (new Vector2(ApplyMove.x * walkVelocity , rb.velocity.y));

    }

    private void Flip()
    {
        if(ApplyMove.x == -1)
        {
            gatorSprite.flipX = true;
        }
        if(ApplyMove.x == 1)
        {
            gatorSprite.flipX = false;
        }
    }
}
